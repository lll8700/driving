<template>
  <div class="app-container">
    <div class="filter-container">
      <el-checkbox-group>
        <el-input v-model="listQuery.name" placeholder="标题关键字" width="100" />
        <el-select ref="select" v-model="listQuery.carTypeId" placeholder="查询车型">
          <el-option v-for="item in carList" :key="item.id" :label="item.name" :value="item.id" />
        </el-select>
        <el-select ref="select" v-model="listQuery.subjectTypeId" placeholder="查询科目">
          <el-option v-for="item in subLIst" :key="item.id" :label="item.name" :value="item.id" />
        </el-select>
        <el-select ref="select" v-model="listQuery.choiceTyope" placeholder="题目类型">
          <el-option v-for="item in choiceTyopeList" :key="item.key" :label="item.label" :value="item.key" />
        </el-select>
      </el-checkbox-group>
    </div>
    <div class="filter-container">
      <el-button @click="getSubList"> 查询</el-button>
      <el-button @click="showExcel"> 导入题库</el-button>
      <el-button @click="() => { imageDialogVisible = true }"> 导入图片</el-button>
    </div>
    <el-dialog v-el-drag-dialog :visible.sync="excelDialogVisible" title="导入题型" @dragDialog="handleDrag">
      <el-select ref="select" v-model="inputExcel.carTypeId" placeholder="请选择车型">
        <el-option v-for="item in carList" :key="item.id" :label="item.name" :value="item.id" />
      </el-select>
      <el-select ref="select" v-model="inputExcel.subjectTypeId" placeholder="请选择科目">
        <el-option v-for="item in subLIst" :key="item.id" :label="item.name" :value="item.id" />
      </el-select>
      <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
      <el-button @click="handleDrag"> 取消</el-button>
      <el-button @click="upload"> 确定</el-button>
    </el-dialog>
    <el-dialog
      v-el-drag-dialog
      :visible.sync="imageDialogVisible"
      title="导入图片Zip"
      @dragDialog="() => { imageDialogVisible = false }"
    >
      <upload-zip-component :before-upload="beforeUploadImage" />
      <el-button @click="() => { imageDialogVisible = false }"> 取消</el-button>
      <el-button @click="uploadIamge"> 确定</el-button>
    </el-dialog>
    <el-table :key="key" :data="tableData" border fit highlight-current-row style="width: 100%">
      <el-table-column prop="title" label="标题" width="180" />
      <el-table-column prop="carType.name" label="车型" width="180" />
      <el-table-column prop="carType.subname" label="执照" width="180" />
      <el-table-column prop="choiceTyopeEnmName" label="类型" width="180" />
      <el-table-column label="选项" width="180">
        <template slot-scope="scope">
          <div v-for="(item, index) in scope.row.options" :key="index">
            {{ item.isCorrect ? "正确" : "错误" }} - {{ item.title }}
          </div>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="180">
        <template slot-scope="scope">
          <el-button @click="update(scope.row)"> 修改</el-button>
          <el-button @click="deleteItem(scope.row)"> 删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-dialog
      v-el-drag-dialog
      :visible.sync="createDialogVisible"
      :title="creatOrUpdateTitle"
      width="800px"
      @dragDialog="() => { createDialogVisible = false }"
    >
      <el-form ref="roleForm" :model="createInput" label-width="auto" class="demo-roleForm">
        <el-form-item label="标题">
          <el-input v-model="createInput.title" />
        </el-form-item>
        <el-form-item label="题库车型">
          <el-select ref="select" v-model="createInput.carTypeId" placeholder="车型">
            <el-option v-for="item in carList" :key="item.id" :label="item.name" :value="item.id" />
          </el-select>
        </el-form-item>
        <el-form-item label="题库科目">
          <el-select ref="select" v-model="createInput.subjectTypeId" placeholder="科目">
            <el-option v-for="item in subLIst" :key="item.id" :label="item.name" :value="item.id" />
          </el-select>
        </el-form-item>
        <el-form-item label="题库类型">
          <el-select ref="select" v-model="createInput.choiceTyope" placeholder="题目类型">
            <el-option v-for="item in choiceTyopeList" :key="item.key" :label="item.label" :value="item.key" />
          </el-select>
        </el-form-item>
        <el-form-item label="答题技巧1">
          <el-input v-model="createInput.skill" />
        </el-form-item>
        <el-form-item label="答题技巧2">
          <el-input v-model="createInput.skillLast" />
        </el-form-item>
        <el-form-item label="试题详解">
          <el-input v-model="createInput.introduce" />
        </el-form-item>
        <el-form-item label="图片集(,分隔)">
          <el-input v-model="createInput.imageSrc" />
        </el-form-item>
        <el-form-item label="">
          <button @click="addoptions">添加选项</button>
        </el-form-item>
        <el-form-item label="选项">
          <el-table
            :key="createdTableKey"
            :data="createInput.options"
            border
            fit
            highlight-current-row
            style="width: 100%"
          >
            <el-table-column prop="index" label="位置" width="50" />

            <el-table-column prop="title" label="选项" width="460">
              <template slot-scope="scope">
                <el-input v-model="scope.row.title" />
              </template>
            </el-table-column>
            <el-table-column label="是否正确" width="50">
              <template slot-scope="scope">
                <el-checkbox :checked="scope.row.isCorrect" @change="updateOptionsItem($event, scope.row)" />
              </template>
            </el-table-column>
            <el-table-column label="操作" width="100">
              <template slot-scope="scope">
                <el-button @click="deleteOptionItem(scope.row)"> 删除</el-button>
              </template>
            </el-table-column>
          </el-table>

          <!-- <el-row v-for="(item, index) in createInput.options" :key="index">
            <el-checkbox :checked="item.isCorrect" style="display:inline" @change="updateOptionsItem(item)" />
            <el-input v-model="item.title" style="display:inline" />
          </el-row> -->
        </el-form-item>
      </el-form>
      <el-button @click="() => { createDialogVisible = false }"> 取消</el-button>
      <el-button @click="saveInput"> 保存</el-button>
    </el-dialog>
    <template>
      <pagination :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />
    </template>
  </div>
</template>

<script>
const defaultFormThead = ['apple', 'banana']
import { fetchList, exlcel, uploadZip, deleteItem, create } from '@/api/practice'
import { getCarTypeList, getSubjectTypeList, getEnum } from '@/api/config'
import UploadExcelComponent from '@/components/UploadExcel/index.vue'
import UploadZipComponent from '@/components/UploadExcel/zip.vue'
import Pagination from '@/components/Pagination'
import { Message } from 'element-ui'
export default {
  components: { UploadExcelComponent, Pagination, UploadZipComponent },
  data() {
    return {
      tableData: [],
      excelDialogVisible: false, // excel弹窗
      imageDialogVisible: false,
      createDialogVisible: false,
      key: 1, // table key
      createdTableKey: 2,
      formTheadOptions: ['apple', 'banana', 'orange'],
      checkboxVal: defaultFormThead, // checkboxVal
      formThead: defaultFormThead, // 默认表头 Default header
      total: 0,
      listQuery: {
        limit: 10
      },
      carList: [], // 车型
      subLIst: [], // 科目
      choiceTyopeList: [], // 选项
      imageZip: undefined,
      createInput: {},
      creatOrUpdateTitle: '新增题库',
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
      getEnum('ChoiceTyope').then(res => {
        this.choiceTyopeList = res
      })
    },
    deleteOptionItem(item) {
      this.createInput.options = this.createInput.options.filter(res => {
        return res !== item
      })
    },
    updateOptionsItem(val, item) {
      item.isCorrect = val
      console.log(this.createInput.options)
    },
    update(item) {
      this.createInput = item
      this.createDialogVisible = true
      if (item.practiceImages && item.practiceImages.length > 0) {
        for (let index = 0; index < item.practiceImages.length; index++) {
          this.createInput.imageSrc += item.practiceImages[index].url
        }
      }
      this.creatOrUpdateTitle = '修改题库'
    },
    getSubList() {
      this.listQuery.limit = 10
      this.listQuery.page = 1
      this.getList()
    },
    addoptions() {
      if (!this.createInput.options) {
        this.createInput.options = []
      }
      var indexItem = 0
      for (let index = 0; index < this.createInput.options.length; index++) {
        if (indexItem < this.createInput.options[index].index) {
          indexItem = this.createInput.options[index].index
        }
      }
      this.createInput.options.push({
        title: '',
        isCorrect: false,
        index: indexItem
      })
    },
    getList() {
      fetchList(this.listQuery).then(res => {
        this.tableData = res.items
        this.total = res.totalCount
      })
    },
    saveInput() {
      var that = this
      if (!that.createInput.title || that.createInput.title.length < 2) {
        that.messge('请输入选项标题')
        return
      }
      if (!that.createInput.carTypeId) {
        that.messge('请选择车型')
        return
      }
      if (!that.createInput.subjectTypeId) {
        that.messge('请选择科目')
        return
      }
      if (!that.createInput.choiceTyope) {
        that.messge('请选择类型')
        return
      }
      if (!that.createInput.options || that.createInput.options.length < 2) {
        that.messge('请增加选项')
        return
      }
      create(that.createInput).then(() => {
        Message({
          message: '操作成功',
          type: 'success',
          duration: 2 * 1000
        })
        this.getSubList()
        this.createDialogVisible = false
      })
    },
    messge(title, isSuccess) {
      Message({
        message: title,
        type: isSuccess ? 'success' : 'error',
        duration: 2 * 1000
      })
    },
    deleteItem(item) {
      deleteItem(item).then(() => {
        Message({
          message: '操作成功',
          type: 'success',
          duration: 2 * 1000
        })
        this.getList()
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
        console.log(this.imageZip)
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
      uploadZip(formData).then(count => {
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

