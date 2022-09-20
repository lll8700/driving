<template>
	<view class="contain">
		<view class="title">

			<uni-tag class="tip" :text="item.type == 1?'单选':'多选'" type="primary" /> {{item.title}}

		</view>
		<view class="ansLists">
			<view class="img" v-for="(m,index) in item.imgArr" :key="'img_'+index">
				<img :src="m.url">
			</view>
			<view class="" v-if="item.type == 1">


				<view class="itemlist" @click="selVal(index)" v-for="(aitem,index) in item.ansArr "
					:key="'anslist_'+index" :class="selIndex == index ? 'actvieColor' :''">

					<span v-if="(selIndex == index || anstype === 1) && anstype !=2 " class="iconShow">
						<span v-if="!aitem.rightTag" style='color:orangered;' class="iconfont icon-cuowu"></span>
						<span v-if="aitem.rightTag" style='color:skyblue;' class="iconfont icon-zhengque"></span>
					</span>
					<span v-else class="iconShow">{{aitem.seq}} </span> {{aitem.name}}
				</view>
			</view>
			<view  v-if="item.type == 2">
				<view class="itemlist" @click="selVal(index)" v-for="(aitem,index) in item.ansArr "
					:key="'anslist_'+index" :class="checkSel.findIndex(c=> c == aitem.seq) !==-1 ? 'actvieColor' :''">
					<span v-if="(checkSel.findIndex(c=> c == aitem.seq) !==-1 || anstype === 1 ) && anstype !=2 " class="iconShow">
						<span v-if="!aitem.rightTag" style='color:orangered;' class="iconfont icon-cuowu"></span>
						<span v-if="aitem.rightTag" style='color:skyblue;' class="iconfont icon-zhengque"></span>
					</span>
					<span v-else class="iconShow">{{aitem.seq}} </span> {{aitem.name}}
				
				
				</view>
			</view>

		</view>

		<view class="isShow" v-if="anstype === 1">

			<view class="rightAns">
				<view class="ans">
					答案 ： {{item.rightAns.rightVal}}
				</view>
				<view class="ans" v-if="anstype === 0">
					您选择 ： {{selected}}
				</view>
			</view>

			<view class="p10">
				<view class="p_h">
					答题技巧
				</view>
				<view class="p_body">
					{{item.rightAns.tec}}
				</view>
			</view>


			<view class="rightAns">
				关键字 ：<span class="keyColor">
					{{item.rightAns.keyVal}}
				</span>
			</view>

			<view class="p10">
				<view class="h_title">
					试题详解
				</view>
				<view class="p_h">
					题目解析
				</view>
				<view class="p_body">
					{{item.rightAns.analysis}}
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
				selIndex: null,
				selected: '', // seq
				checkSel:[]

			}
		},
		watch: {
			item(newValue, oldValue) {
				this.selIndex = null;
				this.selected = ''
			}
		},
		methods: {
			selVal(val) { // index
				if (this.anstype === 1) {
					return
				}
				if(this.item.type == 1){
					this.selIndex = val
					this.selected = this.item.ansArr[val].seq
				}else{
					// 多选
					console.log('duoxua')
					const seq = this.item.ansArr[val].seq
					let fdx = this.checkSel.findIndex(c => c == seq)
					if(fdx !== -1){
						this.checkSel.splice(fdx,1)
					}else{
						this.checkSel.push(seq)
					}
					console.log(this.checkSel)
				}
				
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
