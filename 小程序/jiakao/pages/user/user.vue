<template>
	<view class="contanier">
		<view class="m10">
			<button class="mini-btn" type="warn" plain="true" @click="toUrl()">错题库</button>

		</view>
		<view class="m10">
			<button v-if="userDto.userTypeEnum === 10 || userDto.userTypeEnum === 30" class="mini-btn" type="primary" @click="add()">开通账号</button>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				userDto: {},
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
</style>
