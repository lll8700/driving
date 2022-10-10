<template>
	<view class="contanier">
		<uni-list>
			<uni-list-item title="当前题库">
				<template v-slot:footer class="carClass">
					<span>{{CarType.name}} - {{Subject.name}}</span>
				</template>
			</uni-list-item>
		</uni-list>
		<uni-section title="驾驶证类型" type="line">
			<uni-row class="demo-uni-row">
				<uni-col v-for="(citem,cidx) in typeArrs" :key="cidx" :span="8" class="driveWrap">
					<view @click="changeCarType(citem)" class="drives" :class="CarType.id === citem.id ?'carActive':''">
						<span style='font-size: 36px;color: skyblue;' class="iconfont" :class="citem.icon"></span>
						<br />{{citem.name}}<br /> {{citem.subname}}
					</view>
				</uni-col>
			</uni-row>
		</uni-section>
		<uni-section title="切换题库" type="line">
			<uni-card :is-shadow="false" class="btn">
				<uni-row class="demo-uni-row">
					<uni-col v-for="(t,index) in queTypeArrs" :key="index" :span="6" class="carWrap">
						<uni-tag class="cartype" @click="changeType(t)" :inverted="true" :text="t.name" type="primary"
							:class="Subject.id === t.id ?'carActive':''" />
					</uni-col>
				</uni-row>

			</uni-card>
		</uni-section>
		<uni-section title="题库练习" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button v-if="userDto.phone" class="mini-btn" @click="toUrl(1)" @getphonenumber="all" size="mini"
					plain="true">全量题库</button>
				<!-- <button v-else class="mini-btn" open-type="getPhoneNumber" @getphonenumber="all" size="mini"
					plain="true">全量题库</button> -->
			</uni-card>
		</uni-section>
		<uni-section title="模拟考试" type="line">
			<uni-card :is-shadow="false" class="btn">
				<button v-if="userDto.phone" class="mini-btn" @click="toUrl(2)" @getphonenumber="all" size="mini"
					plain="true">开始考试</button>
				<!-- <button v-else class="mini-btn" open-type="getPhoneNumber" @getphonenumber="test" size="mini"
					plain="true">开始考试</button> -->

			</uni-card>
		</uni-section>


	</view>
</template>

<script>
	export default {
		data() {
			return {
				CarType: {
					name: '',
					id: '',
					icon: ''
				}, // 选择的车型
				Subject: {
					name: ''
				}, // 选择的科目
				typeArrs: [],
				queTypeArrs: [],
				userDto: {},
			}
		},
		mounted() {
			this.initData();
		},
		methods: {
			initData() {
				var that = this;
				
				var token = uni.getStorageSync("Token");
				var user = uni.getStorageSync("User");
				
					that.userDto = user;
				
					that.$http(that.$api.CarType.list, "POST", {
						Sorting: "CreationTime"
					}).then(res => {
						that.typeArrs = res.data.items;
						that.CarType = that.typeArrs[0]
					})
					that.$http(that.$api.SubjectType.list, 'POST', {
						Sorting: "CreationTime"
					}).then(res => {
						that.queTypeArrs = res.data.items;
						that.Subject = that.queTypeArrs[0]
					})
				
			},
			toUrl(type) {
				var that = this;
				if (type === 2) {
					uni.navigateTo({
						url: '/pages/test/start?type=' + type + '&carType=' + that.CarType.id + '&subject=' + that
							.Subject.id,
					});
				} else {
					uni.navigateTo({
						url: '/pages/answer/answer?type=' + type + '&carType=' + that.CarType.id + '&subject=' +
							that.Subject.id,
					});
				}

			},
			changeCarType(item) {
				this.CarType = item
			},
			changeType(item) {
				this.Subject = item
				this.setNav(item)
			},
			setNav(val) {
				uni.setNavigationBarTitle({
					title: val.name
				});
			},
			open() {
				console.log('open')
				this.$refs.popup.open('top')
			},
			close() {
				this.$refs.popup.close()
			},
			all(e) {
				var that = this;
				uni.login({
					success(data) {
						console.log(data)
						if (e.detail.errMsg == 'getPhoneNumber:fail user deny') {
							console.log(e.detail)
							return;
						}
						console.log(e.detail)
						var input = {
							userId: that.userDto.id,
							code: data.code,
							encryptedData: e.detail.encryptedData,
							iv: e.detail.iv
						}
						that.$http(that.$api.login.phone, "POST", input).then(res => {
							if (res.data.data) {
								that.userDto = res.data.data;
								uni.setStorageSync("User", that.userDto);
								that.toUrl(1)
							}
						})
					}
				})

			},
			test(e) {
				var that = this;
				uni.login({
					success(data) {
						console.log(data)
						if (e.detail.errMsg == 'getPhoneNumber:fail user deny') {
							console.log(e.detail)
							return;
						}
						console.log(e.detail)
						var input = {
							userId: that.userDto.id,
							code: data.code,
							encryptedData: e.detail.encryptedData,
							iv: e.detail.iv
						}
						that.$http(that.$api.login.phone, "POST", input).then(res => {
							if (res.data.data) {
								that.userDto = res.data.data;
								console.log(that.userDto)
								uni.setStorageSync("User", that.userDto);
								that.toUrl(2)
							}
						})
					}
				})

			},

		},
		onReady() {
			this.setNav('科目一')
		},


	}
</script>

<style lang="less" scoped>
	.contanier {
		padding: 10px;

		.carClass {
			color: #00ff44;
		}

		.carWrap {
			margin-bottom: 18px;
		}

		.cartype {

			color: #000;
		}

		.carActive {

			background: aliceblue;
			color: #0068f6;
		}

		.drives {
			text-align: center;
			padding: 10px;
			margin: 5px;
			// color: lightskyblue;
		}


		.btn {
			button {
				margin: 0 5px;
			}
		}
	}
</style>
