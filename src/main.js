import Vue from 'vue'
import App from './App.vue'
import router from './router'
import './assets/css/gloab.css'
import './plugins/element.js'
import axios from 'axios'
import store from './store'

axios.defaults.baseURL = 'http://127.0.0.1:5243'
Vue.prototype.$http = axios
Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
