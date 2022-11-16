// 对uni.request进行封装
/* 
   url            请求地址
   method         请求方式
   data           请求参数
   contentType    请求内容类型 1=json  2=form
 */ 
export function request(url,method="get",data,contentType=1) {
	var token = uni.getStorageSync("Token");
	var routes  = getCurrentPages()
	let curRoute = routes[routes.length - 1].route //获取当前页面路由
	if(curRoute !== 'pages/login/login' && (!token || token.length === 0)) {
		uni.setStorageSync("Token", null);
		uni.showToast({
			icon:"none",
			title: '未登入或登入状态已超时',
			duration: 1500
		})
		uni.redirectTo({
			url: '/pages/login/login'
		});
		return
	}
	
	let header = {
		'content-type': contentType === 1? 'application/json':'application/x-www-from-urlencoded',
		'Authorization': token
	}
	return new Promise((resolve,reject) => {
		uni.request({
			url:url,
			method,
			data,
			header,
			success: (res) => {
				if (res.statusCode == 200) {
					// 请求成功
					resolve(res)
				} else if (res.statusCode == 401) {
					uni.setStorageSync("Token", null);
					uni.showToast({
						icon:"none",
						title: '未登入或登入状态已超时',
						duration: 1500
					})
				} else if(res.statusCode == 405) {
					uni.showToast({
						icon:"none",
						title: '请求方式错误',
						duration: 1500
					})
				} else if(res.statusCode == 500)  {
					var str = '错误:服务端请求错误'
					if(res.data) {
						var ex = res.data.split(':')
						if(ex.length > 1) {
							var ex1 =ex[1].split('at')
							str = '错误:' + ex1[0]
						}
					}
					
					
					uni.showToast({
						icon:"none",
						title: str,
						duration: 1500
					})
				}else {
					uni.showToast({
						icon:"none",
						title: '错误：' + res.statusCode,
						duration: 1500
					})
				}
			},
			
			fail: (err) => {
				console.log('err',err);
				uni.showToast({
					icon:"none",
					title: err.errMsg,
					duration: 1500
				})
				reject(err);
			}
		})
		
	})
}