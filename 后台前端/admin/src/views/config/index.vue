<template>
  <div class="filter-container">
    <div class="filter-container">
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
  </div>
</template>

<script>
import carTbale from './carType'
import subjectTable from './subject'
export default {
  // eslint-disable-next-line vue/no-unused-components
  components: { carTbale, subjectTable },
  data() {
    return {
      carList: [], // 车型
      subLIst: [] // 科目
    }
  },
  created() {
  },
  methods: {
  }
}
</script>

<style>

</style>
