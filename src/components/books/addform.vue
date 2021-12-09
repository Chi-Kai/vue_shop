<template>
  <div class="add">
    <el-dialog title="添加图书"
               :visible.sync="adddialogVisible"
               width="800px"
               class="addbox"
               @close="closeAdddialog">
      <el-steps :active="active"
                finish-status="success">
        <el-step title="基本信息" />
        <el-step title="分类信息" />
        <el-step title="入库信息" />
      </el-steps>
      <div v-if="active===0">
        <el-form ref="addFormRef"
                 class="add_form"
                 :model="addform"
                 :rules="addrules">
          <el-form-item prop="name">
            <el-input v-model="addform.name"
                      class="form_control"
                      prefix-icon=" el-icon-user-solid"
                      placeholder="书名" />
          </el-form-item>
          <el-form-item prop="password">
            <el-input v-model="addform.autor"
                      class="form_control"
                      prefix-icon=" el-icon-lock "
                      placeholder="作者" />
          </el-form-item>
          <el-form-item prop="tel">
            <el-input v-model="addform.isdn"
                      class="form_control"
                      prefix-icon=" el-icon-phone"
                      placeholder="ISDN" />
          </el-form-item>
          <el-form-item prop="email">
            <el-input v-model="addform.press"
                      class="form_control"
                      prefix-icon=" el-icon-message"
                      placeholder="出版社" />
          </el-form-item>
        </el-form>
      </div>
      <div v-if="active===1">
        <el-select v-model="addform.type"
                   placeholder="请选择分类">
          <el-option v-for="item in type"
                     :key="item.typeid"
                     :label="item.type"
                     :value="item.type" />
        </el-select>
        <el-input v-model="bookitr"
                  type="textarea"
                  :rows="10"
                  style="min-height: 33px;
                         margin-top: 20px;
                         width: 532px;
                         height: 212px;
                         margin-left: 90px;"
                  placeholder="请输入新书简介" />
      </div>
      <div v-if="active===2">
        <el-form ref="addFormRef"
                 class="add_form"
                 style="height: 300px;"
                 :model="addform"
                 :rules="addrules">
          <el-form-item prop="price">
            <el-input v-model="addform.price"
                      class="form_control"
                      prefix-icon=" el-icon-user-solid"
                      placeholder="价格" />
          </el-form-item>
          <el-form-item prop="sum">
            <el-input v-model="addform.sum"
                      class="form_control"
                      prefix-icon=" el-icon-lock "
                      placeholder="数目" />
          </el-form-item>
          <el-form-item prop="picture">
            <el-input v-model="addform.picture"
                      class="form_control"
                      prefix-icon=" el-icon-lock "
                      placeholder="图片URL" />
          </el-form-item>
        </el-form>
      </div>
      <span slot="footer"
            class="dialog-footer">
        <div v-if="active===2">
          <el-button type="primary"
                     @click="add">完成</el-button>
        </div>
        <div v-else>
          <el-button type="primary"
                     @click="next">下一步</el-button>
        </div>
        <el-button @click="close">取 消</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
export default {
  data () {
    return {
      // 控制步骤条
      active: 0,
      // 控制消息窗
      adddialogVisible: false,
      editdialogVisible: false,
      bookitr: '',
      // 添加的表单
      addform: {
        name: '',
        autor: '',
        type: '',
        press: '',
        isdn: '',
        price: '',
        sum: '',
        picture: ''

      },
      type: []
    }
  },
  // 与父组件联动
  created () {
    this.$on('open', function () {
      this.adddialogVisible = true
    })
  },
  mounted () {
    this.gettype()
  },
  methods: {
    async add () {
      const res = await this.$http.post('api/book/add', this.addform)
      console.log(res)
      await this.$http.post('api/book/additr', { bookid: res.data, introduce: this.bookitr })
      if (res.status !== 200) return this.$message.error('新增图书失败!')
      this.$message.success('新增图书成功！')
      this.adddialogVisible = false
      this.active = 0
      this.getdata()
    },
    async gettype () {
      const res = await this.$http.get('api/book/alltype')
      this.type = res.data
    },
    // 控制步骤条
    next () {
      if (this.active++ > 2) this.active = 2
    },
    // 关闭重置函数
    closeAdddialog () {
      this.$refs.addFormRef.resetFields()
      this.active = 0
    },
    close () {
      this.adddialogVisible = false
      this.active = 0
    },
    closeEditdialog () {
      this.$refs.editFormRef.resetFields()
    }
  }
}

</script>

<style lang="less" scoped>
.add_form {
  margin-top: 40px;
  padding-left: 160px;
  //padding: 30px 20px;
  //box-sizing: border-box;
  height: 300px; // 改变弹出框高度
  width: 400px;
}
.el-select {
  padding-left: 250px;
  padding-top: 40px;
}
.dialog-footer {
  display: flex;
  flex-direction: row-reverse;
}
.form_control {
  border: 1px solid #d4dcec;
  font-size: 20px;
}
</style>
