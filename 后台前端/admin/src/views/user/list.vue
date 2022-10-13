<template>
  <div class="app-container">
    <div class="filter-container">
      <el-checkbox-group>
        <el-select ref="select" v-model="listQuery.userTypeEnum" placeholder="查询账户类型">
          <el-option label="全部" value="" />
          <el-option v-for="item in userTypeEnums" :key="item.key" :label="item.label" :value="item.key" />
        </el-select>
      </el-checkbox-group>
    </div>
    <div class="filter-container">
      <el-checkbox-group>
        <el-button @click="getList()"> 查询</el-button>
        <el-button @click="()=> { excelDialogVisible = true }"> 新增</el-button>
      </el-checkbox-group>
    </div>
    <el-dialog v-el-drag-dialog :visible.sync="excelDialogVisible" title="导入题型" @dragDialog="handleDrag">
      <el-button @click="()=>{excelDialogVisible = false}"> 取消</el-button>  <el-button @click="()=>{}"> 确定</el-button>
    </el-dialog>
    <el-table :key="key" :data="tableData" border fit highlight-current-row style="width: 100%">
      <el-table-column prop="name" label="名称" width="180" />
      <el-table-column prop="phone" label="手机号" width="180" />
      <el-table-column prop="userTypeEnumName" label="类型" width="180" />
      <el-table-column prop="userStatusTypeEnumName" label="状态" width="180" />
      <el-table-column prop="strTimeName" label="开始" width="180" />
      <el-table-column prop="endTimeName" label="结束" width="180" />
    </el-table>
  </div>
</template>

<script>
const defaultFormThead = ['apple', 'banana']
import { fetchList } from '@/api/user'
import { getEnum } from '@/api/config'
export default {
  data() {
    return {
      tableData: [],
      excelDialogVisible: false, // excel弹窗
      key: 1, // table key
      formTheadOptions: ['apple', 'banana', 'orange'],
      checkboxVal: defaultFormThead, // checkboxVal
      formThead: defaultFormThead, // 默认表头 Default header
      listQuery: {
        userTypeEnum: undefined,
        userStatusTypeEnum: undefined
      },
      userTypeEnums: [], // 用户类型
      userStatusTypeEnums: [], // 状态
      createInput: {}
    }
  },
  created() {
    this.initType()
    this.getList()
  },
  methods: {
    initType() {
      getEnum('UserTypeEnum').then(res => {
        this.userTypeEnums = res
      })
    },
    getList() {
      fetchList(this.listQuery).then(res => {
        this.tableData = res.items
      })
    }
  }
}
</script>

