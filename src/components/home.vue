<template>
  <el-container class="container-style">
    <el-aside :width="iscollaspe ? '65px' : '300px'">
      <el-header v-if="!iscollaspe"
                 class="aside_head"
                 height="60px">
        <div class="title">
          <strong>后台管理</strong>
        </div>
      </el-header>
      <el-menu default-active="2"
               class="el-menu-vertical-demo"
               unique-opened
               background-color="#545c64"
               text-color="#fff"
               active-text-color="#ffd04b"
               :collapse=" iscollaspe"
               :collapse-transition="false"
               router>
        <el-submenu index="1">
          <template slot="title">
            <i class="el-icon-user-solid" />
            <span>用户管理</span>
          </template>
          <el-menu-item index="/user">
            <template slot="title">
              <i class="el-icon-s-unfold" />
              <span>用户列表</span>
            </template>
          </el-menu-item>
        </el-submenu>
        <el-submenu index="3">
          <template slot="title">
            <i class="el-icon-s-goods" />
            <span>书籍管理</span>
          </template>
          <el-menu-item index="/books">
            <template slot="title">
              <i class="el-icon-s-unfold" />
              <span>书籍列表</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/type">
            <template slot="title">
              <i class="el-icon-s-unfold" />
              <span>书籍分类</span>
            </template>
          </el-menu-item>
        </el-submenu>
        <el-submenu index="4">
          <template slot="title">
            <i class="el-icon-s-order" />
            <span>订单管理</span>
          </template>
          <el-menu-item index="/order">
            <template slot="title">
              <i class="el-icon-s-unfold" />
              <span>订单列表</span>
            </template>
          </el-menu-item>
        </el-submenu>
        <el-submenu index="5">
          <template slot="title">
            <i class="el-icon-s-marketing" />
            <span>数据统计</span>
          </template>
          <el-menu-item index="/welcome">
            <template slot="title">
              <i class="el-icon-location" />
              <span>数据统计</span>
            </template>
          </el-menu-item>
        </el-submenu>
      </el-menu>
    </el-aside>
    <el-container>
      <el-header class="el_head">
        <div class="toggle-button"
           @click="collaspe"> <<< </div>
        <div class="user">
          <div>
            <el-button type="info"
                       icon="el-icon-switch-button"
                       circle
                       @click="logout" />
          </div>
          <div class="avatar">
            <el-avatar icon="el-icon-user-solid" />
          </div>
        </div>
      </el-header>
      <el-main>
        <router-view />
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
export default {
  name: 'Home',
  data () {
    return {
      iscollaspe: false
    }
  },
  methods: {
    logout () {
      window.sessionStorage.clear()
      this.$router.push('/shophome')
    },
    collaspe () {
      this.iscollaspe = !this.iscollaspe
    }
  }
}

</script>

<style lang="less" scoped>
.el-aside {
  background-color: #d7cfcf;
}
.el-main {
  background-color: #fff;
}
.el_head {
  background-color: #d7cfcf;
  display: flex;
}
.user {
  margin-left: 88rem;
  display: flex;
  margin-top: 10px;
}
.user >.avatar {
  margin-left: 10px;
}
.aside_head {
  background-color: #354968;
  padding: 10px;

  .title {
    width: 100%;
    height: 60px;
    display: flex;
    justify-content: center;
    font-size: 25px;
    font: Poppins;
    color: #fff;
  }

  .user {
    display: flex;
    justify-content: center;
    color: #fff;
    border: #fbf;
    .el-avatar {
      background-color: blue;
      font-size: 50px;
    }
  }
}
.container-style {
  height: 100%;
  margin: 0;
  padding: 0;
}

.toggle-button {
  

  color: #fff;
  font-size: 30px;
  line-height: 60px;
  text-align: center;
 
  cursor: pointer;
}
</style>
