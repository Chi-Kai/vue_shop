import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../components/Login.vue'
import register from '../components/register.vue'
import home from '../components/home.vue'
import welcome from '../components/welcome.vue'
import user from '../components/users/user.vue'

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
    redirect: '/login'
  },
  {
    path: '/home',
    name: 'home',
    component: home,
    redirect: '/welcome',
    children: [
      { path: '/welcome', name: 'welcome', component: welcome },
      { path: '/user', name: 'user', component: user }
    ]
  }

]

const router = new VueRouter({
  routes
})

router.beforeEach((to, from, next) => {
  if (to.path === '/login') return next()
  if (to.path === '/register') return next()
  const token = window.sessionStorage.getItem('token')
  if (!token) return next('/login')
  next()
})

export default router
