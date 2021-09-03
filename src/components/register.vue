<template>
  <div class="login_container">
    <div class="login_box">
      <el-form label-width="0px" class="login_form" ref="loginFormRef" :model='loginform' :rules="loginrules">
        <el-form-item prop="user">
          <el-input v-model="loginform.user" class="form_control" prefix-icon=" el-icon-user-solid" placeholder="用户名"></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input v-model="loginform.password" class="form_control" prefix-icon=" el-icon-lock " type="password" placeholder="密码"></el-input>
        </el-form-item>
        <el-form-item prop="tel">
          <el-input v-model="loginform.tel" class="form_control" prefix-icon=" el-icon-phone" placeholder="电话"></el-input>
        </el-form-item>
        <el-form-item prop="email">
          <el-input v-model="loginform.email" class="form_control" prefix-icon=" el-icon-message" placeholder="邮箱"></el-input>
        </el-form-item>
        <div class="admin">
          <el-radio v-model="loginform.admin" label=true>超级管理员</el-radio>
          <el-radio v-model="loginform.admin" label=false>普通管理员</el-radio>
        </div>
        <el-button type="success" class="bt" @click="register">注册</el-button>
      </el-form>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      loginform: {
        user: '',
        password: '',
        tel: '',
        email: '',
        admin: 'true'
      },

      loginrules: {
        user: [
          { required: true, message: '请输入用户名', trigger: 'blur' },
          { min: 2, max: 5, message: '长度在 2 到 5 个字符', trigger: 'blur' }
        ],
        password: [
          { min: 3, max: 15, message: '长度在 3 到 16 个字符', trigger: 'blur' }
        ],
        tel: [
          { min: 3, max: 14, message: '长度在 3 到 14 个字符', trigger: 'blur' }
        ],
      }
    }
  },

  methods: {
    register() {
      this.$refs.loginFormRef.validate(async valid => {
        if (!valid) return
        /*
        const { status: res } = await this.$http.post('login', this.loginform)
        if (res !== 200) this.$message.error('登陆失败!')  //为什么只能收到200的状态呢？
        this.$message.success('登陆成功！')
       */
        const res = await this.$http.post('register', this.loginform)
        if (res.data === 'None') return this.$message.error('注册失败!')
        this.$message.success('注册成功！')
        // console.log(res)
        this.$router.push('/login')
      })
    }
  }
}

</script>
<style lang="less" scoped>
.login_container {
  background-color: #2b4b6b;
  height: 100%;
}

.login_box {
  width: 450px;
  height: 400px;
  background-color: #fff;
  border-radius: 3px;
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}

.bt {
  display: block;
  width: 100%;
  justify-content: center;
}

.login_form {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  position: absolute;
  left: 50%;
  top: 10%;
  transform: translate(-50%);
  width: 100%;
  padding: 0px 20px;
  box-sizing: border-box;
}

.form_control {
  border: 1px solid #d4dcec;
  font-size: 20px;
}

a {
  color: #000;
}

.admin {
  text-align: center;
  margin-bottom: 10px;
}

</style>
