<template>
  <div class="list">
    <el-container>
      <el-header>
        <h3 class="title">
          海大书店
        </h3>
        <div class="search">
          <el-input v-model="input"
                    style="width:300px"
                    placeholder="输入书籍名称" />
          <el-button type="primary"
                     icon="el-icon-search"
                     circle />
        </div>
        <div class="login">
          <el-link :underline="false"
                   @click="login">
            登录
          </el-link>
        </div>
        <div class="avatar">
          <el-avatar icon="el-icon-user-solid" />
        </div>
      </el-header>
      <el-main>
        <div v-for="item in list"
             :key="item.bookid"
             class="main">
          <div class="box">
            <img :src="item.picture">
            <div class="pro">
              <span class="bname">{{ item.name }}</span>

              <span class="bautor">作者: {{ item.autor }}</span>
              <span class="bpress">出版社: {{ item.press }}</span>

              <span class="bprice">价格: {{ item.price }}</span>
              <el-button class="order"
                         type="success">
                加入购物车
              </el-button>
            </div>
          </div>
        </div>
        <el-pagination :current-page="queryInfo.pagenow"
                       :page-sizes="[1, 2, 4, 8,10]"
                       :page-size="queryInfo.pagesize"
                       layout="total, sizes, prev, pager, next, jumper"
                       @size-change="handleSizeChange"
                       @current-change="handleCurrentChange" />
      </el-main>
    </el-container>
  </div>
</template>

<script>
export default {
  data () {
    return {
      list: [],
      queryInfo: {
        pagesize: 8,
        pagenow: 1
      }
    }
  },
  mounted () {
    this.getdata()
  },
  methods: {
    async getdata () {
      const users = await this.$http.get('api/book/list', { params: { s: this.queryInfo.pagesize, p: this.queryInfo.pagenow } })
      if (users.status !== 200) return this.$message.error('获取图书数据失败')
      this.list = users.data
      console.log(this.list)
      // this.queryInfo.total = parseInt(users.headers.total)
      // console.log(users)
    },
    handleSizeChange (newSize) {
      this.queryInfo.pagesize = newSize
      this.getdata()
    },
    handleCurrentChange (newPage) {
      this.queryInfo.pagenow = newPage
      this.getdata()
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
  margin-top: 20px;
}
.el-button {
}

.el-input {
}

.login {
  margin-left: 1100px;
  margin-top: 20px;
}
.avatar {
  margin-top: 10px;
  margin-left: 30px;
}

.main {
  margin-top: 40px;
  margin-left: 400px;
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
.pro {
  display: flex;
  flex-direction: column;
  margin-left: 200px;
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
  margin-left: 400px;
}
</style>
