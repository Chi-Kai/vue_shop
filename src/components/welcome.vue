<template>
  <div class="welcoem">
    <el-row :gutter="20"
            class="gp">
      <el-col :span="4">
        <el-tag effect="dark"
                type="success">
          图书总数: {{ booknum }}
        </el-tag>
      </el-col>
      <el-col :span="4">
        <el-tag effect="dark"
                size="medium">
          类型总数: {{ typenum }}
        </el-tag>
      </el-col>
      <el-col :span="4">
        <el-tag effect="dark"
                type="info">
          用户总数: {{ usernum }}
        </el-tag>
      </el-col>
      <el-col :span="4">
        <el-tag effect="dark"
                type="warning">
          管理员总数: {{ adminnum }}
        </el-tag>
      </el-col>
      <el-col :span="4">
        <el-tag effect="dark"
                type="danger">
          订单总数: {{ ordernum }}
        </el-tag>
      </el-col>
    </el-row>
    <div class="show">
      <div id="datashow" />
      <div id="ordershow" />
    </div>
  </div>
</template>

<script>
export default {
  data () {
    return {
      booknum: '',
      typenum: '',
      usernum: '',
      adminnum: '',
      ordernum: ''

    }
  },
  mounted () {
    this.getnum()
    this.echartsInit()
  },
  methods: {
    async echartsInit () {
      // 柱形图
      // 因为初始化echarts 的时候，需要指定的容器 id='main'

      var res = await this.$http.get('api/book/groupnum')
      var order = await this.$http.get('api/order/groupnum')
      console.log(order)
      var booktype = []
      var booknums = []
      for (var i = 0; i < res.data.length; i++) {
        booktype.push(res.data[i].key)
        booknums.push(res.data[i].count)
      }

      var orderdate = []
      var ordernum = []
      for (i = 0; i < order.data.length; i++) {
        orderdate.push(order.data[i].key)
        ordernum.push(order.data[i].count)
      }
      console.log(orderdate)

      this.$echarts.init(document.getElementById('datashow')).setOption({
        title: {
          text: '分类图书'
        },
        xAxis: {
          data: booktype
        },
        yAxis: {},
        series: [
          {
            type: 'bar',
            data: booknums,
            barWidth: '20%'
          }
        ]
      })

      this.$echarts.init(document.getElementById('ordershow')).setOption({
        title: {
          text: '订单统计'
        },
        xAxis: {
          type: 'category',
          data: orderdate
        },
        yAxis: { type: 'value' },
        series: [
          {
            type: 'line',
            data: ordernum
          }
        ]
      })
    },
    async getnum () {
      var b = await this.$http.get('api/book/booknum')
      var t = await this.$http.get('api/book/alltype')
      var a = await this.$http.get('api/user/adminnum')
      var u = await this.$http.get('api/user/usernum')
      var o = await this.$http.get('api/order/num')

      this.booknum = b.data[0].num
      // console.log(b)
      this.adminnum = a.data[0].num
      this.usernum = u.data[0].num
      // console.log(a)
      // console.log(u)
      // console.log(t.data.length)
      this.typenum = t.data.length

      this.ordernum = o.data[0].num
      // console.log(o)
    }
  }

}
</script>

<style>
.el-tag {
  height: 60px !important;
  line-height: 60px !important;
  font-size: x-large !important;
}
#datashow {
  width: 40%;
  height: 400px;
  background-color: rgba(22, 127, 246, 0.11);
  margin-left: 35px;
  margin-top: 80px;
  box-shadow: 1px 1px 20px rgb(0 0 0 / 5%);
}
#ordershow {
  width: 40%;
  height: 400px;
  background-color: rgba(22, 127, 246, 0.11);
  margin-left: 400px;
  margin-top: 80px;
  box-shadow: 1px 1px 20px rgb(0 0 0 / 5%);
}
.show {
  display: flex;
}
.gp {
  margin-left: 200px !important;
  //margin-right: -10px;
  margin-top: 50px;
}
</style>
