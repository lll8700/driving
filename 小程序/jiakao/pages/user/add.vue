<template>
	<view class="addContain">
		<!-- 新增账号 -->
		<uni-section title="账号信息" type="line">
		<uni-card title="新增账号">
			<form  class="forms">
					<view class="uni-forms-item uni-column">
						<view class="uni-forms-item uni-column list">
							<view  class="t">输入手机号</view>
							<input class="c" v-model="accout.phone" maxlength="11" placeholder="请输入手机号" />
						</view>
						<view class="uni-forms-item uni-column list">
							<view class="t">请输入密码</view>
							<input class="c" type="password" v-model="accout.password" placeholder="请输入密码" />
						</view>
					</view>
				
			
				</form>
		</uni-card>
		</uni-section>
		<uni-section title="权限信息" type="line">
			<uni-card title="选择考试科目" class="btn">
				<button  type="primary" class="mini-btn cbtn"  size="mini"
					plain="true">科目一</button>
				
			</uni-card>
			<uni-card title="选择考试车型" class="btn">
				<button  type="primary" class="mini-btn cbtn"  size="mini"
					plain="true">小车</button>
				
			</uni-card>
		</uni-section>
		<uni-section title="会员信息" type="line">
		<uni-card title="最低价">
			<text>40 元</text>
		</uni-card>
		</uni-section>
		<uni-section title="备注" type="line">
			<uni-card >
				<textarea  placeholder="请输入备注" id="" cols="30" rows="10"></textarea>
			</uni-card>
			
			<!-- <input type="textarea" name="" id=""> -->
		</uni-section>
		<view class="addbtn">
			<button  type="primary" class="mini-btn cbtn "  size="default"
				plain="true">确认开通</button>
		</view>
		
	</view>
</template>

<script>
	export default {
		data() {
			return {
				accout:{
					phone:'',
					password:''
				}
			}
		},
		methods: {
			
		}
	}
</script>

<style lang="less" scoped>
	.addContain{
		padding:10px;
	}
	.list{
		padding: 10px auto;
		line-height: 1.1rem;
		margin:20px auto;
	}
	.addbtn{
		width: 80%;
		margin:10px 10%;
		
	}

</style>
