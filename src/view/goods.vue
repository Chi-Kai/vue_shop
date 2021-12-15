<template>
  <div class="goods">
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
                     style="height: 44.1036px;"
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
        <div class=" goods-main">
          <div class="box">
            <div class="box2">
              <img :src="data[0].picture"
                   style="width: 400px;">
              <div class="pro">
                <span class="bname">{{ data[0].name }}</span>
                <span class="bautor">作者: {{ data[0].autor }}</span>
                <span class="bautor">出版社: {{ data[0].press }}</span>
                <span class="bautor">ISBN: {{ data[0].isdn }}</span>
                <span class="bautor">价格: {{ data[0].price }}</span>
                <span class="bautor">类型: {{ data[0].type }}</span>
              </div>
            </div>
            <p class="detail">
              {{ detial }}
            </p>
            <el-button class="order"
                       type="success"
                       style="margin-left:350px;margin-bottom:40px"
                       @click="add(data[0])">
              加入购物车
            </el-button>
          </div>
        </div>
      </el-main>
    </el-container>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  data () {
    return {
      id: '',
      type: [],
      token: '',
      data: [],
      detial: '',
      input: ''
    }
  },
  mounted () {
    this.token = window.sessionStorage.getItem('token')
    this.gettype()
    this.id = this.$route.params.id
    this.getdata()
  },
  methods: {
    ...mapGetters([
      'getShoppingCart'
    ]),
    async getbookdata () {
      const users = await this.$http.get('api/book/find', { params: { name: this.input } })
      if (users.data.length === 0) return this.$message.error('查无此书')
      console.log(users.data)
      this.$router.push({ path: `/goods/${users.data[0].bookid}` })
      // this.booklist = users.data
    },
    async add (item) {
      if (this.token === 'true' || !this.token) { return this.$router.push('/login') }
      this.$store.commit('unshiftShoppingCart',
        {
          productID: item.bookid, // 商品id
          productName: item.name, // 商品名称
          productImg: item.picture, // 商品图片
          price: item.price, // 商品价格
          maxNum: item.num,
          num: 1, // 商品数量
          check: false // 是否勾选
        })
      var data = {
        id: window.sessionStorage.getItem('userid'),
        cart: JSON.stringify(this.getShoppingCart())
      }
      // console.log(this.getShoppingCart())
      const res = await this.$http.post('/api/cart/alter', data)
      // console.log(res)
      if (res.status === 200) {
        this.$notify.success({
          title: '成功',
          message: '加入购物车成功'
        })
      } else {
        this.$notify.error({
          title: '失败',
          message: '加入购物车失败'
        })
      }
      this.$router.push('/shophome')
    },
    async gettype () {
      const res = await this.$http.get('api/book/alltype')
      this.type = res.data
    },
    login () {
      this.$router.push('/login')
    },
    logout () {
      window.sessionStorage.clear()
      this.$router.go(0)
    },
    async getdata () {
      const res = await this.$http.get('api/book/findbyid', { params: { id: this.id } })
      const itr = await this.$http.get('api/book/finditrbyid', { params: { id: this.id } })
      this.data = res.data
      this.detial = itr.data[0].introduce
      // console.log(this.data)
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
  margin-top: 10px;
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
.el-main {
  background-image: url('https://img1.baidu.com/it/u=184764084,4011140230&fm=26&fmt=auto');
}
.goods-main {
  border-radius: 40px;
  background-color: #f0f8ffd4;
  margin-left: 400px;
  margin-right: 400px;
}
.box {
  margin-top: 60px;
  display: flex;
  flex-direction: column;
}
img {
  max-height: 400px;
  max-width: 400px;
  margin-left: 30px;
  margin-top: 20px;
}
.bname {
  font-size: 30px;
  margin-bottom: 30px;
  font-style: italic;
  color: rgb(61, 81, 197);
}
.bautor {
  margin-bottom: 30px;
  font-size: 22px;
}
.detail {
  margin-top: 20px;
  margin-bottom: 20px;
  font-size: 16px;
  text-indent: 2em;
  line-height: 3em;
  letter-spacing: 5px;
}
.order {
  width: 300px;
  margin-bottom: 50px;
  margin-left: 400px;
}
.pro {
  display: flex;
  flex-direction: column;

  margin-top: 50px;
  margin-left: 200px;
}
.box2 {
  display: flex;
}
</style>
