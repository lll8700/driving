<template>
	<view class="contain">
		<view class="title" v-if="item.title">
		
		<span v-if="seqNo != null">{{seqNo+1}} 、</span>	<uni-tag class="tip" :text="item.choiceTyopeEnmName" type="primary" /> {{item.title}}
		</view>
		<view class="ansLists">
			<view class="img" v-if="item.practiceImages && item.practiceImages.length === 1" style="text-align: center; height: 220px;">
				<img :src="getImage(item.practiceImages[0].url)" style="max-height:150px; max-width: 500px;">
			</view>
			<view class="img" v-else v-for="(m,index) in item.practiceImages" :key="'img_'+index">
				<img :src="getImage(m.url)" style="max-height:200px; max-width: 200px;">
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
					
					<span v-if="(checkSel.findIndex(c=> c.id == aitem.id) !==-1 || anstype === 1 ) && anstype !=2 && anstype !=3"
						class="iconShow">
						<span v-if="!aitem.isCorrect" style='color:orangered;' class="iconfont icon-cuowu"></span>
						<span v-if="aitem.isCorrect" style='color:skyblue;' class="iconfont icon-zhengque"></span>
					</span>
					<span v-else class="iconShow"></span> 
					{{aitem.title}}
				</view>
			</view>

		</view>

		<view class="isShow" v-if="(checkSel.length === isOptions.length && anstype ===0 && isError) || anstype == 1 || isShow">
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
			<view class="barline">
				
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
		props: ['item', 'anstype','seqNo'],
		data() {
			return {
				isOptions: [], // 正确答案
				checkSel: [], // 选择的答案
				count: 1 ,//正确数量
				isEdit: false, // 是否是修改
				isError: false, //是否答错题
				isShow: false // 是否直接显示
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
				this.isOptions = list && list.length > 0 &&list.filter(s => s.isCorrect) || []
				this.count = this.isOptions.length;
				this.checkSel = [];
				this.isError = false;
				this.isShow =false
				if(this.item.checkSel) {
					this.checkSel = this.item.checkSel;
					if(this.anstype !== 3) 
						this.isShow = true
				}
			},
			clearData() {
				this.isOptions = [] // 正确答案
				this.checkSel = [] // 选择的答案
				this.count = 1 //正确数量
				this.isError = false
			},
			getImage(url) {
				return 'http://106.14.209.175:800/api/imgae/' + url
			},
			
			selVal(val) { // index
				var that = this
				if (this.checkSel.length >= this.count) { // 选完了
					if(this.anstype === 3) {
						this.checkSel = [];
						this.isEdit =true;
					}else {
						return
					}
				}
				this.checkSel.push(this.item.options[val]);
				
				if(this.checkSel.length === this.count) { // 全部答完了 
					this.item.checkSel = this.checkSel
					if(this.anstype === 0) { // 答题结束后 如果是全部正确则自动下一个
						var isNext = true
						
						for(var i = 0; i < that.isOptions.length; i++) {
							var item = that.checkSel.filter(s => s.seq === that.isOptions[i].seq);
							if(item.length === 0) {
								isNext = false
								break;
							}
						}
						if(isNext) {
							this.clearData()
							
							this.$emit('next');
						}else {
							this.isError =true;
						}
						
					}else if(this.anstype === 3) { // 考试也要下一个
						if(!this.isEdit) {
							setTimeout(()=>{
								this.clearData()
								this.$emit('next');
							},500)
						}
					}
				}
			}
		},

	}
</script>

<style lang="less" scoped>
	.contain {
		padding-top:10px;
		line-height: 72upx;
		min-height: 70vh;
	}

	.img {
		img {
			width: 80%;
			margin:20px 10%;
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
	.barline{
		width: 100%;
		height: 0.1rem;
		background-color: #0085ff;
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
