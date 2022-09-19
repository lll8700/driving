<template>
	<view class="contain">
		<view class="title">

			<uni-tag class="tip" :inverted="true" :text="item.type == 1?'单选':'多选'" type="primary" /> {{item.title}}

		</view>
		<view class="ansLists">
			<view class="img">
				<img v-if="item.imgArr.length>0" :src="item.imgArr[0]" alt="">

			</view>
			<view class="itemlist" @click="selVal(index)" v-for="(aitem,index) in item.ansArr " :key="'anslist_'+index"
				:class="selIndex == index ? 'actvieColor' :''">
				{{aitem}}
			</view>

		</view>
		<view class="isShow" v-if="anstype === 1 ||( anstype === 0 && selIndex !== null)">

			<view class="rightAns">
				<view class="ans">
					答案 ： {{item.rightAns.rightVal}}
				</view>
				<view class="ans" v-show="anstype === 0">
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
				selected: ''

			}
		},
		watch: {
			item(newValue, oldValue) {
				this.selIndex = null;
				this.selected = ''
			}
		},
		methods: {
			selVal(val) {
				if (this.anstype === 1) {
					return
				}
				this.selIndex = val
				this.selected = this.item.ansArr[val].substring(0, 1)
			}
		},
		
	}
</script>

<style lang="less" scoped>
	.contain {
		line-height: 72upx;
	}

	.title {
		line-height: 72upx;

		.tip {
			margin-right: 20upx;
			margin-bottom: 20upx;
		}
	}

	.itemlist {
		line-height: 72upx;
	}

	.rightAns {
		background: #efefef;
		padding: 20upx;
		display: flex;


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

	.actvieColor {
		color: #0085ff;
	}
</style>
