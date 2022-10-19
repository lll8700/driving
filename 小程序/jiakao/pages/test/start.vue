<template>
	<view class="content">
		<view class="content-wrap">
			<view class="center">
				倒计时 ：{{countdownTxt}}
			</view>
			<test :item='dataItem' :anstype='seltype' :next = "next"/>
		</view>

		<view class="u_foot">
			<view class="itemflex">
				<button class="mini-btn" type="primary" @click="pre">上一题</button>
				<button class="mini-btn " type="primary" @click="next">下一题</button>
			</view>
		</view>

	</view>
</template>

<script>
	import {
		question
	} from '../../common/data.js'
	import test from './test.vue'
	export default {
		components: {
			test
		},
		data() {
			return {
				question,
				seltype: '',
				countdownTxt: '',
				timer: null,
				dataItem: {}, // 选择的项
				seq: 0,
				input: {}, // 请求页面参数
				list: [], // 加载的题库集合
				index: -1, // 当前显示的题库指针
				isNext: true
			}
		},
		methods: {
			countdownFun(diffTime) {
				if (diffTime > 0) {
					this.countdownTime = setInterval(() => {
						let diffM =
							Math.floor(diffTime / 60) > 9 ?
							Math.floor(diffTime / 60) :
							`0${Math.floor(diffTime / 60)}`;
						let diffS =
							Math.floor(diffTime % 60) > 9 ?
							Math.floor(diffTime % 60) :
							`0${Math.floor(diffTime % 60)}`;
						this.countdownTxt = `${diffM}分${diffS}秒`;


						diffTime--;
						if (diffTime < 0) {
							clearInterval(this.countdownTime);
							return;
						}

					}, 1000);


				}
			},
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
				that.$http(that.$api.Practice.random, "POST", that.input).then(res => {
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

		},
		onLoad: function(option) {
			this.input.carTypeId = option.carType;
			this.input.subjectTypeId = option.subject;
			this.input.ids = [];
			this.next();
		},
		onReady() {
			this.countdownFun(45 * 60)
		},
	}
</script>

<style lang="less" scoped>
	.content {
		height: 90vh;
		position: relative;

		.center {
			text-align: center;
		}
	}

	.content-wrap {
		padding: 20px;
		height: auto;
		font-size: 1.1rem;
	}

	.u_foot {
		
		width: 100%;
		bottom: 20px;
		.itemflex {
			display: flex;
		}
	}
</style>
