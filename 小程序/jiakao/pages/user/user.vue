<template>
	<view class="contanier">
		<view class="m10">
			<button class="mini-btn" type="warn" plain="true" @click="toUrl()">错题库</button>

		</view>
		<view class="m10">
			<button v-if="userDto.userTypeEnum === 10 || userDto.userTypeEnum === 30" class="mini-btn" type="primary" @click="add()">开通账号</button>
			<button v-if="userDto.userTypeEnum === 10 || userDto.userTypeEnum === 30" class="mini-btn" type="primary" @click="$refs.alertDialog.open()">开通账号</button>
		</view>
		<view>
			<!-- 提示窗示例 -->
			<uni-popup ref="alertDialog" type="dialog">
				<uni-popup-dialog cancelText="关闭" confirmText="确认开通"  title="开通账号"  :before-close="true" @confirm="createUser" 
					@close="$refs.alertDialog.close()">
					<view class="dialogContent" >
						<view class="uni-forms-item uni-column">
									<view class="uni-forms-item uni-column list">
										<view  class="t">账号:</view>
										<input class="c" v-model="createInput.phoneNumber" maxlength="11" placeholder="请输入手机号" />
									</view>
									<view class="uni-forms-item uni-column list">
										<view class="t">密码:</view>
										<input class="c" v-model="createInput.password" placeholder="请输入密码" />
									</view>
						</view>
						
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
				userDto: {},
				createInput: {
					phoneNumber:'',
					password:''
				}
			}
		},
		created() {
			var user = uni.getStorageSync("User");
			this.userDto = user;
		},
		methods: {
			add(){
				uni.navigateTo({
					url: '/pages/user/add'
				});
			},
			toUrl(type) {
				uni.navigateTo({
					url: '/pages/errlist/errlist'
				});
			},
			createUser() {
				var that = this;
				console.log(111111)
				that.$http(that.$api.login.create, "POST", that.createInput).then(res => {
					this.$refs.alertDialog.close()
					uni.showToast({
						title: '添加成功',
						duration: 1000
					})
				})
			},
		},

	}
</script>

<style lang="less" scoped>
	.contanier {
		padding: 10px;

		.m10 {
			margin: 5px;
		}

	}
	.dialogContent{
		.alertTitle{
			font-weight: 500;
			font-size: 1.2rem;
			.red{
				color: orangered;
			}
		}
		.tipContent{
			margin:10px;
			font-size: 1.1rem;
			font-weight: 500;
			color: orangered;
		}
	}
	.list {
		padding: 1rem;
		border:1px solid #1ba7e3;
		border-radius: 30px;
		color: #1ba7e3;
		
		margin:10px;
		.t{
			width:20%;
			float: left;
			
		}
		.c{
			width:80%;
		}
	
	}
</style>
