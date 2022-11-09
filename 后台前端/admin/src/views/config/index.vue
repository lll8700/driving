<template>
  <div class="filter-container">
    <div class="filter-container">
      <el-card label="11111">
        单价
        <el-form
          ref="roleForm"
          :model="createInput"
          label-width="auto"
          class="demo-roleForm"
        >
          <el-form-item label="账户单价">
            <el-input-number v-model=" configDto.createUserBasePrice" :disabled="!isUpdate" /> <el-button @click="editConfig">{{ !isUpdate? "修改":"保存" }}</el-button>
          </el-form-item>
        </el-form>
      </el-card>
      <el-card label="11111">
        车型
        <carTbale />
      </el-card>

    </div>
    <div class="filter-container">
      <el-card aria-label="">
        科目
        <subjectTable />
      </el-card>

    </div>
    <div class="filter-container">
      <el-card aria-label="">
        题库
        <practiceTypeTable />
      </el-card>

    </div>
  </div>
</template>

<script>
import carTbale from './carType'
import subjectTable from './subject'
import practiceTypeTable from './practiceType'

import { Message } from 'element-ui'
import { getFist, updateConfig } from '@/api/config'
export default {
  // eslint-disable-next-line vue/no-unused-components
  components: { carTbale, subjectTable, practiceTypeTable },
  data() {
    return {
      carList: [], // 车型
      subLIst: [], // 科目
      configDto: {},
      isUpdate: false
    }
  },
  created() {
    this.initConfig()
  },
  methods: {
    initConfig() {
      getFist().then(res => {
        this.configDto = res
      })
    },
    editConfig() {
      if (this.isUpdate) {
        // 这是保存
        updateConfig(this.configDto).then(res => {
          this.configDto = res
          Message({
            message: '保存成功',
            type: 'success',
            duration: 2 * 1000
          })
          this.isUpdate = false
        })
      } else {
        this.isUpdate = true
      }
    }
  }
}
</script>

<style>

</style>
