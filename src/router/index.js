import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../components/Login.vue'
import register from '../components/register.vue'
import home from '../components/home.vue'
import welcome from '../components/welcome.vue'
import user from '../components/users/user.vue'
import shop from '../view/shophome.vue'
import list from '../view/list.vue'
import books from '../components/books/booklist.vue'
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
      { path: '/books', name: 'books', component: books }
    ]
  },

  {
    path: '/list',
    name: 'list',
    component: list
  },
  {
    path: '/shophome',
    name: 'shophome',
    component: shop
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
  const token = window.sessionStorage.getItem('token')
  if (token === 'true') next()
  if (token === 'false') return next('/shophome')
  if (!token) return next('/login')
  next()
})

export default router
