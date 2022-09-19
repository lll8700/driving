<template>
    <view class="component-view-scroll-nav" :style="[{position:tabStyle.position,left:tabStyle.left,top:tabStyle.top,bottom:tabStyle.bottom,right:tabStyle.right}]">
		<scroll-view scroll-x :scroll-with-animation="animate" enable-flex class="navbar" :scroll-left="navStyle.scrollLeft" :skip-hidden-item-layout="true" :style="{height,background}">
			<view class="nav-wrapper">
				<view @click="changeTab(key)" :class="['nav-item',{'active':key === modelKey}]" v-for="(item,key) in scrollData" :key="key" :style="{background:fontStyle[key === modelKey?'active':'default']['background']}">
					<text :class="['item-label',{animate}]" v-text="item.label" :style="[{fontSize:fontStyle[key === modelKey?'active':'default']['fontSize'],color:fontStyle[key === modelKey?'active':'default']['color']}]"></text>
				</view>
				<view :class="['line',{animate}]" :style="[{'width':navStyle.width+'px','left':navStyle.left+'px','height':lineStyle.height}]">
					<view class="s" :style="[{width:lineStyle.width,'backgroundColor':lineStyle.backgroundColor}]"></view>
				</view>
			</view>	
		</scroll-view>
	</view>
</template>
<script>
const propStyleMap = {
	line:{
		color:'backgroundColor',
		height:'height',
		width:'width'
	},
	font:{
		default:{
			defaultColor:'color',
			defaultSize:'fontSize',
			defaultBackground:'background'
		},
		active:{
			activeColor:'color',
			activeSize:'fontSize',
			activeBackground:'background'
		}
	},
	fixed:{
		top:'top',
		left:'left',
		bottom:'bottom',
		right:'right'
	}
};
export default {
    components: {},
    props: {
		scrollData:{
			default:[],
			type:Array
		},
		activeKey:{
			default:0,
			type:Number
		},
		animate:{
			default:true,
			type:Boolean
		},
		height:{
			default:'70rpx',
			type:String
		},
		line:{
			default:()=>({}),
			type:Object
		},
		font:{
			default:()=>({}),
			type:Object
		},
		background:{
			default:'transparent',
			type:String
		},
		fixed:{
			default:()=>({}),
			type:Object
		}
	},
	emits:['tabClick','change','update:activeKey','rendered'],
    // mixins: [require('@/components/mixins/Component').default],
    data() {
        return {
			modelKey:null,
			navStyle:{
				width:0,
				left:0,
				scrollLeft:0
			}
		};
    },
    created() {
	},
    mounted() {},
    unmounted() {},
    computed: {
		lineStyle:{
			get(){
				return this.formatProp('line');
			}
		},
		fontStyle:{
			get(){
				const font = this.font;
				var style = {};
				for(var key in propStyleMap.font){
					style[key] = {};
					var cfg = propStyleMap.font[key];
					for(var ck in font){
						ck in cfg && (style[key][cfg[ck]] = font[ck]);
					}
				}
				return style;
			}
		},
		tabStyle:{
			get(){
				const formated = this.formatProp('fixed');
				Object.keys(formated).length && Object.assign(formated,{
					position:'fixed'
				});
				return formated;
			}
		}
	},
    watch: {
		activeKey:{
			handler(n){
				n !== this.modelKey && this.render(n);
			},
			deep:true,
			immediate:true
		}
	},
    methods: {
		init(){

		},
		load(){

		},
		formatProp(prop){
			const oThis = this[prop];
			var props = {};
			for(var key in oThis){
				key in propStyleMap[prop] && (props[propStyleMap[prop][key]] = oThis[key]);
			}
			return props;
		},
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
		async render(e){
			this.move = true;
			this.modelKey = e;
			await this.$nextTick();
			await this.changeStyle(e);
			await new Promise(r=>setTimeout(r,300));
			this.move = false;
		},
		async changeTab(e){
			!this.move && (async()=>{
				this.$emit('tabClick',e);
				if(this.modelKey !== e){
					this.$emit('update:activeKey',e);
					this.$emit('change',e);
					await this.render(e);
				}
			})();
		},
		async changeStyle(e){
			var active = await this.$document(this,'.nav-item.active'),
				scroll = await this.$document(this,'.navbar');
			if(active && scroll){
				var left = active.left+scroll.scrollLeft;
				Object.assign(this.navStyle,{
					width:active.width,
					left,
					...(this.animate ? {scrollLeft:left - scroll.width/2 + active.width/2} : null)
				});
			}
		}
	},
    directives: {},
    errorCaptured() {},
    renderTracked() {},
    renderTriggered() {},
	
};
</script>
<style lang='stylus'>
$animate = {transition:all .3s linear};
.component-view-scroll-nav
	width 100%;z-index 999;
	.navbar
		overflow auto;display flex;width 100%;height 70rpx;position relative;background-color #fff;
		.nav-wrapper
			display flex;position relative;width 100%;
			.line
				position absolute;bottom 0;height 4rpx;
				&.animate
					transition all .3s linear;
				.s
					width 100%;height 100%;margin 0 auto;background-color #02c4c7;border-radius 2rpx;max-width:100%;transition @transition;
		.nav-item
			display flex;align-items center;padding 0 30rpx;white-space nowrap;width 100%;justify-content center;
			&.active
				.item-label
					color #02c4c7;
					&.animate
						transition color .3s linear;
			.item-label
				font-size 24rpx;color #333;
</style>