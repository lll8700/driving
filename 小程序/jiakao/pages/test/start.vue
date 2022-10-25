<template>
	<view class="content">
		<view class="content-wrap">
			<view class="center">
				倒计时 ：{{countdownTxt}}  
			</view>
			<test :item='dataItem' :anstype='seltype' @next="next()" />
		</view>

		<view class="u_foot">
			<view class="itemflex">
				<button class="mini-btn" type="primary" @click="pre" :disabled="!isLast">上一题</button>
				<button class="mini-btn" type="primary"@click="showDrawer('showLeft')" >{{ getInfo() }} </button>
				<button v-if="!isEnd" class="mini-btn " type="primary" :disabled="!isNext" @click="next">下一题</button>
				<button v-else class="mini-btn " type="primary" @click="put">提交</button>
			</view>
		</view>
		
			
		
		
		<view title="答案" type="line">
			<view class="answer_body">
				<!-- >90  success  info   <90 error -->
				<button type="primary" @click="dialogToggle('error')" ><text class="word-btn-white" >提交</text>
				</button>
				
				<uni-drawer :width="320" ref="showLeft" mode="left" :mask-click="false"
					@change="change($event,'showLeft')">
					<view class="close">
						<view class="ans_result">
							<uni-row>
								<!-- 答案结果显示 -->
								<uni-col :span="6" class="carWrap" v-for="(t,cidx) in list" :key="'ii_'+ cidx" >
									<!-- result == null  noblock  :class=" ? 'blue':'red'  -->
									<view class="selicon blue" v-if="t.checkSel && t.checkSel.length > 0" @click="saveIndex(cidx)"> 
										<!-- result == null  noblock  :class=" ? 'blue':'red'  -->
										{{cidx+1}} 
									</view>
									<view v-else class="selicon" @click="saveIndex(cidx)">
										{{cidx+1}}
									</view>
								</uni-col>

							</uni-row>
						</view>
						<button @click="closeDrawer('showLeft')"><text class="word-btn-white">关闭</text></button>
					</view>
				</uni-drawer>
			</view>
		</view>
		
		<view>
			<!-- 提示窗示例 -->
			<uni-popup ref="alertDialog" type="dialog">
				<uni-popup-dialog :type="msgType" cancelText="提交试卷" confirmText="继续答题"  :title="alertTitle"  @confirm="dialogConfirm"
					@close="dialogClose">
					<view class="dialogContent">
						<!-- <text class="alertTitle  red">{{alertTitle}}</text> -->
						<view class="tipContent">
							未做题 :50
						</view>
						<view class="">
							<text>剩余时间：{{countdownTxt}}</text>
						</view>
					</view>
					</uni-popup-dialog>
			</uni-popup>
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
				testlen: [0, 1, 2, 3, 4, 5, 6],
				alertTitle:'成绩不合格',
				showLeft: false,
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
				},
				msgType: 'success',
			}
		},
		created() {
			this.initData()
		},
		// onNavigationBarButtonTap(e) {
		// 	if (this.showLeft) {
		// 		this.$refs.showLeft.close()
		// 	} else {
		// 		this.$refs.showLeft.open()
		// 	}
		// },
		methods: {
			dialogClose() {
				console.log('点击关闭')
				// 调 提交
				
			},
			
			dialogConfirm() {
				this.messageText = `点击确认了 ${this.msgType} 窗口`
				this.$refs.alertDialog.close()
			},
			dialogToggle(type) {
				this.msgType = type
				this.$refs.alertDialog.open()
			},
			showDrawer(e) {
				this.$refs[e].open()
			},
			// 关闭窗口
			closeDrawer(e) {
				this.$refs[e].close()
			},
			// 抽屉状态发生变化触发
			change(e, type) {
			
				this.showLeft = e
			},
			saveIndex(index) {
				this.index = index
				this.dataItem = this.list[this.index];
				this.closeDrawer('showLeft')
			},
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
				that.$http(that.$api.Practice.testlist, "POST", that.testInput).then(res => {
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
			getInfo() {
				return this.list.filter(item=> {
					return item.checkSel
				}).length + '/' + this.list.length
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
				if (that.index < 1) {
					that.isLast = false;
				} else {
					that.isLast = true;
				}
				if (that.index >= that.totalCount - 1) {
					that.isEnd = true
				} else {
					that.isEnd = false
				}
				if (that.index < that.testList.length) {
					this.dataItem = this.testList[this.index];
				} else if (that.index < this.list.length) {
					this.dataItem = this.list[this.index];
					that.testList.push(this.dataItem);
				}
			},
			next() {
				var that = this;
				if (that.index < that.totalCount) {
					that.index++;
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
			.selico{
				display: inline-block;
				padding:4px 10px;
				margin-left: 28px;
				border:1px solid cadetblue;
				color:cadetblue ;
				border-radius: 20px;
				
			}
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
	

	.answer_body {
		padding: 20px;

		button {
			width: 80%;
			margin-top: 30px;
		}

		.ans_result {
			padding:10px;
			height: 100vh;
			overflow: hidden;
			overflow-y: scroll;
			.carWrap {
				.selicon {
					width: 40px;
					height: 40px;
					text-align: center;
					line-height: 40px;
					border: 2px solid #333;
					margin: 10px;
					border-radius: 50%;


				}

				.noblock {
					color: #333;
					border-color: #333;
				}

				.red {
					color: orangered;
					border-color: orangered;
				}

				.blue {
					color: royalblue;
					border-color: royalblue;
				}
			}
		}
	}
</style>
