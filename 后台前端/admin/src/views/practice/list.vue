<template>
  <div class="app-container">
    <div class="filter-container">
      <el-checkbox-group v-model="checkboxVal">
        <!-- <el-checkbox label="apple">
          apple
        </el-checkbox>
        <el-checkbox label="banana">
          banana
        </el-checkbox>
        <el-checkbox label="orange">
          orange
        </el-checkbox> -->
        <el-button @click="getList"> 新增</el-button>
        <el-button @click="showExcel"> 导入</el-button>
      </el-checkbox-group>
    </div>
    <el-dialog v-el-drag-dialog :visible.sync="excelDialogVisible" title="导入题型" @dragDialog="handleDrag">
      <el-select ref="select" v-model="inputExcel.carTypeId" placeholder="请选择车型">
        <el-option v-for="item in carList" :key="item.id" :label="item.name" :value="item.id" />
      </el-select>
      <el-select ref="select" v-model="inputExcel.subjectTypeId" placeholder="请选择科目">
        <el-option v-for="item in subLIst" :key="item.id" :label="item.name" :value="item.id" />
      </el-select>
      <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
      <el-button @click="handleDrag"> 取消</el-button>  <el-button @click="upload"> 确定</el-button>
    </el-dialog>
    <el-table :key="key" :data="tableData" border fit highlight-current-row style="width: 100%">
      <el-table-column prop="title" label="标题" width="180" />
      <el-table-column prop="carType.name" label="车型" width="180" />
      <el-table-column prop="carType.subname" label="执照" width="180" />
      <el-table-column prop="choiceTyopeEnmName" label="类型" width="180" />
      <el-table-column>
        <template slot-scope="scope" label="选项">
          <div v-for="(item, index) in scope.row.options" :key="index">
            {{ item.isCorrect ? "正确": "错误" }} - {{ item.title }}
          </div>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
const defaultFormThead = ['apple', 'banana']
import { fetchList } from '@/api/practice'
import { getCarTypeList, getSubjectTypeList } from '@/api/config'
import UploadExcelComponent from '@/components/UploadExcel/index.vue'
export default {
  components: { UploadExcelComponent },
  data() {
    return {
      tableData: [],
      excelDialogVisible: false, // excel弹窗
      key: 1, // table key
      formTheadOptions: ['apple', 'banana', 'orange'],
      checkboxVal: defaultFormThead, // checkboxVal
      formThead: defaultFormThead, // 默认表头 Default header
      listQuery: {
      },
      carList: [], // 车型
      subLIst: [], // 科目
      inputExcel: {
        subjectTypeId: undefined,
        carTypeId: undefined,
        file: undefined
      }
    }
  },
  watch: {
    checkboxVal(valArr) {
      this.formThead = this.formTheadOptions.filter(i => valArr.indexOf(i) >= 0)
      this.key = this.key + 1// 为了保证table 每次都会重渲 In order to ensure the table will be re-rendered each time
    }
  },
  created() {
    this.getList()
  },
  methods: {
    initType() {
      getCarTypeList().then(res => {
        this.carList = res.items
      })
      getSubjectTypeList().then(res => {
        this.subLIst = res.items
      })
    },
    getList() {
      fetchList(this.listQuery).then(res => {
        this.tableData = res.items
      })
    },
    showExcel() {
      if (this.carList.length === 0) {
        this.initType()
      }
      this.excelDialogVisible = true
    },
    handleDrag() {
      this.excelDialogVisible = false
    },
    beforeUpload(file) {
      const isLt1M = file.size / 1024 / 1024 < 1

      if (isLt1M) {
        this.inputExcel.file = file
        console.log(file)
        return true
      }

      this.$message({
        message: 'Please do not upload files larger than 1m in size.',
        type: 'warning'
      })
      return false
    },
    upload() {
      console.log(this.inputExcel)
    },
    handleSuccess({ results, header }) { // 文件上传后成功回调
      console.log(results)
      console.log(header)
    }
  }
}
</script>

