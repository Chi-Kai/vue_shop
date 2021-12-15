<template>
  <div class="order">
    <el-container>
      <!-- 购物车头部 -->
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
      <!-- 购物车头部END -->
      <el-main style="height: 880px;">
        <div class="ordermain">
          <van-address-edit :area-list="areaList"
                            show-postal
                            show-delete
                            show-search-result
                            save-button-text="下单"
                            :search-result="searchResult"
                            :area-columns-placeholder="['请选择', '请选择', '请选择']"
                            @save="onSave"
                            @delete="onDelete" />
        </div>
      </el-main>
      <!-- 购物车为空的时候显示的内容END -->
    </el-container>
  </div>
</template>
<script>
import { mapGetters } from 'vuex'
import { areaList } from '@vant/area-data'
export default {
  data () {
    return {
      areaList,
      searchResult: [],
      token: '',
      type: [],
      input: ''
    }
  },
  mounted () {
    this.token = window.sessionStorage.getItem('token')
    this.gettype()
  },
  methods: {
    ...mapGetters([
      'getShoppingCart',
      'getTotalPrice'
    ]),
    onSave () {
      Toast('save')
    },
    onDelete () {
      Toast('delete')
    },
    async onSave (content) {
      console.log(content)
      console.log(content.city + content.country + content.county + content.addressDetail)
      var myDate = new Date()
      console.log(myDate.toLocaleDateString())
      var item = {
        id: parseInt(window.sessionStorage.getItem('userid')),
        data: myDate.toLocaleDateString(),
        addr: content.city + content.country + content.county + content.addressDetail,
        sum: this.getTotalPrice(),
        content: JSON.stringify(this.getShoppingCart()),
        name: content.name,
        tel: content.tel,
        code: content.postalCode

      }

      const res = await this.$http.post('api/order/add', item)
      if (res.data === 'succeed') {
        this.$notify.success({
          title: '成功',
          message: '下单成功'
        })

        this.$router.push('/shophome')
      }
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
    },
    async getbookdata () {
      const users = await this.$http.get('api/book/find', { params: { name: this.input } })
      if (users.data.length === 0) return this.$message.error('查无此书')
      console.log(users.data)
      this.$router.push({ path: `/goods/${users.data[0].bookid}` })
      // this.booklist = users.data
    }
  }
}
</script>
<style scoped>
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
.ordermain {
  width: 700px;
  margin-left: 600px;
  margin-right: 0px;
  margin-top: 100px;
  background-color: aliceblue;
  height: 600px;
}
.el-main {
  background-image: url('https://img1.baidu.com/it/u=1028997311,610239936&fm=26&fmt=auto');
}
/* 购物车为空的时候显示的内容CSS END */
</style>
