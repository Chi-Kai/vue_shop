<template>
  <div class="shophome">
    <el-container>
      <el-header>
        <el-menu class="title"
                 mode="horizontal"
                 router>
          <el-submenu index="2">
            <template slot="title">
              <span style="font-size:20px;font-weight: 1000;">海大书店</span>
            </template>
            <el-menu-item index="/list">
              排行榜
            </el-menu-item>
            <el-menu-item index="/cart">
              购物车
            </el-menu-item>
            <el-submenu index="2-4">
              <template slot="title">
                分类
              </template>
              <div v-for="item in type">
                <el-menu-item>
                  {{ item.type }}
                </el-menu-item>
              </div>
            </el-submenu>
          </el-submenu>
        </el-menu>
        <div class="search">
          <el-input v-model="input"
                    style="width:300px"
                    placeholder="输入书籍名称" />
          <el-button type="primary"
                     icon="el-icon-search"
                     circle />
        </div>
        <div v-if="token==='true' || !token"
             class="login">
          <el-link :underline="false"
                   @click="login">
            登录
          </el-link>
        </div>
        <div v-if="token==='false'"
             class="login">
          <el-link :underline="false"
                   @click="logout">
            注销
          </el-link>
        </div>
        <div class="avatar">
          <el-avatar icon="el-icon-user-solid" />
        </div>
      </el-header>
      <el-main>
        <div class="main">
          <el-carousel :interval="4000"
                       type="card"
                       height="400px">
            <el-carousel-item v-for="item in imgs"
                              :key="item.id">
              <img :src="item.url"
                   style="max-width: 100%;
                          max-height: 100%;
                          width: 100%;">
            </el-carousel-item>
          </el-carousel>
          <el-container>
            <el-header>
              热销榜
            </el-header>
            <el-main>
              <div class="hot">
                <img src="../assets/shop/1.jpg">
                <img src="../assets/shop/2.jpg">
                <img src="../assets/shop/3.jpg">
                <img src="../assets/shop/4.jpg">
              </div>
            </el-main>
          </el-container>
          <el-container>
            <el-header>
              新书榜
            </el-header>
            <el-main>
              <div class="hot">
                <img src="../assets/shop/1.jpg">
                <img src="../assets/shop/2.jpg">
                <img src="../assets/shop/3.jpg">
                <img src="../assets/shop/4.jpg">
              </div>
            </el-main>
          </el-container>
          <el-container>
            <el-header>
              推荐书单
            </el-header>
            <el-main>
              <div class="hot">
                <img src="../assets/shop/1.jpg">
                <img src="../assets/shop/2.jpg">
                <img src="../assets/shop/3.jpg">
                <img src="../assets/shop/4.jpg">
              </div>
            </el-main>
          </el-container>
          <el-container>
            <el-header>
              分类榜单
            </el-header>
            <el-main>
              <div class="hot">
                <img src="../assets/shop/1.jpg">
                <img src="../assets/shop/2.jpg">
                <img src="../assets/shop/3.jpg">
                <img src="../assets/shop/4.jpg">
              </div>
            </el-main>
          </el-container>
        </div>
      </el-main>
    </el-container>
  </div>
</template>

<script>
export default {
  data () {
    return {
      type: [],
      token: '',
      imgs: [
        {
          id: '1',
          url: 'https://puui.qpic.cn/vcover_hz_pic/0/o8mbrpo92gni5uc1614137102933/0'
        },
        {
          id: '2',
          url: 'https://puui.qpic.cn/vcover_hz_pic/0/mzc002004jql6911612779226790/0'
        },
        {
          id: '3',
          url: 'https://puui.qpic.cn/vcover_hz_pic/0/mzc002004660qpk1610075881185/0'
        },
        {
          id: '4',
          url: 'https://puui.qpic.cn/vcover_hz_pic/0/mzc00200x9fxrc91603793682527/0'
        }
      ]

    }
  },
  created () {
    this.token = window.sessionStorage.getItem('token')
    this.gettype()
  },
  methods: {
    login () {
      this.$router.push('/login')
    },
    logout () {
      window.sessionStorage.clear()
      this.$router.go(0)
    },
    async gettype () {
      const res = await this.$http.get('api/book/alltype')
      this.type = res.data
    }
  }
}
</script>
<style lang="less" scoped>
.el-header {
  display: flex;
}
.title {
  margin-right: 200px;
}
.search {
  display: grid;
  grid-template-columns: 1fr 40px;
  grid-gap: 30px;
  margin-top: 20px;
}
.el-button {
}

.el-input {
}

.login {
  margin-left: 1050px;
  margin-top: 20px;
}
.avatar {
  margin-top: 10px;
  margin-left: 30px;
}

.main {
  margin-top: 40px;
  margin-left: 200px;
  margin-right: 200px;
}
.hot {
  display: grid;
  grid-template-columns: 300px 300px 300px 300px;
  grid-template-rows: 300px;
  grid-gap: 30px;
}
.hot > img {
  width: 100%;
  height: 100%;
}

.main .el-header {
  background-color: #b3c0d1;
  color: #333;
  text-align: center;
  line-height: 60px;
}
</style>
