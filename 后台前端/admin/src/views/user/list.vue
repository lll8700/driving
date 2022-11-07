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
    <el-dialog v-el-drag-dialog :visible.sync="excelDialogVisible" title="新增账户" @dragDialog="() => { excelDialogVisible =false }">
      <el-form
        ref="roleForm"
        :model="createInput"
        label-width="auto"
        class="demo-roleForm"
      >
        <el-form-item label="账户类型">
          <el-select ref="select" v-model="createInput.userTypeEnum" placeholder="账户类型">
            <el-option v-for="item in userTypeEnums" :key="item.key" :label="item.label" :value="item.key" />
          </el-select>
        </el-form-item>
        <el-form-item label="账户状态">
          <el-select ref="select" v-model="createInput.userStatusTypeEnum" placeholder="账户状态">
            <el-option v-for="item in userStatusTypeEnums" :key="item.key" :label="item.label" :value="item.key" />
          </el-select>
        </el-form-item>
        <el-form-item label="手机号">
          <el-input v-model="createInput.phone" maxlength="11" minlength="11" />
        </el-form-item>
        <el-form-item label="密码">
          <el-input v-model="createInput.password" maxlength="16" minlength="6" />
        </el-form-item>
        <el-form-item label="到期时间">
          <el-input v-model="createInput.endTime" type="date" />
        </el-form-item>
      </el-form>
      <el-button @click="()=>{excelDialogVisible = false}"> 取消</el-button>  <el-button @click="createUser()"> 确定</el-button>
    </el-dialog>

    <el-dialog v-el-drag-dialog :visible.sync="passwordDialogVisible" :title="`修改账户${updateInput.name}`" @dragDialog="() => { passwordDialogVisible =false }">
      <el-form
        ref="roleForm"
        :model="updateInput"
        label-width="auto"
        class="demo-roleForm"
      >
        <el-form-item label="账户类型">
          <el-select ref="select" v-model="updateInput.userTypeEnum" placeholder="账户类型">
            <el-option v-for="item in userTypeEnums" :key="item.key" :label="item.label" :value="item.key" />
          </el-select>
        </el-form-item>
        <el-form-item label="账户状态">
          <el-select ref="select" v-model="updateInput.userStatusTypeEnum" placeholder="账户状态">
            <el-option v-for="item in userStatusTypeEnums" :key="item.key" :label="item.label" :value="item.key" />
          </el-select>
        </el-form-item>
        <el-form-item label="密码">
          <el-input v-model="updateInput.password" maxlength="16" minlength="6" />
        </el-form-item>
      </el-form>
      <el-button @click="()=>{passwordDialogVisible = false}"> 取消</el-button>  <el-button @click="update()"> 确定</el-button>
    </el-dialog>

    <el-table :key="key" :data="tableData" border fit highlight-current-row style="width: 100%">
      <el-table-column prop="name" label="名称" width="130" />
      <el-table-column prop="phone" label="手机号" width="130" />
      <el-table-column prop="userTypeEnumName" label="类型" width="100" />
      <el-table-column prop="userStatusTypeEnumName" label="状态" width="100" />
      <el-table-column prop="strTimeName" label="开始" width="180" />
      <el-table-column prop="endTimeName" label="结束" width="180" />
      <el-table-column prop="createUserName" label="开户人" width="130" />
      <el-table-column label="操作" width="600">
        <template slot-scope="scope">
          <el-button @click="updateUser(scope.row)"> 修改信息</el-button>
          <el-button @click="updateJurs(scope.row)"> 修改权限</el-button>
          <el-button @click="deleteOptionItem(scope.row)"> 删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <template>
      <pagination
        :total="total"
        :page.sync="listQuery.page"
        :limit.sync="listQuery.limit"
        @pagination="getList"
      />
    </template>
  </div>

</template>

<script>
const defaultFormThead = ['apple', 'banana']
import { fetchList, create, update, deleteUser } from '@/api/user'
import { getEnum } from '@/api/config'
import { Message, MessageBox } from 'element-ui'
import Pagination from '@/components/Pagination'
export default {
  components: { Pagination },
  data() {
    return {
      tableData: [],
      excelDialogVisible: false, // excel弹窗
      passwordDialogVisible: false,
      key: 1, // table key
      formTheadOptions: ['apple', 'banana', 'orange'],
      checkboxVal: defaultFormThead, // checkboxVal
      formThead: defaultFormThead, // 默认表头 Default header
      listQuery: {
        userTypeEnum: undefined,
        userStatusTypeEnum: undefined,
        limit: 10
      },
      userTypeEnums: [], // 用户类型
      userStatusTypeEnums: [], // 状态
      total: 0,
      createInput: {
        userTypeEnum: 0, // 固定的
        userStatusTypeEnum: 20, // 固定的
        phone: '',
        password: '',
        endTime: undefined
      },
      updateInput: {
        id: undefined,
        password: undefined,
        name: undefined,
        userTypeEnum: undefined,
        userStatusTypeEnum: undefined
      }
    }
  },
  created() {
    this.initCreateInput()
    this.initType()
    this.baseList()
  },
  methods: {
    initCreateInput() {
      var date = new Date()
      var endTimeDate = new Date(date.setTime(date.getTime() + 3600 * 1000 * 24 * 365 * 3))
      var month = endTimeDate.getMonth() + 1
      var day = endTimeDate.getDate()

      if (month < 10) {
        month = '0' + month
      }
      if (day < 10) {
        day = '0' + day
      }
      var endDate = endTimeDate.getFullYear() + '-' + month + '-' + day
      this.createInput = {
        userTypeEnum: 0, // 固定的
        userStatusTypeEnum: 20, // 固定的
        phone: '',
        password: '',
        endTime: endDate
      }
      console.log(this.createInput)
    },
    initType() {
      getEnum('UserTypeEnum').then(res => {
        this.userTypeEnums = res
      })
      getEnum('UserStatusTypeEnum').then(res => {
        this.userStatusTypeEnums = res
      })
    },
    createUser() {
      var that = this
      if (!that.createInput.phone || that.createInput.phone.length !== 11 || that.createInput.password.length < 6) {
        Message({
          message: '请填写正确的手机号和不低于6位数密码',
          type: 'error',
          duration: 2 * 1000
        })
        return
      }
      create(that.createInput).then(res => {
        Message({
          message: '添加成功',
          type: 'success',
          duration: 2 * 1000
        })
        this.excelDialogVisible = false
        this.getList()
      })
    },
    updateUser(item) {
      this.updateInput.id = item.id
      this.updateInput.name = item.name
      this.updateInput.userTypeEnum = item.userTypeEnum
      this.updateInput.userStatusTypeEnum = item.userStatusTypeEnum
      this.passwordDialogVisible = true
    },
    update() {
      var that = this
      if (!that.updateInput.id) {
        Message({
          message: '请先选择修改的账户',
          type: 'error',
          duration: 2 * 1000
        })
        return
      }
      update(that.updateInput).then(res => {
        this.passwordDialogVisible = false
        this.baseList()
      })
    },
    updateJurs(item) {
      console.log(item)
      Message({
        message: '正在开发中',
        type: 'error',
        duration: 2 * 1000
      })
      return
    },
    deleteOptionItem(item) {
      MessageBox.confirm(`确定需要把${item.name}删除吗？`, '系统提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deleteUser(item).then(res => {
          if (res) {
            Message({
              message: '删除成功',
              type: 'success',
              duration: 2 * 1000
            })
            this.baseList()
          }
        })
      }).catch(() => {

      })
    },
    getList() {
      fetchList(this.listQuery).then(res => {
        this.tableData = res.items
        this.total = res.totalCount
      })
    },
    baseList() {
      this.listQuery.page = 1
      this.listQuery.limit = 10
      this.getList()
    }
  }
}
</script>

