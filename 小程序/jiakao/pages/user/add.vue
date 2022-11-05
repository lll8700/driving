<template>
	<view class="addContain">
		<!-- 新增账号 -->
		<uni-section title="账号信息" type="line">
		<uni-card title="新增账号">
			<form  class="forms">
					<view class="uni-forms-item uni-column">
						<view class="uni-forms-item uni-column list">
							<input class="c" v-model="accout.phoneNumber" maxlength="11" placeholder="请输入手机号" />
						</view>
						<view class="uni-forms-item uni-column list">
							<input class="c" v-model="accout.password" placeholder="请输入密码" />
						</view>
					</view> 
				
			
				</form>
		</uni-card>
		</uni-section>
		<uni-section title="权限信息" type="line">
			<uni-card title="选择考试车型" class="btn">
				<checkbox-group @change="updateCar">
					<checkbox v-for="(item,index) in typeArrs" :value="item.id" style="margin-left: 10px;">{{ item.name }}</checkbox>
				</checkbox-group>
				
			</uni-card>
			<uni-card title="选择考试科目" class="btn">
				<checkbox-group @change="updateSub">
					<checkbox v-for="(item,index) in queTypeArrs" :value="item.id" style="margin-left: 10px;">{{ item.name }}</checkbox>
				</checkbox-group>
				
			</uni-card>
			
		</uni-section>
		<uni-section title="会员信息" type="line">
		<uni-card :title="'销售价(元)   最低:' + config.createUserBasePrice">
			<input class="c" type="number" v-model="accout.price" placeholder="开户金额" />
		</uni-card>
		</uni-section>
		<uni-section title="备注" type="line">
			<uni-card >
				<textarea  placeholder="请输入备注" id="" cols="30" rows="10"></textarea>
			</uni-card>
			
			<!-- <input type="textarea" name="" id=""> -->
		</uni-section>
		<view class="addbtn">
			<button  type="primary" class="mini-btn cbtn "  size="default"
				plain="true" @click="addUser">确认开通</button>
		</view>
		
	</view>
</template>

<script>
	export default {
		data() {
			return {
				accout:{
					phoneNumber:'',
					password:'',
					carsIds: [],
					subjectTypes: [],
					price: 0,
				},
				typeArrs: [],
				queTypeArrs: [],
				config: {}
			}
		},
		created() {
			this.initData()
		},
		methods: {
			initData() {
				var that = this
				that.$http(that.$api.CarType.list, "POST", {
					Sorting: "CreationTime"
				}).then(res => {
					that.typeArrs = res.data.items;
				})
				that.$http(that.$api.SubjectType.list, 'POST', {
					Sorting: "CreationTime"
				}).then(res => {
					that.queTypeArrs = res.data.items;
				})
				that.$http(that.$api.config.getfist, 'get').then(res => {
					that.config = res.data;
					console.log(that.config)
					that.accout.price = that.config.createUserBasePrice
				})
			},
			updateCar(e) {
				this.accout.carsIds = e.detail.value
			},
			updateSub(e) {
				this.accout.subjectTypes = e.detail.value
			},
			addUser() {
				var that = this
				if(!that.accout.phoneNumber || that.accout.phoneNumber.length != 11) {
					uni.showToast({
						icon:'error',
						title:'请输入正确的手机号'
					})
					return
				}
				if(!that.accout.password || that.accout.password.password < 6) {
					uni.showToast({
						icon:'error',
						title:'请输入正确的密码'
					})
					return
				}
				if(!that.accout.carsIds || that.accout.carsIds.length === 0 || !that.accout.subjectTypes || that.accout.subjectTypes.length === 0 ) {
					uni.showToast({
						icon:'error',
						title:'请配置权限'
					})
					return
				}
				if(!that.accout.price || parseInt(that.accout.price) < that.config.createUserBasePrice ) {
					uni.showToast({
						icon:'error',
						title:'输入的价格不能低于设定价'
					})
					return
				}
				
				that.$http(that.$api.login.create, 'post', that.accout).then(res => {
					uni.showToast({
						icon:'success',
						title:'添加成功'
					})
					setTimeout(()=>{
						uni.switchTab({
							url: '/pages/index/index'
						});
					},500)
				})
			}
		}
	}
</script>

<style lang="less" scoped>
	.addContain{
		padding:10px;
	}
	.list{
		padding: 10px auto;
		line-height: 1.1rem;
		margin:20px auto;
	}
	.addbtn{
		width: 80%;
		margin:10px 10%;
		
	}

</style>
