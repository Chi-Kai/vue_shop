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
                    placeholder="请输入书籍名称"
                    class="input-with-select"
                    clearable
                    @clear="getuserdata">
            <el-button slot="append"
                       icon="el-icon-search"
                       @click="getuserdata" />
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary"
                     @click="open()">
            添加书籍
          </el-button>
          <addform ref="form" />
        </el-col>
      </el-row>
      <el-table :data="booklist"
                border
                stripe
                fit>
        <el-table-column prop="bookid"
                         label="ID" />
        <el-table-column prop="name"
                         label="书名" />
        <el-table-column prop="autor"
                         label="作者" />
        <el-table-column prop="press"
                         label="出版社" />
        <el-table-column prop="type"
                         label="类型" />
        <el-table-column prop="sum"
                         label="数量" />
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button type="primary"
                       icon="el-icon-edit"
                       circle
                       @click="editclick(scope.row)" />
            <el-button type="success"
                       icon="el-icon-delete"
                       circle
                       @click="deletebook(scope.row.bookid)" />
            <el-button type="warning"
                       icon="el-icon-setting"
                       circle />
          </template>
        </el-table-column>
      </el-table>
      <el-pagination :current-page="queryInfo.pagenow"
                     :page-sizes="[1, 2, 4, 8,10]"
                     :page-size="queryInfo.pagesize"
                     layout="sizes, prev, pager, next, jumper"
                     :total="queryInfo.total"
                     @size-change="handleSizeChange"
                     @current-change="handleCurrentChange" />
    </el-card>
  </div>
</template>
<script>
import addform from './addform.vue'
export default {
  // 全局数据
  components: {
    addform: addform
  },
  data () {
    // 数据
    return {
      // 获取的列表
      booklist: [],
      // 请求的信息
      queryInfo: {
        pagesize: 8,
        total: 0,
        pagenow: 1
      }
    }
  },
  created () {
    this.getdata()
  },
  methods: {
    async getdata () {
      const users = await this.$http.get('api/book/list', { params: { s: this.queryInfo.pagesize, p: this.queryInfo.pagenow } })
      if (users.status !== 200) return this.$message.error('获取图书数据失败')
      this.booklist = users.data
      this.queryInfo.total = parseInt(users.headers.total)
      // console.log(users)
    },
    handleSizeChange (newSize) {
      this.queryInfo.pagesize = newSize
      this.getdata()
    },
    handleCurrentChange (newPage) {
      this.queryInfo.pagenow = newPage
      this.getdata()
    },
    async userStateChange (userifon) {
      const res = await this.$http.post('api/user/alteradmin', { id: userifon.id })
      console.log(res)
      if (res.status !== 200) {
        userifon.admin = !userifon.admin
        return this.$message.error('更新用户状态失败')
      }
      this.$message.success('更新用户状态成功')
    },
    // 删除
    deletebook (id) {
      this.$confirm('此操作将删除此图书记录, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        // 报错await is a reserved word，是因为错把async放到deleteuser上，这里async和await是成对出现的，所以应该放在匿名函数的位置，加async的函数会被await阻塞，await会跳出async让出线程，
        const res = await this.$http.get('api/book/delete', { params: { id: id } })
        console.log(res)
        if (res.status !== 200) return this.$message.error('删除图书失败!')
        this.$message({ type: 'success', message: '删除成功!' })
        this.getdata()
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        })
      })
    },
    open () {
      this.$refs.form.$emit('open')
      this.getdata()
    }
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
