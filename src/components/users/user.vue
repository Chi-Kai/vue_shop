<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>用户管理</el-breadcrumb-item>
      <el-breadcrumb-item>用户列表</el-breadcrumb-item>
    </el-breadcrumb>
    <el-card>
      <el-row :gutter="20">
        <el-col :span="8">
          <el-input placeholder="请输入内容"
                    class="input-with-select">
            <el-button slot="append"
                       icon="el-icon-search"></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary">添加用户</el-button>
        </el-col>
      </el-row>
      <el-table :data="userlist" border stripe fit>
        <el-table-column prop="id"
                         label="ID"
                         width="180"></el-table-column>
        <el-table-column prop="user"
                         label="姓名"
                         width="180"></el-table-column>
        <el-table-column prop="email"
                         label="邮箱"
                         width="180"></el-table-column>
        <el-table-column prop="email"
                         label="状态"
                         width="180"></el-table-column>
        <el-table-column prop="email"
                         label="操作"
                         width="180"></el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<script>
export default {
  data () {
    return {
      userlist: []
    }
  },
  created () {
    this.userdata()
  },
  methods: {
    async userdata () {
      const users = await this.$http.get('user')
      if (users.status !== 200) return this.$message.error('获取用户数据失败')
      this.userlist = users.data.res
    }
  }
}

</script>

<style lang="less" scoped>
</style>
