import Vue from 'vue'
import App from './App.vue'
import router from './router'
import './assets/css/gloab.css'
import './plugins/element.js'
import axios from 'axios'

axios.defaults.baseURL = 'http://127.0.0.1:5000'
Vue.prototype.$http = axios
Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
