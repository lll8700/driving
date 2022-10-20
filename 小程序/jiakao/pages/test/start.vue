<template>
	<view class="content">
		<view class="content-wrap">
			<view class="center">
				倒计时 ：{{countdownTxt}}
			</view>
			<test :item='dataItem' :anstype='seltype' @next ="next()"/>
		</view>

		<view class="u_foot">
			<view class="itemflex">
				<button class="mini-btn" type="primary" @click="pre" :disabled="!isLast">上一题</button>
				<button v-if="!isEnd" class="mini-btn " type="primary" :disabled="!isNext" @click="next" >下一题</button>
				<button v-else class="mini-btn " type="primary" @click="put">提交</button>
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
				seltype: 3,
				countdownTxt: '',
				timer: null,
				dataItem: {}, // 选择的项
				seq: 0,
				input: {}, // 请求页面参数
				list: [], // 加载的题库集合
				index: -1, // 当前显示的题库指针
				isNext: true, // 是否可以下一题
				isLast: false, // 是否可以上一题
				testList: [], // 已经答了的考试题
				totalCount: 0, // 考试题数量
				isEnd: false, // 是否已经全部答完
				testInput: {
					choiceCount: 40, // 单选40个
					unChoiceCount: 60, // 选择题60个
				}
			}
		},
		created() {
			this.initData()
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
			initData() {
				var that = this;
				console.log('initData')
				that.$http(that.$api.Practice.testlist, "POST", that.testInput).then(res => {
					console.log(res)
					that.totalCount = res.data.totalCount;
					that.list = res.data.items;
					that.next()
				})
			},
			pre() {
				var that = this;
				if (this.index > -1) {
					this.index--;
					that.getItem();
				}
			},
			// 提交考试
			put() { 
				uni.showToast({
					icon: "none",
					title: '这里是提交后的显示',
					duration: 3000
				})
			},
			getItem() {
				var that = this;
				if(that.index < 1) {
					that.isLast = false;
				}else {
					that.isLast = true;
				}
				if(that.index >= that.totalCount - 1) {
					that.isEnd = true
				}else {
					that.isEnd = false
				}
				if(that.index < that.testList.length) {
					this.dataItem = this.testList[this.index];
				} else if(that.index <  this.list.length) {
					this.dataItem = this.list[this.index];
					that.testList.push(this.dataItem);
				}
			},
			next() {
				var that = this;
				console.log(that.testList)
				if(that.index < that.totalCount) {
					that.index ++ ;
					that.getItem();
				}
			},

		},
		onLoad: function(option) {
			this.input.carTypeId = option.carType;
			this.input.subjectTypeId = option.subject;
			this.input.ids = [];
			// this.next();
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
