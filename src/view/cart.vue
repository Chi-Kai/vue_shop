<!--
 * @Description: 我的购物车页面组件
 * @Author: hai-27
 * @Date: 2020-02-20 01:55:47
 * @LastEditors: hai-27
 * @LastEditTime: 2020-02-27 13:36:42
 -->

<template>
  <div class="shoppingCart">
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
                  {{ item.type }}
                </el-menu-item>
              </div>
            </el-submenu>
          </el-submenu>
        </el-menu>
        <div class="search">
          <el-input style="width:300px"
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
      <!-- 购物车头部END -->
      <el-main>
        <!-- 购物车主要内容区 -->
        <div v-if="getShoppingCart.length>0"
             class="content">
          <ul>
            <!-- 购物车表头 -->
            <li class="header">
              <div class="pro-check">
                <el-checkbox v-model="isAllCheck">
                  全选
                </el-checkbox>
              </div>
              <div class="pro-img" />
              <div class="pro-name">
                商品名称
              </div>
              <div class="pro-price">
                单价
              </div>
              <div class="pro-num">
                数量
              </div>
              <div class="pro-total">
                小计
              </div>
              <div class="pro-action">
                操作
              </div>
            </li>
            <!-- 购物车表头END -->

            <!-- 购物车列表 -->
            <li v-for="(item,index) in getShoppingCart"
                :key="item.id"
                class="product-list">
              <div class="pro-check">
                <el-checkbox :value="item.check"
                             @change="checkChange($event,index)" />
              </div>
              <div class="pro-img">
                <router-link :to="{ path: '/goods/details', query: {productID:item.productID} }">
                  <img :src="item.productImg">
                </router-link>
              </div>
              <div class="pro-name">
                <router-link :to="{ path: '/goods/details', query: {productID:item.productID} }">
                  {{ item.productName }}
                </router-link>
              </div>
              <div class="pro-price">
                {{ item.price }}元
              </div>
              <div class="pro-num">
                <el-input-number size="small"
                                 :value="item.num"
                                 :min="1"
                                 :max="item.maxNum"
                                 @change="handleChange($event,index,item.productID)" />
              </div>
              <div class="pro-total pro-total-in">
                {{ item.price*item.num }}元
              </div>
              <div class="pro-action">
                <el-popover placement="right">
                  <p>确定删除吗？</p>
                  <div style="text-align: right; margin: 10px 0 0">
                    <el-button type="primary"
                               size="mini"
                               @click="deleteItem($event,item.productID)">
                      确定
                    </el-button>
                  </div>
                  <i slot="reference"
                     class="el-icon-error"
                     style="font-size: 18px;" />
                </el-popover>
              </div>
            </li>
            <!-- 购物车列表END -->
          </ul>
          <div style="height:20px;background-color: #f5f5f5" />
          <!-- 购物车底部导航条 -->
          <div class="cart-bar">
            <div class="cart-bar-left">
              <span>
                <router-link to="/goods">继续购物</router-link>
              </span>
              <span class="sep">|</span>
              <span class="cart-total">
                共
                <span class="cart-total-num">{{ getNum }}</span> 件商品，已选择
                <span class="cart-total-num">{{ getCheckNum }}</span> 件
              </span>
            </div>
            <div class="cart-bar-right">
              <span>
                <span class="total-price-title">合计：</span>
                <span class="total-price">{{ getTotalPrice }}元</span>
              </span>
              <router-link :to="getCheckNum > 0 ? '/confirmOrder' : ''">
                <div :class="getCheckNum > 0 ? 'btn-primary' : 'btn-primary-disabled'">
                  去结算
                </div>
              </router-link>
            </div>
          </div>
          <!-- 购物车底部导航条END -->
        </div>
        <!-- 购物车主要内容区END -->

        <!-- 购物车为空的时候显示的内容 -->
        <div v-else
             class="cart-empty">
          <div class="empty">
            <h2>您的购物车还是空的！</h2>
            <p>快去购物吧！</p>
          </div>
        </div>
      </el-main>
      <!-- 购物车为空的时候显示的内容END -->
    </el-container>
  </div>
</template>
<script>
import { mapActions, mapGetters } from 'vuex'

export default {
  data () {
    return { type: [], token: '' }
  },
  methods: {
    ...mapActions(['updateShoppingCart', 'deleteShoppingCart', 'checkAll', 'setShoppingCart']),
    // 修改商品数量的时候调用该函数
    handleChange (currentValue, key, productID) {
      // 当修改数量时，默认勾选
      this.updateShoppingCart({ key: key, prop: 'check', val: true })
      this.updateShoppingCart({
        key: key,
        prop: 'num',
        val: currentValue
      })
      // 向后端发起更新购物车的数据库信息请求
      this.$http
        .post('/api/cart/alter', {
          id: window.sessionStorage.getItem('userid'),
          cart: JSON.stringify(this.$store.getters.getShoppingCart)
        })
        .then(res => {
          switch (res.data.code) {
            case '200':
              // “001”代表更新成功
              // 更新vuex状态
              this.updateShoppingCart({
                key: key,
                prop: 'num',
                val: currentValue
              })
              // 提示更新成功信息
              this.$notify({
                title: '成功',
                message: '增添成功',
                type: 'success'
              })
              break
            default:
              // 提示更新失败信息
              this.$notify({
                title: '失败',
                message: '增添失败',
                type: 'error'
              })
          }
        })
        .catch(err => {
          return Promise.reject(err)
        })
    },
    checkChange (val, key) {
      // 更新vuex中购物车商品是否勾选的状态
      this.updateShoppingCart({ key: key, prop: 'check', val: val })
    },
    // 向后端发起删除购物车的数据库信息请求
    deleteItem (e, id) {
      this.deleteShoppingCart(id)
      this.$http
        .post('/api/cart/alter', {
          id: window.sessionStorage.getItem('userid'),
          cart: JSON.stringify(this.$store.getters.getShoppingCart)
        })
        .then(res => {
          console.log(res.data)
          switch (res.data) {
            case 'alter succeed':
              // “001” 删除成功
              // 更新vuex状态
              this.deleteShoppingCart(id)
              // 提示删除成功信息

              break
            default:
            // 提示删除失败信息
          }
        })
        .catch(err => {
          return Promise.reject(err)
        })
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
    async init () {
      this.gettype()
      this.token = window.sessionStorage.getItem('token')
      const id = window.sessionStorage.getItem('userid')
      const cart = await this.$http.get('api/cart/find', { params: { id: id } })
      this.setShoppingCart(JSON.parse(cart.data[0].cart))
    }
  },
  mounted () {
    this.init()
  },
  computed: {
    ...mapGetters([
      'getShoppingCart',
      'getCheckNum',
      'getTotalPrice',
      'getNum'
    ]),
    isAllCheck: {
      get () {
        return this.$store.getters.getIsAllCheck
      },
      set (val) {
        this.checkAll(val)
      }
    }
  }
}
</script>
<style scoped>
.shoppingCart {
  background-color: #f5f5f5;
  padding-bottom: 20px;
}
/* 购物车头部CSS */
.shoppingCart .cart-header {
  height: 64px;
  border-bottom: 2px solid #ff6700;
  background-color: #fff;
  margin-bottom: 20px;
}
.shoppingCart .cart-header .cart-header-content {
  width: 1225px;
  margin: 0 auto;
}
.shoppingCart .cart-header p {
  font-size: 28px;
  line-height: 58px;
  float: left;
  font-weight: normal;
  color: #424242;
}
.shoppingCart .cart-header span {
  color: #757575;
  font-size: 12px;
  float: left;
  height: 20px;
  line-height: 20px;
  margin-top: 30px;
  margin-left: 15px;
}
/* 购物车头部CSS END */

/* 购物车主要内容区CSS */
.shoppingCart .content {
  width: 1425px;
  margin: 0 auto;
  background-color: #fff;
  white-space: nowrap;
}

.shoppingCart .content ul {
  background-color: #fff;
  color: #424242;
  line-height: 85px;
}
/* 购物车表头及CSS */
.shoppingCart .content ul .header {
  height: 85px;
  padding-right: 26px;
  color: #424242;
}
.shoppingCart .content ul .product-list {
  height: 85px;
  padding: 15px 26px 15px 0;
  border-top: 1px solid #e0e0e0;
}
.shoppingCart .content ul .pro-check {
  float: left;
  height: 85px;
  width: 110px;
}
.shoppingCart .content ul .pro-check .el-checkbox {
  font-size: 16px;
  margin-left: 24px;
}
.shoppingCart .content ul .pro-img {
  float: left;
  height: 85px;
  width: 120px;
}
.shoppingCart .content ul .pro-img img {
  height: 80px;
  width: 80px;
}
.shoppingCart .content ul .pro-name {
  float: left;
  width: 300px;
}
.shoppingCart .content ul .pro-name a {
  color: #424242;
}
.shoppingCart .content ul .pro-name a:hover {
  color: #ff6700;
}
.shoppingCart .content ul .pro-price {
  float: left;
  width: 140px;
  padding-right: 18px;
  text-align: center;
}
.shoppingCart .content ul .pro-num {
  float: left;
  width: 150px;
  text-align: center;
}
.shoppingCart .content ul .pro-total {
  float: left;
  width: 120px;
  padding-right: 81px;
  text-align: right;
}
.shoppingCart .content ul .pro-total-in {
  color: #ff6700;
}
.shoppingCart .content ul .pro-action {
  float: left;
  width: 80px;
  text-align: center;
}
.shoppingCart .content ul .pro-action i:hover {
  color: #ff6700;
}
/* 购物车表头及CSS END */

/* 购物车底部导航条CSS */
.shoppingCart .cart-bar {
  width: 1225px;
  height: 50px;
  line-height: 50px;
  background-color: #fff;
}
.shoppingCart .cart-bar .cart-bar-left {
  float: left;
}
.shoppingCart .cart-bar .cart-bar-left a {
  line-height: 50px;
  margin-left: 32px;
  color: #757575;
}
.shoppingCart .cart-bar .cart-bar-left a:hover {
  color: #ff6700;
}
.shoppingCart .cart-bar .cart-bar-left .sep {
  color: #eee;
  margin: 0 20px;
}
.shoppingCart .cart-bar .cart-bar-left .cart-total {
  color: #757575;
}
.shoppingCart .cart-bar .cart-bar-left .cart-total-num {
  color: #ff6700;
}
.shoppingCart .cart-bar .cart-bar-right {
  float: right;
}
.shoppingCart .cart-bar .cart-bar-right .total-price-title {
  color: #ff6700;
  font-size: 14px;
}
.shoppingCart .cart-bar .cart-bar-right .total-price {
  color: #ff6700;
  font-size: 30px;
}
.shoppingCart .cart-bar .cart-bar-right .btn-primary {
  float: right;
  width: 200px;
  text-align: center;
  font-size: 18px;
  margin-left: 50px;
  background: #ff6700;
  color: #fff;
}
.shoppingCart .cart-bar .cart-bar-right .btn-primary-disabled {
  float: right;
  width: 200px;
  text-align: center;
  font-size: 18px;
  margin-left: 50px;
  background: #e0e0e0;
  color: #b0b0b0;
}
.shoppingCart .cart-bar .cart-bar-right .btn-primary:hover {
  background-color: #f25807;
}
/* 购物车底部导航条CSS END */
/* 购物车主要内容区CSS END */

/* 购物车为空的时候显示的内容CSS */
.shoppingCart .cart-empty {
  width: 1225px;
  margin: 0 auto;
}
.shoppingCart .cart-empty .empty {
  height: 300px;
  padding: 0 0 130px 558px;
  margin: 65px 0 0;
  /*background: url(../assets/imgs/cart-empty.png) no-repeat 124px 0;*/
  color: #b0b0b0;
  overflow: hidden;
}
.shoppingCart .cart-empty .empty h2 {
  margin: 70px 0 15px;
  font-size: 36px;
}
.shoppingCart .cart-empty .empty p {
  margin: 0 0 20px;
  font-size: 20px;
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
/* 购物车为空的时候显示的内容CSS END */
</style>
