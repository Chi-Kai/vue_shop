<template>

  <el-container class="container-style">
    <el-aside :width="iscollaspe ? '65px' : '300px'">
      <el-header v-if="!iscollaspe"
                 class="aside_head"
                 height="200px">
        <div class="title">
          <strong> BackStage ManageMent</strong>
        </div>
        <div class="title">
          chikai
        </div>
        <div class="user">
          <el-button type="info"
                     icon="el-icon-switch-button"
                     circle
                     @click="logout"></el-button>
        </div>
      </el-header>
      <div class="toggle-button"
           @click="collaspe"> ||| </div>
      <el-menu default-active="2"
               class="el-menu-vertical-demo"
               unique-opened
               background-color="#545c64"
               text-color="#fff"
               active-text-color="#ffd04b"
               :collapse=' iscollaspe'
               :collapse-transition='false'
               router>
        <el-submenu index="1">
          <template slot="title">
            <i class="el-icon-user-solid"></i>
            <span>用户管理</span>
          </template>
          <el-menu-item index="/user">
            <template slot="title">
              <i class="el-icon-s-unfold"></i>
              <span>用户列表</span>
            </template>
          </el-menu-item>
        </el-submenu>
        <el-submenu index="2">
          <template slot="title">
            <i class="el-icon-s-cooperation"></i>
            <span>权限管理</span>
          </template>
          <el-menu-item index="2-1">
            <template slot="title">
              <i class="el-icon-s-unfold"></i>
              <span>角色列表</span>
            </template>
          </el-menu-item>
          <el-menu-item index="2-2">
            <template slot="title">
              <i class="el-icon-s-unfold"></i>
              <span>权限列表</span>
            </template>
          </el-menu-item>
        </el-submenu>
        <el-submenu index="3">
          <template slot="title">
            <i class="el-icon-s-goods"></i>
            <span>商品管理</span>
          </template>
          <el-menu-item index="3-1">
            <template slot="title">
              <i class="el-icon-s-unfold"></i>
              <span>商品列表</span>
            </template>
          </el-menu-item>
          <el-menu-item index="3-2">
            <template slot="title">
              <i class="el-icon-s-unfold"></i>
              <span>分类参数</span>
            </template>
          </el-menu-item>
          <el-menu-item index="3-3">
            <template slot="title">
              <i class="el-icon-s-unfold"></i>
              <span>商品分类</span>
            </template>
          </el-menu-item>
        </el-submenu>
        <el-submenu index="4">
          <template slot="title">
            <i class="el-icon-s-order"></i>
            <span>订单管理</span>
          </template>
          <el-menu-item index="4-1">
            <template slot="title">
              <i class="el-icon-s-unfold"></i>
              <span>订单列表</span>
            </template>
          </el-menu-item>
        </el-submenu>
        <el-submenu index="5">
          <template slot="title">
            <i class="el-icon-s-marketing"></i>
            <span>数据统计</span>
          </template>
          <el-menu-item index="5-1">
            <template slot="title">
              <i class="el-icon-location"></i>
              <span>选项1</span>
            </template>
          </el-menu-item>
        </el-submenu>
      </el-menu>
    </el-aside>
    <el-container>
      <el-header class="left_head">Header</el-header>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>

</template>

<script>
export default {
  name: 'home',
  data () {
    return {
      iscollaspe: false
    }
  },
  methods: {
    logout () {
      window.sessionStorage.clear()
      this.$router.push('/login')
    },
    collaspe () {
      this.iscollaspe = !this.iscollaspe
    }
  }
}

</script>

<style lang="less" scoped>
.el-aside {
  background-color: #1b2a3f;
}
.el-main {
  background-color: #fff;
}
.left_head {
  background-color: #212121;
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
  background-color: #212121;
  color: #fff;
  font-size: 10px;
  line-height: 60px;
  text-align: center;
  letter-spacing: 0.2;
  cursor: pointer;
}
</style>
