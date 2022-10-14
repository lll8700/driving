<template>
  <div>

    <div class="filter-container">
      <el-checkbox-group>
        <el-button @click="()=> { subDialogVisible = true, createInput = {}}"> 新增</el-button>
      </el-checkbox-group>
    </div>
    <el-dialog v-el-drag-dialog :visible.sync="subDialogVisible" title="新增" @dragDialog="() => { subDialogVisible =false }">
      <el-form
        ref="roleForm"
        :model="createInput"
        label-width="auto"
        class="demo-roleForm"
      >

        <el-form-item label="科目名称">
          <el-input v-model="createInput.name" />
        </el-form-item></el-form>
      <el-button @click="()=>{subDialogVisible = false}"> 取消</el-button>  <el-button @click="createSub()"> 确定</el-button>
    </el-dialog>
    <el-table :data="subLIst" border fit highlight-current-row style="width: 100%">
      <el-table-column prop="name" label="科目名称" width="180" />
      <el-table-column label="操作" width="180">
        <template slot-scope="scope">
          <el-button @click="update(scope.row)"> 修改</el-button>
          <el-button @click="deleteItem(scope.row)"> 删除</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getSubjectTypeList, createSub, deleteSub } from '@/api/config'
import { Message } from 'element-ui'
export default {
  data() {
    return {
      subDialogVisible: false,
      subLIst: [], // 科目
      createInput: {}
    }
  },
  created() {
    this.initType()
  },
  methods: {
    initType() {
      getSubjectTypeList().then(res => {
        this.subLIst = res.items
      })
    },
    createSub() {
      var that = this
      if (!that.createInput.name) {
        Message({
          message: '请填写科目名称',
          type: 'error',
          duration: 2 * 1000
        })
        return
      }
      createSub(this.createInput).then(res => {
        Message({
          message: '操作成功',
          type: 'success',
          duration: 2 * 1000
        })
        this.subDialogVisible = false
        this.initType()
      })
    },
    update(item) {
      this.createInput = item
      this.subDialogVisible = true
    },
    deleteItem(item) {
      deleteSub(item).then(() => {
        Message({
          message: '操作成功',
          type: 'success',
          duration: 2 * 1000
        })
        this.initType()
      })
    }
  }
}
</script>

  <style>

  </style>

