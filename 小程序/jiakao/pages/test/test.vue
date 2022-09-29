<template>
	<view class="contain">
		<view class="title">
			<uni-tag class="tip" :text="item.choiceTyopeEnmName" type="primary" /> {{item.title}}
		</view>
		<view class="ansLists">
			<view class="img" v-for="(m,index) in item.practiceImages" :key="'img_'+index">
				<img :src="m.url">
			</view>
			<!-- <view class="" v-if="item.choiceTyope !== 20">
				<view class="itemlist" @click="selVal(index)" v-for="(aitem,index) in item.options "
					:key="'anslist_'+index" :class="selIndex == index ? 'actvieColor' :''">

					<span v-if="(selIndex == index || anstype === 1) && anstype !=2 " class="iconShow">
						<span v-if="!aitem.isCorrect" style='color:orangered;' class="iconfont icon-cuowu"></span>
						<span v-if="aitem.isCorrect" style='color:skyblue;' class="iconfont icon-zhengque"></span>
					</span>
					<span v-else class="iconShow">{{aitem.seq}} </span> {{aitem.title}}
				</view>
			</view> -->
			<view>
				<view class="itemlist" @click="selVal(index)" v-for="(aitem,index) in item.options "
					:key="'anslist_'+index" :class="checkSel.findIndex(c=> c.id == aitem.id) !==-1 ? 'actvieColor' :''">
					<span v-if="(checkSel.findIndex(c=> c.id == aitem.id) !==-1 || anstype === 1 ) && anstype !=2 "
						class="iconShow">
						<span v-if="!aitem.isCorrect" style='color:orangered;' class="iconfont icon-cuowu"></span>
						<span v-if="aitem.isCorrect" style='color:skyblue;' class="iconfont icon-zhengque"></span>
					</span>
					<span v-else class="iconShow"></span> {{aitem.title}}
				</view>
			</view>

		</view>

		<view class="isShow" v-if="checkSel.length === isOptions.length || anstype == 1">
			<view class="rightAns">
				<view class="ans">
					答案 ： <span v-for="item1 in isOptions" :key="item1.id" class="isOpensSpan">{{item1.seq}}</span>
				</view>
				<view class="ans" v-if="anstype === 0">
					您选择 ： <span v-for="item1 in checkSel" :key="item1.id" class="isOpensSpan">{{item1.seq}}</span>
				</view>
			</view>

			<view class="p10" v-if="item.skill">
				<view class="p_h">
					答题技巧{{ item.skillLast?"1":"" }}
				</view>
				<view class="p_body">
					{{item.skill}}
				</view>
			</view>
			<view class="p10" v-if="item.skillLast">
				<view class="p_h">
					答题技巧2
				</view>
				<view class="p_body">
					{{item.skillLast}}
				</view>
			</view>

			<view class="rightAns" v-if="item.keyWordls">
				关键字 ：<span class="keyColor">
					{{item.keyWordls}}
				</span>
			</view>

			<view class="p10" v-if="item.Introduce">
				<view class="h_title">
					试题详解
				</view>
				<view class="p_h">
					题目解析
				</view>
				<view class="p_body">
					{{item.Introduce}}
				</view>
			</view>

		</view>


	</view>
</template>

<script>
	export default {
		props: ['item', 'anstype'],
		data() {
			return {
				isOptions: [], // 正确答案
				checkSel: [], // 选择的答案
				count: 1 //正确数量
			}
		},
		watch: {
			item(newValue, oldValue) {
				this.initData()
			}
		},
		methods: {
			initData() {
				var list = this.item.options;
				this.isOptions = list.filter(s => s.isCorrect)
				this.count = this.isOptions.length;
				this.checkSel = []
			},
			selVal(val) { // index
				if (this.checkSel.length >= this.count) { // 选完了
					return
				}
				console.log(val)
				this.checkSel.push(this.item.options[val]);
				// if (this.item.type == 1) {
				// 	this.selIndex = val
				// 	this.selected = this.item.ansArr[val].seq
				// } else {
				// 	// 多选
				// 	const seq = this.item.ansArr[val].seq
				// 	let fdx = this.checkSel.findIndex(c => c == seq)
				// 	if (fdx !== -1) {
				// 		this.checkSel.splice(fdx, 1)
				// 	} else {
				// 		this.checkSel.push(seq)
				// 	}
				// 	console.log(this.checkSel)
				// }

			}
		},

	}
</script>

<style lang="less" scoped>
	.contain {
		line-height: 72upx;
		min-height: 70vh;
	}

	.img {
		img {
			width: 100%;
			height: auto;
		}
	}

	.isOpensSpan {
		margin-left: 10px;
	}

	.title {
		line-height: 72upx;

		.tip {
			margin-right: 20upx;
			margin-bottom: 20upx;
		}
	}

	.itemlist {
		// line-height: 72upx;
	}

	.rightAns {
		background: #e5f7ff;
		padding: 20upx;
		display: flex;
		border-radius: 10px;


		.ans {
			flex: 1
		}

		.keyColor {
			color: #0085ff;
		}
	}

	.p10 {
		padding-top: 20upx;
		padding-bottom: 20upx;

	}

	.h_title {
		font-weight: 500;
		font-size: 32upx;
		text-align: center;


	}

	.actvieColor {
		color: #0085ff;
	}

	.iconShow {
		width: 56upx;
		display: inline-block;

	}
</style>
