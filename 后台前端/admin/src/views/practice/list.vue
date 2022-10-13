<template>
  <div class="app-container">
    <div class="filter-container">
      <el-checkbox-group>
        <el-select ref="select" v-model="listQuery.carTypeId" placeholder="查询车型">
          <el-option v-for="item in carList" :key="item.id" :label="item.name" :value="item.id" />
        </el-select>
        <el-select ref="select" v-model="listQuery.subjectTypeId" placeholder="查询科目">
          <el-option v-for="item in subLIst" :key="item.id" :label="item.name" :value="item.id" />
        </el-select>
      </el-checkbox-group>
    </div>
    <div class="filter-container">
      <el-button @click="getList"> 查询</el-button>
      <el-button @click="showExcel"> 导入题库</el-button>
      <el-button @click="()=>{imageDialogVisible =true}"> 导入图片</el-button>
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
    <el-dialog v-el-drag-dialog :visible.sync="imageDialogVisible" title="导入图片Zip" @dragDialog="()=>{imageDialogVisible =false}">
      <upload-excel-component :before-upload="beforeUploadImage" />
      <el-button @click="()=>{imageDialogVisible =false}"> 取消</el-button>  <el-button @click="upload"> 确定</el-button>
    </el-dialog>
    <el-table :key="key" :data="tableData" border fit highlight-current-row style="width: 100%">
      <el-table-column prop="title" label="标题" width="180" />
      <el-table-column prop="carType.name" label="车型" width="180" />
      <el-table-column prop="carType.subname" label="执照" width="180" />
      <el-table-column prop="choiceTyopeEnmName" label="类型" width="180" />
      <el-table-column label="选项" width="180">
        <template slot-scope="scope">
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
import { fetchList, exlcel } from '@/api/practice'
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
      imageZip: undefined,
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
    this.initType()
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
      const isLt1M = file.size / 1024 / 1024 < 10

      if (isLt1M) {
        this.inputExcel.file = file
        console.log(file)
        return true
      }

      this.$message({
        message: '上传的文件不可高于10M',
        type: 'warning'
      })
      return false
    },
    beforeUploadImage(file) {
      const isLt1M = file.size / 1024 / 1024 < 10

      if (isLt1M) {
        this.imageZip = file
        return true
      }

      this.$message({
        message: '上传的文件不可高于10M',
        type: 'warning'
      })
      return false
    },
    uploadIamge() {
      var formData = new FormData()// 新建表单对象
      formData.append('file', this.imageZip)// 把文件对象添加到表单对象里
      formData.append('filename', this.imageZip.name)// 把文件名称添加到表单对象里
      exlcel(formData).then(count => {
        if (count > 0) {
          this.$message({
            message: '成功！',
            type: 'success'
          })
          this.imageDialogVisible = false
        }
      })
    },
    upload() {
      console.log(this.inputExcel)
      exlcel(this.inputExcel).then(count => {
        if (count > 0) {
          this.$message({
            message: '成功！',
            type: 'success'
          })
          this.handleDrag()
          this.getList()
        }
      })
    },
    handleSuccess({ results, header }) { // 文件上传后成功回调
      this.inputExcel.ResultsLists = []
      // 图片: "11.jpg,22.png"
      // 序号: 1
      // 正确答案: "A "
      // 答题技巧一: "实习期禁止牵引挂车。"
      // 答题技巧二: "题干说实习期不得牵引挂车，所以选对。"
      // 选项一: "A 正确"
      // 选项三: "C"
      // 选项二: "B 错误"
      // 选项四: "D"
      // 题型: "判断/新规题"
      // 题目: "机动车驾驶人在实习期内驾驶机动车不得牵引挂车。"
      // 题目解析: "《机动车驾驶证申领和使用规定》规定：机动车驾驶人在实习期内不得驾
      for (let index = 0; index < results.length; index++) {
        var item = {
          Title: results[index].题目,
          Skill: results[index].答题技巧一,
          SkillLast: results[index].答题技巧二,
          Images: results[index].图片,
          TypeName: results[index].题型,
          Introduce: results[index].题目解析
        }
        item.Options = []
        var optionIndex = 1
        if (results[index].选项一 && results[index].选项一.trim().length > 1) {
          item.Options.push({
            index: optionIndex++,
            Title: results[index].选项一,
            IsCorrect: results[index].选项一.toString().trim().startsWith(results[index].正确答案.toString().trim())
          })
        }
        if (results[index].选项二 && results[index].选项二.trim().length > 1) {
          item.Options.push({
            index: optionIndex++,
            Title: results[index].选项二,
            IsCorrect: results[index].选项二.toString().trim().startsWith(results[index].正确答案.toString().trim())
          })
        }
        if (results[index].选项三 && results[index].选项三.trim().length > 1) {
          item.Options.push({
            index: optionIndex++,
            Title: results[index].选项三,
            IsCorrect: results[index].选项三.toString().trim().startsWith(results[index].正确答案.toString().trim())
          })
        }
        if (results[index].选项四 && results[index].选项四.trim().length > 1) {
          item.Options.push({
            index: optionIndex++,
            Title: results[index].选项四,
            IsCorrect: results[index].选项四.toString().trim().startsWith(results[index].正确答案.toString().trim())
          })
        }

        this.inputExcel.ResultsLists.push(item)
      }
      console.log(this.inputExcel)
    }
  }
}
</script>

