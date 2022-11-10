<template>
	<view class="content">
		<view class="uni-forms">
			<view class="head">
				注册/登录
			</view>

			<form @submit="formSubmit" @reset="formReset" class="forms">
				<view class="uni-forms-item uni-column">
					<view class="uni-forms-item uni-column list">
						<view  class="t">手机号</view>
						<input class="c" v-model="input.PhoneNumber" maxlength="11" placeholder="请输入手机号" />
					</view>
					<view class="uni-forms-item uni-column list">
						<view class="t">密码</view>
						<input class="c" type="password" v-model="input.password" placeholder="请输入密码" />
					</view>
				</view>
				<view class="sub">
					<view  class="btn" @click="subLogin()" >
						登录
					</view>
		<!-- 			<button class="btn" type="primary" plain="true">登录</button> -->
				</view>

			</form>
		</view>

	</view>
</template>

<script>
	export default {
		data() {
			return {
				input: {
					PhoneNumber: '',
					password: ''
				}
			}
		},
		created() {
			
		},
		onShow() {
			uni.clearStorageSync()
		},
		methods: {
			subLogin() {
				var that = this;
				if (!that.input.PhoneNumber || that.input.PhoneNumber.length !== 11 || !that.input.PhoneNumber.startsWith(
						'1')) {
					uni.showToast({
						title: '请输入正确的手机号',
						icon: 'none',
						duration: 1000
					});
					return
				}
				if (!that.input.password || that.input.password.length < 6) {
					uni.showToast({
						title: '请输入最少六位数密码',
						icon: 'none',
						duration: 1000
					});
					return
				}
				that.$http(that.$api.login.weblogin, "POST", that.input).then(res => {
					if (res.data.code !== 20000) {
						uni.showToast({
							title: '账户或密码错误!',
							icon: 'none',
							duration: 1000
						});
					} else {
						uni.setStorageSync("Token", res.data.data.token);
						uni.setStorageSync("User", res.data.data.userDto);
						uni.showToast({
							title: '登录成功',
							icon: 'none',
							duration: 1000
						});
						uni.switchTab({
							url: '/pages/index/index'
						});
					}

				})
			}
		}
	}
</script>

<style lang="less" scoped>
	.content {
		height: 100vh;
		// position: relative;
		.head{
			width: 100%;
			text-align: center;
			color: #1ba7e3;
			font-size:2rem;
			margin-top: 6rem;
			margin-bottom: 2rem;
		}

		.forms {
			width: 100%;
			padding: 40px;
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
		}

		.sub {
			margin-top:2rem;
			width: 100%;
			
			.btn{
				width: 60%;
				
				margin:10px;
				margin-left: 20%;
				font-size: 1.6rem;
				text-align: center;
				// padding: 1rem;
				border:0;
				border-bottom:1px solid #1ba7e3;
				// border-radius: 30px;
				// width: 60%;
				color: #1ba7e3;
				
				
			}

		}
	}
</style>
