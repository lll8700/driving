<template>
    <view class="component-view-tab">
		<scroll-nav :fixed="tabFix" ref="scrollNav" :background="background" :scrollData="scrollData" :activeKey.sync="activeKey" @change="changeTab" @tabClick="tabClick" :animate="animate" :height="height" :line="line" :font="font"></scroll-nav>
		<view class="scroll-view">
			<view :class="['tab-wrapper',{animate}]" :style="[{left:tabview.left+'px'}]">
				<view class="tab" v-for="(item,key) in scrollData" :key="key">
					<view v-if="forceRender ? true :activeKey === key" class="tab-content" @touchstart="touchStart" @touchmove="touchMove" @touchend="touchEnd($event,key)">
						<slot name="{{item.slot}}"/>
					</view>
				</view>
			</view>
		</view>
	</view>
</template>
<script>
import scrollNav from './ScrollNav';
export default {
    components: {scrollNav},
    props: {
		navData:{
			default:()=>[],
			type:Array
		},
		value:{
			default:0,
			type:Number
		},
		animate:{
			default:true,
			type:Boolean
		},
		scrollThreshold:{
			default:50,
			type:Number
		},
		height:{
			default:'70rpx',
			type:String
		},
		line:Object,
		font:Object,
		background:String,
		forceRender:{
			default:false,
			type:Boolean
		},
		tabFix:Object
	},
    // mixins: [require('@/components/mixins/Component').default],
    data() {
        return {
			tabview:{
				left:0
			},
			scrollData:[],												
            activeKey:0
		};
    },
	emits:['change','update:value','tabClick','rendered'],
    created() {
	},
    mounted() {},
    unmounted() {},
    computed: {
	},
    watch: {
		navData:{
			handler(n){
				this.scrollData = n;
				this.$nextTick(this.resizeStyle);
			},
			deep:true,
			immediate:true
		},
		value:{
			handler(n){
				this.setPosition(n,'set');
			},
			deep:true,
			immediate:true
		}
	},
    methods: {
		async init(){
		},
		load(){},
		$document(self, select, cfg) {
			return new Promise((res, rej) => {
				if (!self || !select) rej('$document:params error');
				uni.createSelectorQuery().in(self).select(select).fields(Object.assign({
					id: true,
					dataset: true,
					rect:true,
					size: true,
					scrollOffset: true,
					context:true
				},cfg),data=>res(data)).exec();
			})
		},
		async changeTab(e){
			await this.setPosition(e);
        },
		tabClick(e){
			this.$emit('tabClick',this.handleItem(e));
		},
		handleItem(e){
			return {
				index:e,
				item:this.scrollData[e]
			}
		},
		touchStart(e){
			if(this.locked) return;
			var client = e.touches[0];
			Object.assign(this,{
				client,
				position:[client]
			});
		},
		touchMove(e){
			if(!this.position) return;
			var client = e.touches[0];
			if(Math.abs(client.clientY - this.client.clientY) < 25){
				this.position.push(client);
				const 
					positions = this.position.map(item=>Object.assign({},item)),
					sorted = positions.sort((a,b)=>a.clientX - b.clientX);
				if(sorted[0].clientX !== client.clientX && sorted.slice(-1)[0].clientX !== client.clientX){
					this.clearPosition();
				}
			}else{
				this.clearPosition();
			}
		},
		async touchEnd(e,key){
			var 
				positions = this.position,
				act;
			if(positions){
				var move = positions.slice(-1)[0].clientX - positions[0].clientX;
				if(Math.abs(move) < this.scrollThreshold) return;
				act = move > 0 ? 1 : -1;
				var changeKey = key - act;
				if(changeKey < 0 || changeKey > this.navData.length - 1) return;
				await this.setPosition(changeKey);
				this.clearPosition();
			}
		},
		async setPosition(key,act){
			this.locked = true;
			switch(act){
				case 'set':
					break;
				default:
					this.$emit('update:value',key);
					this.$emit('change',this.handleItem(key));
			}
			this.activeKey = key;
			try{
				!this.itemWidth && (this.itemWidth = (await this.$document(this,'.tab')).width);
				this.tabview.left = -key * this.itemWidth;
			}catch(e){}
			await new Promise(r=>setTimeout(r,300));
			this.locked && (this.locked = false);
		},
		clearPosition(){
			this.position = null;
		},
		resizeStyle(){
			this.$refs.scrollNav.changeStyle(this.activeKey);
		}
	},
    directives: {},
    errorCaptured() {},
    renderTracked() {},
    renderTriggered() {},
};
</script>
<style lang='stylus'>
.component-view-tab
	width 100%;overflow auto;
	.scroll-view
		overflow hidden;width 100%;max-width 100%;
		.tab-wrapper
			width 100%;display flex;felx-wrap:nowrap;position relative;
			&.animate
				transition all .3s;
			.tab
				min-width 100%;max-width 100%;
				.tab-content
					overflow hidden;width 100%;height 100%;
</style>