<template>
	<view class="content">
		<yui-tabs sticky swipeable :tabs="scrollData" v-model="activeKey" @click="tabClick" @change="tabChange"
			:lineWidth='100' titleActiveColor="#009fff" color="#009fff"  :offsetTop="offsetTop">
			<template #pane0>
				<view class="content-wrap">
					<test :item='question[seq]' :anstype='0' />
				</view>
			</template>
			<template #pane1>
				<view class="content-wrap">
					<test :item='question[seq]' :anstype='1' />
				</view>
			</template>

		</yui-tabs>
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

	import test from '../test/test.vue'
	export default {
		components: {
			test
		},
		data() {
			return {
				question,
				activeKey: 0,
				offsetTop: 0, //粘性定位布局下与顶部的最小距离
				scrollData: ['答题', '背题'],
				seq:0,
			}
		},
		methods: {
			pre(){
				if(this.seq === 0){
					return
				}
				this.seq--
			},
			next(){
				if(this.seq == question.length-1){
					return
				}
				this.activeKey= 0 
				this.seq++
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
			console.log(option.type); //打印出上个页面传递的参数。

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
	.content{
		height: 100vh;
	}
	.content-wrap {
		padding: 20px;
		// height: 70vh;
	}
	
	.u_foot {

		width: 100%;
		bottom: 20px;
		.itemflex {
			display: flex;
		}
	}
</style>
