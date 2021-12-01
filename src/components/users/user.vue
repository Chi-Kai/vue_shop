<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">
        首页
      </el-breadcrumb-item>
      <el-breadcrumb-item>用户管理</el-breadcrumb-item>
      <el-breadcrumb-item>用户列表</el-breadcrumb-item>
    </el-breadcrumb>
    <el-card>
      <el-row :gutter="20">
        <el-col :span="8">
          <el-input v-model="queryInfo.query"
                    placeholder="请输入内容"
                    class="input-with-select"
                    clearable
                    @clear="getuserdata">
            <el-button slot="append"
                       icon="el-icon-search"
                       @click="finduser" />
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary"
                     @click="adddialogVisible = true">
            添加用户
          </el-button>
        </el-col>
      </el-row>
      <el-table :data="userlist"
                border
                stripe
                fit>
        <el-table-column prop="id"
                         label="ID" />
        <el-table-column prop="username"
                         label="姓名" />
        <el-table-column prop="email"
                         label="邮箱" />
        <el-table-column prop="tel"
                         label="电话" />
        <el-table-column prop="admin"
                         label="权限">
          <template slot-scope="scope">
            <el-switch v-model="scope.row.admin"
                       active-value="true"
                       inactive-value="false"
                       @change="userStateChange(scope.row)" />
          </template>
        </el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button type="primary"
                       icon="el-icon-edit"
                       circle
                       @click="editclick(scope.row)" />
            <el-button type="success"
                       icon="el-icon-delete"
                       circle
                       @click="deleteuser(scope.row.id)" />
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
    <el-dialog title="添加用户"
               :visible.sync="adddialogVisible"
               width="500px"
               @close="closeAdddialog">
      <el-form ref="addFormRef"
               class="add_form"
               :model="addform"
               :rules="addrules">
        <el-form-item prop="username">
          <el-input v-model="addform.username"
                    class="form_control"
                    prefix-icon=" el-icon-user-solid"
                    placeholder="用户名" />
        </el-form-item>
        <el-form-item prop="password">
          <el-input v-model="addform.password"
                    class="form_control"
                    prefix-icon=" el-icon-lock "
                    type="password"
                    placeholder="密码" />
        </el-form-item>
        <el-form-item prop="tel">
          <el-input v-model="addform.tel"
                    class="form_control"
                    prefix-icon=" el-icon-phone"
                    placeholder="电话" />
        </el-form-item>
        <el-form-item prop="email">
          <el-input v-model="addform.email"
                    class="form_control"
                    prefix-icon=" el-icon-message"
                    placeholder="邮箱" />
        </el-form-item>
        <div class="admin">
          <el-radio v-model="addform.admin"
                    label="true">
            管理员
          </el-radio>
          <el-radio v-model="addform.admin"
                    label="false">
            用户
          </el-radio>
        </div>
      </el-form>
      <span slot="footer"
            class="dialog-footer">
        <el-button @click="adddialogVisible = false">取 消</el-button>
        <el-button type="primary"
                   @click="adduser">确 定</el-button>
      </span>
    </el-dialog>
    <el-dialog title="修改用户"
               :visible.sync="editdialogVisible"
               width="500px"
               @close="closeEditdialog">
      <el-form ref="editFormRef"
               class="add_form"
               :model="editform"
               :rules="addrules">
        <el-form-item prop="username">
          <el-input v-model="editform.username"
                    class="form_control"
                    prefix-icon=" el-icon-user-solid"
                    placeholder="用户名"
                    :disabled="true" />
        </el-form-item>
        <el-form-item prop="tel">
          <el-input v-model="editform.tel"
                    class="form_control"
                    prefix-icon=" el-icon-phone"
                    placeholder="电话" />
        </el-form-item>
        <el-form-item prop="email">
          <el-input v-model="editform.email"
                    class="form_control"
                    prefix-icon=" el-icon-message"
                    placeholder="邮箱" />
        </el-form-item>
      </el-form>
      <span slot="footer"
            class="dialog-footer">
        <el-button @click="editdialogVisible = false">取 消</el-button>
        <el-button type="primary"
                   @click="edituser">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script>
export default {
  // 全局数据
  data () {
    // 检验规则
    var checkEmail = (rule, value, cb) => {
      // 验证邮箱的正则表达式
      const regEmail = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/
      if (regEmail.test(value)) {
        return cb()
      }
      cb(new Error('请输入合法邮箱'))
    }
    var checkTel = (rule, value, cb) => {
      // 验证邮箱的正则表达式
      const regTel = /^[1]([3-9])[0-9]{9}$/

      if (regTel.test(value)) {
        return cb()
      }
      cb(new Error('请输入合法电话'))
    }
    // 数据
    return {
      // 获取的用户列表
      userlist: [],
      // 请求的信息
      queryInfo: {
        query: '',
        pagesize: 8,
        total: 0,
        pagenow: 1,
        admin: true
      },
      // 控制消息窗
      adddialogVisible: false,
      editdialogVisible: false,
      // 添加用户的表单
      addform: {
        username: '',
        password: '',
        tel: '',
        email: '',
        admin: 'true'
      },
      // 修改用户表单
      editform: {
        id: '',
        username: '',
        tel: '',
        email: ''
      },
      // 检验规则
      addrules: {
        username: [
          { required: true, message: '请输入用户名', trigger: 'blur' },
          { min: 2, max: 5, message: '长度在 2 到 5 个字符', trigger: 'blur' }
        ],
        password: [
          { required: true, message: '请输入密码', trigger: 'blur' },
          { min: 3, max: 15, message: '长度在 3 到 16 个字符', trigger: 'blur' }
        ],
        tel: [
          { required: true, message: '请输入电话号码', trigger: 'blur' },
          { validator: checkTel, trigger: 'blur' }
        ],
        email: [
          { required: true, message: '请输入邮箱', trigger: 'blur' },
          { validator: checkEmail, trigger: 'blur' }
        ]
      }
    }
  },
  created () {
    this.getuserdata()
  },
  methods: {
    async getuserdata () {
      const users = await this.$http.get('api/user/list', { params: { s: this.queryInfo.pagesize, p: this.queryInfo.pagenow } })
      if (users.status !== 200) return this.$message.error('获取用户数据失败')
      this.userlist = users.data
      this.queryInfo.total = parseInt(users.headers.total)
      // console.log(users)
    },
    handleSizeChange (newSize) {
      this.queryInfo.pagesize = newSize
      this.getuserdata()
    },
    handleCurrentChange (newPage) {
      this.queryInfo.pagenow = newPage
      this.getuserdata()
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
    adduser () {
      this.$refs.addFormRef.validate(async valid => {
        if (!valid) return this.$message.error('存在非法输入')
        const res = await this.$http.post('api/user/register', this.addform)
        if (res.status !== 200) return this.$message.error('添加用户失败!')
        this.$message.success('添加用户成功！')
        this.getuserdata()
        this.adddialogVisible = false
      })
    },
    // 查询函数
    async finduser () {
      const users = await this.$http.get('api/user/find', { params: { username: this.queryInfo.query } })
      if (users.status !== 200) return this.$message.error('获取用户数据失败')
      console.log(users)
      this.userlist = users.data

    },
    // 关闭重置函数
    closeAdddialog () {
      this.$refs.addFormRef.resetFields()
    },
    closeEditdialog () {
      this.$refs.editFormRef.resetFields()
    },
    // 触发修改操作
    editclick (ifno) {
      this.editform.id = ifno.id
      this.editform.username = ifno.username
      this.editform.tel = ifno.tel
      this.editform.email = ifno.email
      this.editdialogVisible = true
    },
    // 修改用户
    edituser () {
      this.$refs.editFormRef.validate(async valid => {
        if (!valid) return this.$message.error('存在非法输入')
        const res = await this.$http.post('api/user/alter', { id: this.editform.id, tel: this.editform.tel, email: this.editform.email })
        if (res.status !== 200) return this.$message.error('修改用户信息失败!')
        this.$message.success('修改用户信息成功！')
        this.getuserdata()
        this.editdialogVisible = false
      })
    },
    // 删除用户
    deleteuser (userid) {
      this.$confirm('此操作将永久删除该用户, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        // 报错await is a reserved word，是因为错把async放到deleteuser上，这里async和await是成对出现的，所以应该放在匿名函数的位置，加async的函数会被await阻塞，await会跳出async让出线程，
        const res = await this.$http.post('api/user/delete', { id: userid })
        console.log(res)
        if (res.status !== 200) return this.$message.error('删除用户失败!')
        this.$message({ type: 'success', message: '删除成功!' })
        this.getuserdata()
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        })
      })
    }
  }
}

</script>
<style lang="less" scoped>
.add_form {
  padding: 0px 20px;
  box-sizing: border-box;
}

.form_control {
  border: 1px solid #d4dcec;
  font-size: 20px;
}
</style>
