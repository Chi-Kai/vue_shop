import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../components/Login.vue'
import register from '../components/register.vue'
import home from '../components/home.vue'
import welcome from '../components/welcome.vue'
import user from '../components/users/user.vue'
import shop from '../view/shophome.vue'
import list from '../view/list.vue'
import cart from '../view/cart.vue'
import order from '../view/order.vue'
import goods from '../view/goods.vue'
import types from '../view/type.vue'
import books from '../components/books/booklist.vue'
import type from '../components/books/type.vue'
import orders from '../components/books/order.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/login',
    name: 'login',
    component: Login
  },
  {
    path: '/register',
    name: 'register',
    component: register
  },
  {
    path: '/',
    redirect: '/shophome'
  },
  {
    path: '/home',
    name: 'home',
    component: home,
    redirect: '/welcome',
    children: [
      { path: '/welcome', name: 'welcome', component: welcome },
      { path: '/user', name: 'user', component: user },
      { path: '/type', name: 'type', component: type },
      { path: '/order', name: 'order', component: orders },
      { path: '/books', name: 'books', component: books }

    ]
  },

  {
    path: '/list',
    name: 'list',
    component: list
  },
  {
    path: '/cart',
    name: 'cart',
    component: cart
  },
  {
    path: '/orderback',
    name: 'orderback',
    component: order
  },
  {
    path: '/shophome',
    name: 'shophome',
    component: shop
  },
  {
    path: '/goods/:id',
    name: 'goods',
    component: goods
  },
  {
    path: '/types/:id',
    name: 'types',
    component: types
  }

   
]

const router = new VueRouter({
  routes
})
// 控制路由
router.beforeEach((to, from, next) => {
  if (to.path === '/shophome') return next()
  if (to.path === '/list') return next()
  if (to.path === '/login') return next()
  if (to.path === '/register') return next()
  if (to.path.split('/goods/')[1]) return next() // 参数守卫
  if (to.path.split('/types/')[1]) return next() // 参数守卫
  const token = window.sessionStorage.getItem('token')
  if (!token) return next('/login')
  next()
})

export default router
