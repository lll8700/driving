<template>
	<view class="contanier">
		<uni-list>
			<uni-list-item title="当前题库" showArrow>
				<template v-slot:footer class="carClass">
					<span @click="open()">{{carName}} {{selVal}}</span>
				</template>
			</uni-list-item>
		</uni-list>
		<uni-section title="切换题库" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button class="mini-btn" type="primary" size="mini" plain="true" v-for="(t,index) in queTypeArrs"
					:key="index" @click="changeType(index)">{{t}}</button>
			</uni-card>
		</uni-section>
		<uni-section title="驾驶证类型" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button class="mini-btn" v-for="(citem,cidx) in typeArrs" :key="cidx" type="primary" size="mini"
					plain="true" @click="changeCarType(cidx)">{{citem.name}}<br /> {{citem.subname}}</button>
			</uni-card>
		</uni-section>
		<uni-section title="题库练习" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button class="mini-btn" type="default" size="mini" plain="true" @click="toUrl(1)">全量题库</button>
				<button class="mini-btn" type="warn" size="mini" plain="true" @click="toUrl(2)">错题库</button>
			</uni-card>
		</uni-section>
		<uni-section title="模拟考试" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button class="mini-btn" type="primary" size="mini" plain="true" @click="toUrl(2)">开始考试</button>

			</uni-card>
		</uni-section>
		<uni-popup ref="popup" type="bottom" background-color="#fff">
			<view class="">

			</view>
		</uni-popup>

	</view>
</template>

<script>
	export default {
		data() {
			return {
				carName: '小车',
				queType: 0,
				typeArrs: [{
					name: '小车',
					subname: 'C1C2C3',
					value: 1
				}, {
					name: '货车',
					subname: 'A2/N2',
					value: 2
				}, {
					name: '轻型牵引挂车',
					subname: 'C6',
					value: 3
				}],
				queTypeArrs: ['科目一', '科目四', '扣满12分'],


			}
		},
		computed: {
			selVal() {
				return this.queTypeArrs[this.queType]
			}
		},
		methods: {
			toUrl(type) {
				if (type === 2) {
					uni.navigateTo({
						url: '/pages/test/start?type=' + type,
					});
				} else {
					uni.navigateTo({
						url: '/pages/answer/answer?type=' + type,
					});
				}

			},
			changeCarType(val) {
				this.carName = this.typeArrs[val].name
				this.close()

			},
			changeType(val) {
				this.queType = val
				this.setNav(this.queTypeArrs[this.queType])
			},
			setNav(val) {
				uni.setNavigationBarTitle({
					title: val
				});
			},
			open() {
				console.log('open')
				this.$refs.popup.open('top')
			},
			close() {
				this.$refs.popup.close()
			}


		},
		onReady() {
			this.setNav('科目一')
		}
	}
</script>

<style lang="less" scoped>
	.contanier {
		padding: 10px;

		.carClass {
			color: #00ff44;
		}

		.btn {
			button {
				margin: 0 5px;
			}
		}
	}
</style>
