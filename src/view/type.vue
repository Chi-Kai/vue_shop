<template>
  <div class="list">
    <el-container>
      <el-header>
        <el-menu class="title"
                 mode="horizontal"
                 router>
          <el-submenu index="2">
            <template slot="title">
              <span style="font-size:20px;font-weight: 1000;">
                海大书店
              </span>
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
      <el-main style="height: 880px;">
        <div class="main-list">
          <div v-for="item in list"
               :key="item.bookid"
               class="main">
            <div class="box">
              <router-link :to="{name:'goods',params:{id:item.bookid}}">
                <img :src="item.picture">
              </router-link>
              <div class="pro">
                <span class="bname">{{ item.name }}</span>

                <span class="bautor">作者: {{ item.autor }}</span>
                <span class="bpress">出版社: {{ item.press }}</span>

                <span class="bprice">价格: {{ item.price }}</span>
                <el-button class="order"
                           type="success"
                           @click="add(item)">
                  加入购物车
                </el-button>
              </div>
            </div>
          </div>
        </div>
        <el-pagination :current-page="queryInfo.pagenow"
                       :page-sizes="[5,10,20,30,50]"
                       :page-size="queryInfo.pagesize"
                       layout="total, sizes, prev, pager, next, jumper"
                       @size-change="handleSizeChange"
                       @current-change="handleCurrentChange" />
      </el-main>
    </el-container>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  data () {
    return {
      list: [],
      queryInfo: {
        pagesize: 10,
        pagenow: 1
      },
      type: [],
      token: '',
      input: '',
      id: ''
    }
  },
  mounted () {
    this.token = window.sessionStorage.getItem('token')
    this.id = this.$route.params.id
    this.getdata()
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

    async getdata () {
      console.log(this.id)
      const res = await this.$http.get('api/book/findbytype', { params: { name: this.id } })
      this.list = res.data
      console.log(this.data)
    },
    handleSizeChange (newSize) {
      this.queryInfo.pagesize = newSize
      this.getdata()
    },
    handleCurrentChange (newPage) {
      this.queryInfo.pagenow = newPage
      this.getdata()
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
    ...mapGetters([
      'getShoppingCart'
    ]),
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
  background-image: url('https://img1.baidu.com/it/u=1104741512,1892509565&fm=26&fmt=auto');
}
.main {
  margin-top: 40px;
  margin-left: 200px;
  margin-right: 400px;
}
.box {
  margin-top: 60px;
  display: flex;
}
img {
  max-height: 400px;
  max-width: 400px;
}
.main-list {
  margin-top: 20px;
  margin-left: 400px;
  margin-right: 400px;
  border-radius: 40px;
  background-color: #f0f8ffd4;
}
.pro {
  display: flex;
  flex-direction: column;
  margin-left: 100px;
}

.bname {
  font-size: 30px;
  margin-bottom: 10px;
}
.bautor {
  margin-bottom: 10px;
}
.bprice {
  margin-top: 10px;
  margin-bottom: 20px;
}
.order {
  width: 150px;
}
.el-pagination {
  margin-top: 60px;
  margin-left: 700px;
}
</style>
