<template>
	<view>

		<uni-list>
			<uni-list-item clickable @click="todetail(1)" :title="`账号：${'1771009281'}`" note="普通会员" rightText="正式" />
		</uni-list>
	</view>
</template>

<script>
	export default {
		data() {
			return {

			}
		},
		methods: {
			todetail(detailId){
				console.log('lll')
				uni.navigateTo({
					url: '/pages/errlist/detail?detailId='+detailId
				});
			}
		}
	}
</script>

<style lang="less" scoped>
	.errCard {
		padding: 10px;
		margin: 10px auto;
	}
</style>
