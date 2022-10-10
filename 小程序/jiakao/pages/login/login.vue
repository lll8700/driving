<template>
	<view>
		<span>手机号:</span> <input type="text" v-model="input.PhoneNumber" maxlength="11" />
		<span>密码:</span> <input type="password" v-model="input.password" />
		<button class="mini-btn" @click="subLogin()" maxlength="16"  size="mini" plain="true">登录</button>
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
			uni.clearStorageSync()
		},
		methods: {
			subLogin() {
				var that = this;
				if(!that.input.PhoneNumber || that.input.PhoneNumber.length !== 11 || !that.input.PhoneNumber.startsWith('1')) {
					uni.showToast({
						title: '请输入正确的手机号',
						icon: 'none',
						duration: 1000
					});
					return
				}
				if(!that.input.password || that.input.password.length < 6 ) {
					uni.showToast({
						title: '请输入最少六位数密码',
						icon: 'none',
						duration: 1000
					});
					return
				}
				that.$http(that.$api.login.weblogin, "POST", that.input).then(res=> {
					if(res.data.code !== 20000) {
						uni.showToast({
							title: '账户或密码错误!',
							icon: 'none',
							duration: 1000
						});
					}else {
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

<style>

</style>
