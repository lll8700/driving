<template>
	<view class="contanier">
		<uni-list>
			<uni-list-item title="当前题库"   >
				<template v-slot:footer class="carClass">
					<span>{{carName}} {{selVal}}</span>	
				</template>
			</uni-list-item>
		</uni-list>
		<uni-section title="驾驶证类型" type="line">
				<uni-row class="demo-uni-row">
					<uni-col v-for="(citem,cidx) in typeArrs" :key="cidx" :span="8" class="driveWrap">
						<view  @click="changeCarType(cidx)" class="drives"  :class="cartype === citem.value ?'carActive':''"  >
					<span style='font-size: 36px;color: skyblue;' class="iconfont" :class="citem.icon"></span> <br/>{{citem.name}}<br/> {{citem.subname}}
						</view>
						
					</uni-col>
				</uni-row>
				
		</uni-section>
		<uni-section title="切换题库" type="line">
			<uni-card :is-shadow="false" class="btn">
				<uni-row class="demo-uni-row">
					<uni-col v-for="(t,index) in queTypeArrs" :key="index" :span="6" class="carWrap">
						<uni-tag  class="cartype" @click="changeType(index)" :inverted="true" :text="t" type="primary" :class="queType === index ?'carActive':''"  />
						
					</uni-col>
				</uni-row>
			
			</uni-card>
		</uni-section>
		<uni-section title="题库练习" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button class="mini-btn" type="default" size="mini" plain="true" @click="toUrl(1)">全量题库</button>
				
			</uni-card>
		</uni-section>
		<uni-section title="模拟考试" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button class="mini-btn" type="primary" size="mini" plain="true" @click="toUrl(2)">开始考试</button>

			</uni-card>
		</uni-section>
		<uni-popup ref="popup" type="bottom" background-color="#fff">
			<view class="">
				<uni-section title="驾驶证类型" type="line">
					<uni-card :is-shadow="false" class="btn">
						<button class="mini-btn" v-for="(citem,cidx) in typeArrs" :key="cidx" type="primary" size="mini" plain="true" @click="changeCarType(cidx)">{{citem.name}}<br/> {{citem.subname}}</button>
					</uni-card>
				</uni-section>
			</view>
		</uni-popup>

	</view>
</template>

<script>
	export default {
		data() {
			return {
				carName:'小车',
				queType: 0,
				cartype:1,
				typeArrs: [{
					name: '小车',
					subname:'C1C2C3',
					value: 1,
					icon:'icon-xiaoche'
				},{
					name: '摩托车',
					subname:'D/F/E',
					value: 2,
					icon:'icon-motuoche'
				} ,{
					name: '货车',
					subname:'A2/N2',
					value: 3,
					icon:'icon-xiaochewuliu'
				},{
					name: '轻型牵引挂车',
					subname:'C6',
					value: 4,
					icon:'icon-qianyinche01'
				}],
				queTypeArrs: ['未报名','科目一','科目二','科目三', '科目四', '扣满12分'],
				

			}
		},
		computed: {
			selVal() {
				return this.queTypeArrs[this.queType]
			}
		},
		methods: {
			toUrl(type) {
				if(type === 2){
					uni.navigateTo({
						url: '/pages/test/start?type='+type,
					});
				}else{
					uni.navigateTo({
						url: '/pages/answer/answer?type='+type,
					});
				}
				
			},
			changeCarType(val){
				this.carName = this.typeArrs[val].name
				this.cartype = this.typeArrs[val].value
				// this.close()
				
			},
			changeType(val) {
				this.queType = val
				this.setNav(this.queTypeArrs[this.queType])
			},
			setNav(val) {
				uni.setNavigationBarTitle({
					title: val
				});
			},
			open() {
				console.log('open')
				this.$refs.popup.open('top')
			},
			close() {
				this.$refs.popup.close()
			}


		},
		onReady() {
			this.setNav('科目一')
		}
	}
</script>

<style lang="less" scoped>
	.contanier {
		padding: 10px;

		.carClass {
			color: #00ff44;
		}
		.carWrap{
			margin-bottom: 18px;
		}
		.cartype{
			
			color: #000;
		}
		.carActive{
			
			background: aliceblue;
			color: #0068f6;
		}
		.drives{
			text-align: center;
			padding:10px;
			margin:5px;
			// color: lightskyblue;
		}
		

		.btn {
			button {
				margin: 0 5px;
			}
		}
	}
</style>
