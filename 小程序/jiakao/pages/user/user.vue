<template>
	<view class="contanier">
		<view class="m10">
			<button class="mini-btn" type="warn" plain="true" @click="toUrl()">错题库</button>

		</view>
		<view class="m10">
			<button class="mini-btn" type="primary">开通账号</button>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {

			}
		},
		methods: {
			toUrl(type) {
				uni.navigateTo({
					url: '/pages/answer/answer?type=' + type,
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
