<template>
	<view class="contanier">
		<!-- <view class="m10">
			<button class="mini-btn" type="warn" plain="true" @click="toUrl()">错题库</button>

		</view> -->
		<view class="m10" v-if="userDto.userTypeEnum === 10 || userDto.userTypeEnum === 30">
			<button class="mini-btn" type="primary" @click="add()">开通账号</button>
		</view>
		<view class="m10" v-if="userDto.userTypeEnum === 10 || userDto.userTypeEnum === 30">
			<button class="mini-btn" type="primary" @click="list()">账户列表</button>
		</view>
		<view class="m10" style="margin-top: 20px;">
			<button class="mini-btn" type="default" @click="$refs.alertDialog.open()">退出登录</button>
		</view>
		<view>
			<!-- 提示窗示例 -->
			<uni-popup ref="alertDialog" type="dialog">
				<uni-popup-dialog cancelText="关闭" confirmText="确认退出"  title="退出登录"  :before-close="true" @confirm="outUser" 
					@close="$refs.alertDialog.close()">
					<view class="dialogContent" >
						
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
			
		},
		onShow() {
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
			list() {
				uni.navigateTo({
					url: '/pages/user/list'
				});
			},
			outUser() {
				var that = this;
				uni.clearStorageSync()
				that.$refs.alertDialog.close()
				uni.navigateTo({
					url: '/pages/login/login',
				});
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
