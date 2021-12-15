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
                  <router-link :to="{name:'types',params:{id:item.type}}">
                    {{ item.type }}
                  </router-link>
                </el-menu-item>
              </div>
            </el-submenu>
          </el-submenu>
        </el-menu>
        <div class="search">
          <el-input v-model="input"
                    style="width:300px"
                    placeholder="输入书籍名称"
                    clearable
                    @clear="input=''" />
          <el-button type="primary"
                     icon="el-icon-search"
                     circle
                     @click="getbookdata" />
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
                <img src="http://img3m3.ddimg.cn/14/18/29294213-1_l_8.jpg">
                <img src="http://img3m2.ddimg.cn/25/19/29239972-1_l_12.jpg">
                <img src="http://img3m8.ddimg.cn/25/27/29278978-1_l_1448.jpg">
                <img src="http://img3m8.ddimg.cn/90/13/29314188-1_l_11.jpg">
              </div>
            </el-main>
          </el-container>
          <el-container>
            <el-header>
              新书榜
            </el-header>
            <el-main>
              <div class="hot">
                <img src="http://img3m9.ddimg.cn/93/8/25092039-1_l_4.jpg">
                <img src="http://img3m5.ddimg.cn/51/34/26921715-1_l_8.jpg">
                <img src="http://img3m2.ddimg.cn/0/27/28473192-1_l_11.jpg">
                <img src="http://img3m5.ddimg.cn/98/7/23445575-1_l_10.jpg">
              </div>
            </el-main>
          </el-container>
          <el-container>
            <el-header>
              推荐书单
            </el-header>
            <el-main>
              <div class="hot">
                <img src="http://img3m2.ddimg.cn/14/2/28509242-1_l_14.jpg">
                <img src="http://img3m1.ddimg.cn/38/0/29267111-1_l_6.jpg">
                <img src="http://img3m6.ddimg.cn/23/11/29296796-1_l_12.jpg">
                <img src="http://img3m0.ddimg.cn/29/31/28971290-1_l_3.jpg">
              </div>
            </el-main>
          </el-container>
          <el-container>
            <el-header>
              分类榜单
            </el-header>
            <el-main>
              <div class="hot">
                <img src="http://img3m7.ddimg.cn/60/27/29316237-1_b_13.jpg">
                <img src="http://img3m8.ddimg.cn/56/24/29301878-1_b_3.jpg">
                <img src="http://img3m9.ddimg.cn/41/3/29322059-1_b_27.jpg">
                <img src="http://img3m0.ddimg.cn/42/4/29322060-1_b_15.jpg">
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
      input: '',
      type: [],
      token: '',
      imgs: [
        {
          id: '1',
          url: 'http://img62.ddimg.cn/upload_img/00838/cxtc/750x315_1210-1639460166.jpg'
        },
        {
          id: '2',
          url: 'http://img60.ddimg.cn/upload_img/00877/202111/750x315-1637735923.jpg'
        },
        {
          id: '3',
          url: 'http://img61.ddimg.cn/cuxiao/1215cx750-315.jpg'
        },
        {
          id: '4',
          url: 'http://img57.ddimg.cn/9002820235646047.jpg'
        }
      ]

    }
  },
  created () {
    this.token = window.sessionStorage.getItem('token')
    this.gettype()
  },
  methods: {
    async getbookdata () {
      const users = await this.$http.get('api/book/find', { params: { name: this.input } })
      if (users.data.length === 0) return this.$message.error('查无此书')
      console.log(users.data)
      this.$router.push({ path: `/goods/${users.data[0].bookid}` })
      // this.booklist = users.data
    },
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
<style scoped>
.el-submenu__title {
  border-bottom: 2px solid #ebeef5 !important;
  color: #303133;
}
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
  margin-left: 1000px;
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
