<template>
	<view>

		<uni-list v-for="(item, index) in list" :key="index">
		<uni-list-item clickable @click="show(item)" :title="`账号：${item.name}`" :note="item.userTypeEnumName" :rightText="item.userStatusTypeEnumName" />
		<!-- 	<view class="uni-list-cell" hover-class="uni-list-cell-hover" v-for="(value, key) in list" :key="key" @click="goDetail(value)">
			    
				<view class="uni-media-list">
			        <view class="uni-media-list-body">
			            <view class="uni-media-list-text-top">{{ value.userTypeEnumName }}</view>
			                <view class="uni-media-list-text-bottom">
			                    <text>{{ value.name }}</text>
			                    <text style="margin-left: 30rpx;margin-top: 6rpx;">{{ value.userStatusTypeEnumName }}</text>
			                </view>
			        </view>
			    </view>
			</view> -->
		</uni-list>
		<view>
			<!-- 提示窗示例 -->
			<uni-popup ref="alertDialog" type="dialog">
				<uni-popup-dialog cancelText="关闭" confirmText="修改权限"  :title="`您想对<${dataItem.name}>账户做什么`"  :before-close="true" @confirm="jurisdiction" 
					@close="$refs.alertDialog.close()">
					<view class="dialogContent" >
						
					</view>
					</uni-popup-dialog>
			</uni-popup>
			
			<!-- 权限弹窗 -->
			<uni-popup ref="jurisdiction" type="dialog">
				<uni-popup-dialog cancelText="关闭" confirmText="确认修改"  :title="`对<${dataItem.name}>修改权限`"  :before-close="true" @confirm="save" 
					@close="$refs.jurisdiction.close()">
					<view class="dialogContent" >
						<uni-section title="权限信息" type="line">
							<uni-card title="选择考试车型" class="btn">
								<checkbox-group @change="updateCar">
									<checkbox v-for="(item,index) in carArrs" :value="item.id" style="margin-left: 10px;" :checked="isCarChecked(item)" >{{ item.name }}</checkbox>
								</checkbox-group>
								
							</uni-card>
							<uni-card title="选择考试科目" class="btn">
								<checkbox-group @change="updateSub">
									<checkbox v-for="(item,index) in subjectArrs" :value="item.id" style="margin-left: 10px;" :checked="isSubChecked(item)">{{ item.name }}</checkbox>
								</checkbox-group>
								
							</uni-card>
							
						</uni-section>
					</view>
					</uni-popup-dialog>
			</uni-popup>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				input: {
					isSelfCreate: true
				},
				list: [],
				totalCount: 0,
				dataItem: {}, // 点击的账户
				carArrs: [],
				subjectArrs: [],
				jurList: [],
				jurInput: {
					cars: [],
					subjectTypes: [],
					userId: undefined
				}
			}
		},
		onShow() {
			this.initData()
		},
		methods: {
			initData() {
				var that = this
				that.$http(that.$api.user.list, 'post', that.input).then(res => {
					this.list = res.data.items,
					this.totalCount = res.data.totalCount
				})
			},
			initUser() {
				var that = this
				that.$http(that.$api.CarType.list, "POST", {
					Sorting: "CreationTime"
				}).then(res => {
					that.carArrs = res.data.items;
				})
				that.$http(that.$api.SubjectType.list, 'POST', {
					Sorting: "CreationTime"
				}).then(res => {
					that.subjectArrs = res.data.items;
				})
			},
			show(item) {
				this.dataItem = item;
				this.$refs.alertDialog.open()
			},
			todetail(detailId){
				uni.navigateTo({
					url: '/pages/errlist/detail?detailId='+detailId
				});
			},
			jurisdiction() {
				var that = this
				if(!this.subjectArrs || this.subjectArrs.length === 0) {
					this.initUser()
				}
				this.jurInput.cars = []
				this.jurInput.subjectTypes = []
				that.$http(that.$api.config.jurisdiction + '?userId=' + that.dataItem.id, 'get').then(res => {
					this.jurList = res.data;
					var carsList = this.jurList.filter(s => s.user_JurisdictionTypeEnum === 10);
					for(var i = 0;i< carsList.length; i++) {
						this.jurInput.cars.push(carsList[i].tableId)
					}
					var subs = this.jurList.filter(s => s.user_JurisdictionTypeEnum === 20);
					for(var i = 0;i< subs.length; i++) {
						this.jurInput.subjectTypes.push(subs[i].tableId)
					}
				})
				that.jurInput.userId = this.dataItem.id
				this.$refs.jurisdiction.open()
				this.$refs.alertDialog.close()
			},
			updateCar(e) {
				this.jurInput.cars = e.detail.value
			},
			updateSub(e) {
				this.jurInput.subjectTypes = e.detail.value
			},
			isSubChecked(item) {
				return this.jurInput.subjectTypes.filter(x=>x === item.id).length > 0
			},
			isCarChecked(item) {
				return this.jurInput.cars.filter(x=>x === item.id).length > 0
			},
			save() {
				var that = this
				if(!that.jurInput.cars || that.jurInput.cars.length === 0 || !that.jurInput.subjectTypes || that.jurInput.subjectTypes.length === 0 ) {
					uni.showToast({
						icon:'error',
						title:'请配置权限'
					})
					return
				}
				that.$http(that.$api.config.createjurisdiction, 'POST', that.jurInput).then(res => {
					if(res.data) {
						uni.showToast({
							icon:'success',
							title:'设置成功'
						})
						that.$refs.jurisdiction.close()
					}
				})
			}
		}
	}
</script>

<style lang="less" scoped>
	.errCard {
		padding: 10px;
		margin: 10px auto;
	}
	
	  .uni-media-list{
	        display: flex;
	        flex-direction: row;
	        margin-left: 32rpx;
	        margin-right: 32rpx;
	        margin-top: 20rpx;
			border-color: #999999;
			border: black;
			
	        .uni-media-list-logo {
	            width: 480rpx;
	            height: 140rpx;
	        }
	        
	        
	        .uni-media-list-body {
	            flex-direction: row;
	            height: auto;
	            margin-left: 32rpx;
	            margin-right: 32rpx;
	            justify-content: space-around;
	        }
	         
	        .uni-media-list-text-top {
	            height: 74rpx;
	            font-size: 28rpx;
	            overflow: hidden;
				 color: #999999;
	        }
	         
	        .uni-media-list-text-bottom {
	            display: flex;
	            flex-direction: row;
	            margin-top: 20rpx;
	            margin-right: 20rpx;
	            font-size: 27rpx;
	           
	        }
	    }
</style>
