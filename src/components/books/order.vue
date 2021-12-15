<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">
        首页
      </el-breadcrumb-item>
      <el-breadcrumb-item>书籍管理</el-breadcrumb-item>
      <el-breadcrumb-item>书籍列表</el-breadcrumb-item>
    </el-breadcrumb>
    <el-card>
      <el-row :gutter="20">
        <el-col :span="8">
          <el-input v-model="queryInfo.query"
                    placeholder="请输入订单号"
                    class="input-with-select"
                    clearable
                    @clear="find">
            <el-button slot="append"
                       icon="el-icon-search"
                       @click="find" />
          </el-input>
        </el-col>
      </el-row>
      <el-table :data="list"
                border
                stripe
                fit>
        <el-table-column prop="orderid"
                         label="ID" />
        <el-table-column prop="name"
                         label="用户名" />
        <el-table-column prop="addr"
                         label="地址" />
        <el-table-column prop="code"
                         label="邮编" />
        <el-table-column prop="tel"
                         label="电话" />
        <el-table-column prop="sum"
                         label="总金额" />
        <el-table-column prop="data"
                         label="时间" />
      </el-table>

      <el-pagination :current-page="queryInfo.pagenow"
                     :page-sizes="[5, 10, 15, 20,50]"
                     :page-size="queryInfo.pagesize"
                     layout="sizes, prev, pager, next, jumper"
                     :total="queryInfo.total"
                     @size-change="handleSizeChange"
                     @current-change="handleCurrentChange" />
    </el-card>
  </div>
</template>
<script>
export default {
  data () {
    // 数据
    return {
      // 获取的列表
      list: [],
      // 请求的信息
      queryInfo: {
        query: '',
        pagesize: 10,
        pagenow: 1
      }
    }
  },
  created () {
    this.getdata()
  },
  methods: {
    async getdata () {
      const users = await this.$http.get('api/order/list', { params: { s: this.queryInfo.pagesize, p: this.queryInfo.pagenow } })
      if (users.status !== 200) return this.$message.error('获取图书数据失败')
      this.list = users.data
      console.log(users)
    },
    handleSizeChange (newSize) {
      this.queryInfo.pagesize = newSize
      this.getdata()
    },
    handleCurrentChange (newPage) {
      this.queryInfo.pagenow = newPage
      this.getdata()
    },
    async find () {
      const users = await this.$http.get('api/order/find', { params: { id: this.queryInfo.query } })
      if (users.status !== 200) return this.$message.error('获取用户数据失败')
      // console.log(users)
      this.list = users.data
    }
    // 删除
  }
}

</script>
<style lang="less" scoped>
.add_form {
  margin-top: 40px;
  padding: 0px 20px;
  box-sizing: border-box;
  height: 300px; // 改变弹出框高度
  width: 600px;
}

.form_control {
  border: 1px solid #d4dcec;
  font-size: 20px;
}
</style>
