<template>
  <div class="login_container">
    <div class="login_box">
      <div class="head_box">
        <img src="../assets/head.jpeg">
      </div>
      <el-form ref="loginFormRef"
               label-width="0px"
               class="login_form"
               :model="loginform"
               :rules="loginrules">
        <el-form-item prop="username">
          <el-input v-model="loginform.username"
                    class="form_control"
                    prefix-icon=" el-icon-user-solid" />
        </el-form-item>
        <el-form-item prop="password">
          <el-input v-model="loginform.password"
                    class="form_control"
                    prefix-icon=" el-icon-lock "
                    type="password" />
        </el-form-item>

        <el-button type="success"
                   class="bt"
                   @click="login">
          登陆
        </el-button>
        <div class="url">
          <a @click="register">注册</a>
          |
          <a href="#">忘记密码</a>
        </div>
      </el-form>
    </div>
  </div>
</template>

<script>
export default {
  data () {
    return {
      loginform: {
        username: 'admin',
        password: '123456'
      },

      loginrules: {
        username: [
          { required: true, message: '请输入用户名', trigger: 'blur' },
          { min: 3, message: '长度在 3 到 5 个字符', trigger: 'blur' }
        ],
        password: [
          { min: 3, max: 10, message: '长度在 6 到 10 个字符', trigger: 'blur' }
        ]
      }
    }
  },

  methods: {
    login () {
      this.$refs.loginFormRef.validate(async valid => {
        if (!valid) return
        try {
          const res = await this.$http.post('api/user/login', this.loginform)
          this.$message.success('登陆成功！')
          console.log(res.data)
          window.sessionStorage.setItem('token', res.data)
          this.$router.push('/home')
        } catch (error) {
          if (error.response) {
            if (error.response.status === 401) this.$message.error('帐号或者密码错误!')
          }
        } finally {

        }
      })
    },
    register () {
      this.$router.push('/register')
    }
  }
}

</script>

<style lang="less" scoped>
.login_container {
  background-color: #2b4b6b;
  height: 100%;
  background-image: url('../assets/bg.jpg');
  background-size: 100%;
  background-repeat: no-repeat;
}
.login_box {
  width: 450px;
  height: 300px;
  background-color: #fff9;
  border-radius: 40px;
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);

  .head_box {
    width: 130px;
    height: 130px;
    border-radius: 50%;
    padding: 10px;
    border: 1px solid #eee;
    box-shadow: 0 0 10px #ddd;
    position: absolute;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    img {
      width: 100%;
      height: 100%;
      border-radius: 50%;
      background-color: #eee;
    }
  }
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
  top: 30%;
  transform: translate(-50%);
  width: 100%;
  padding: 0px 20px;
  box-sizing: border-box;
}

.form_control {
  border: 1px solid #d4dcec;
  font-size: 20px;
}

.url {
  padding: 5px;
  text-align: center !important;
  cursor: pointer;
}

a {
  color: #000;
}
</style>
