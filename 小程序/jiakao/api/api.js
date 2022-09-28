 //这里存放所有api路径
 
const portUrl = 'https://localhost:44312/api' //正式地址
// const portUrl = "/api";
var api = {
	// 车型
	CarType: {
		list: portUrl + '/CarType/' + 'list' 
	},
	// 科目
	SubjectType: {
		list: portUrl + '/SubjectType/' + 'list' 
	},
	// 枚举
	Enums: {
		getenum: portUrl +  '/Enum/' + "getenum"
	},
	// 登录
	login: {
		token:  portUrl +  '/login/' + "gettoken",
		phone:  portUrl +  '/login/' + "phone"
	},
	// 
	Practice: {
		list: portUrl + '/Practice/' + 'list' 
	},
}

module.exports = {
	api
}