<template>
	<view class="content">
		<yui-tabs sticky swipeable :tabs="scrollData" v-model="activeKey" @click="tabClick" @change="tabChange"
			:lineWidth='100' titleActiveColor="#009fff" color="#009fff" :offsetTop="offsetTop">
			<template #pane0>
				<view class="content-wrap">
					<test :item='dataItem' :anstype='0' @next = "next"/>
				</view>
			</template>
			<template #pane1>
				<view class="content-wrap">
					<test :item='dataItem' :anstype='1'/>
				</view>
			</template>

		</yui-tabs>
		<view class="u_foot">
			<view class="itemflex">
				<button class="mini-btn" type="primary" @click="pre" :disabled="index <= 0">上一题</button>
				<button class="mini-btn " type="primary" @click="next" :disabled="!isNext">下一题</button>
			</view>
		</view>

	</view>
</template>

<script>
	// import {
	// 	question
	// } from '../../common/data.js'

	import test from '../test/test.vue'
	export default {
		components: {
			test
		},
		data() {
			return {
				activeKey: 0,
				offsetTop: 0, //粘性定位布局下与顶部的最小距离
				scrollData: ['答题', '背题'],
				seq: 0,
				input: {}, // 请求页面参数
				list: [], // 加载的题库集合
				index: -1, // 当前显示的题库指针
				dataItem: {}, // 选择的项
				isNext: true
			}
		},

		methods: {
			pre() {
				var that = this;
				if (this.index > -1) {
					this.index--;
					this.dataItem = this.list[this.index]
					that.isNext = true;
				}
			},
			next() {
				var that = this;
				if (that.index <= this.list.length - 2 && that.list.length > 0) {
					this.index++;
					this.dataItem = this.list[this.index];
					return
				}
				if (!that.isNext) {
					return
				}
				that.input.id = that.dataItem.id;
				if (that.dataItem.id) {
					that.input.ids.push(that.dataItem.id);
				}
				that.dataItem = {}
				that.$http(that.$api.Practice.next, "POST", that.input).then(res => {
					if (res.data.id) {
						that.list.push(res.data);
						this.index++;
						that.dataItem = res.data;
						that.index = that.list.length - 1;
					} else {
						that.isNext = false;
						uni.showToast({
							icon: "none",
							title: '已经没有再多的题了',
							duration: 1500
						})
					}

				})
			},
			tabClick(index, item) {
				console.log("tabClick", index, item);
			},
			// 标签切换事件
			tabChange(index, item) {
				console.log("tabChange", index, item);
			},

		},
		onLoad: function(option) {
			this.input.carTypeId = option.carType;
			this.input.subjectTypeId = option.subject;
			this.input.ids = [];
			this.next();
		},
		onReady() {
			uni.setNavigationBarTitle({
				title: '答题'
			});
		},
		onPageScroll(e) {
			//页面滚动事件
			uni.$emit('onPageScroll', e)
		},
		mounted() {
			uni.getSystemInfo({
				success: (e) => {
					let offsetTop = 0
					// #ifdef H5
					offsetTop = 43
					// #endif

					this.offsetTop = offsetTop;
				}
			})
		},
		// 页面滚动触发事件
		onPageScroll(e) {
			//页面滚动事件
			uni.$emit('onPageScroll', e)
		},
	}
</script>

<style lang="less" scoped>
	.content {
		height: 98vh;
	}

	.content-wrap {
		padding: 20px;
		margin-bottom: 40px;
		font-size: 1.1rem;
		// height: 70vh;
	}

	.u_foot {
		position: sticky;
		background: #fff;

		width: 100%;
		bottom: 0;
		padding: 10px;

		.itemflex {
			display: flex;
		}
	}
</style>
