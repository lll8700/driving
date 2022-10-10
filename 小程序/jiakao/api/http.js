

export function get(url,params={},requestheader,isshowLoading=false){
 if (isshowLoading) {  // 是否打开加载缓冲
	 	uni.showLoading({
	 		title: '加载数据中',
	 		mask: true
	 	});
	  }
	 if (!requestheader) {
		 var token = uni.getStorageSync("Token");
	 	requestheader = {
	 		'content-type': 'application/json; charset=utf-8',
			'Authorization': token
	 	};
	 };
	 return new Promise((resolve,reject) => {
		uni.request({
	 	url: url,
	 	data: params || {},
	 	method: 'GET',
	 	header: requestheader,
	 	success: function(res) {
	 		switch (res.statusCode) {
	 			case 200:
	 				resolve(res.data);
	 				break;
	 			case 401:
					uni.setStorageSync("Token", null);
	 				uni.showToast({
	 					title: '未登录或者登录超时，请登录',
	 					icon: 'none',
	 					duration: 2000
	 				});
	 				uni.switchTab({
	 					url: '/pages/login/login'
	 				});
	 				break;
	 			default:
	 				 reject(err)
	 				break;
	 		}
	 	},
	 	fail: function(res) {
			console.error(res);
	 	},
	 	complete: function() {
	 		 if (isshowLoading) {
	 		 	uni.hideLoading();
	 		 }
	 	}
	 });
})
}


/**
 * 封装post请求
 * @param url  地址
 * @param data 参数
 * @returns {Promise} 返回数据
 */
 export function post(url,data = {},requestheader,isshowLoading=false) {
	 
	 if (isshowLoading) {  // 是否打开加载缓冲
	 	uni.showLoading({
	 		title: '加载数据中',
	 		mask: true
	 	});
	  }
	 if (!requestheader) {
		var token = uni.getStorageSync("Token");
	 	requestheader = {
	 		'content-type': 'application/json; charset=utf-8',
	 		'Authorization': token
	 	};
	 };
	 return new Promise((resolve,reject) => {
		uni.request({
	 	url: url,
	 	data: data || {},
	 	method: 'POST',
	 	header: requestheader,
	 	success: function(res) {
	 		switch (res.statusCode) {
	 			case 200:
	 				resolve(res.data);
	 				break;
	 			case 401:
	 				uni.showToast({
	 					title: '未登录或者登录超时，请登录',
	 					icon: 'none',
	 					duration: 2000
	 				});
	 				uni.switchTab({
	 					url: '/pages/home/home.vue'
	 				});
	 				break;
	 			default:
	 				 reject(err)
	 				break;
	 		}
	 	},
	 	fail: function(res) {
			console.error(res);
	 	},
	 	complete: function() {
	 		 if (isshowLoading) {
	 		 	uni.hideLoading();
	 		 }
	 	}
	 });
})
}

