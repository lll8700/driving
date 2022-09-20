<template>
	<view class="content">
		<view class="content-wrap">
			<view class="center">
				倒计时 ：{{countdownTxt}}
			</view>
			<test :item='question[seq]' :anstype='seltype' />
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
				seq: 0,
				countdownTxt: '',
				timer: null

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
				if (this.seq === 0) {
					return
				}
				this.seq--
			},
			next() {
				if (this.seq == question.length - 1) {
					return
				}
				this.seq++
			}

		},
		onLoad: function(option) {
			this.seltype = option.type
			console.log(option.type); //打印出上个页面传递的参数。

		},
		onReady() {
			this.countdownFun(60 * 60)


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
	}

	.u_foot {
		
		width: 100%;
		bottom: 20px;
		.itemflex {
			display: flex;
		}
	}
</style>
