<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">
        首页
      </el-breadcrumb-item>
      <el-breadcrumb-item>书籍管理</el-breadcrumb-item>
      <el-breadcrumb-item>分类管理</el-breadcrumb-item>
    </el-breadcrumb>
    <el-card>
      <el-row :gutter="20">
        <el-col :span="8">
          <el-input v-model="type"
                    placeholder="请输入书籍名称"
                    class="input-with-select"
                    clearable
                    @clear="getdata">
            <el-button slot="append"
                       icon="el-icon-plus"
                       @click="add" />
          </el-input>
        </el-col>
        <el-table :data="typelist"
                  style="width: 100%">
          <el-table-column label="序号"
                           width="180">
            <template slot-scope="scope">
              <span>{{ scope.row.typeid }}</span>
            </template>
          </el-table-column>
          <el-table-column label="类型"
                           width="180">
            <template slot-scope="scope">
              <span>{{ scope.row.type }}</span>
            </template>
          </el-table-column>

          <el-table-column label="操作">
            <template slot-scope="scope">
              <el-button size="mini"
                         type="danger"
                         @click="deletebook(scope.row.typeid)">
                删除
              </el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-row>
    </el-card>
  </div>
</template>
<script>
export default {
  // 全局数据

  data () {
    // 数据
    return {
      // 获取的列表
      typelist: [],
      type: ''
    }
  },
  created () {
    this.getdata()
  },
  methods: {
    async getdata () {
      const types = await this.$http.get('api/book/alltype')
      if (types.status !== 200) return this.$message.error('获取数据失败')
      this.typelist = types.data
    },
    // 删除
    deletebook (id) {
      this.$confirm('此操作将删除此图书记录, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        // 报错await is a reserved word，是因为错把async放到deleteuser上，这里async和await是成对出现的，所以应该放在匿名函数的位置，加async的函数会被await阻塞，await会跳出async让出线程，
        const res = await this.$http.get('api/book/deletetype', { params: { id: id } })
        console.log(res)
        if (res.status !== 200) return this.$message.error('删除失败!')
        this.$message({ type: 'success', message: '删除成功!' })
        this.getdata()
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        })
      })
    },
    async add () {
      const res = await this.$http.post('api/book/addtype', { type: this.type })
      console.log(res)
      this.getdata()
    }
  }
}
</script>
<style lang="less" scoped>
</style>
