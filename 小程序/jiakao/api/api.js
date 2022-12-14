 //这里存放所有api路径
 
// const portUrl = 'http://106.14.209.175:801/api' //正式地址
// const portUrl = 'https://localhost:44312/api' //本地地址
const portUrl = "/api/api";
var api = {
	// 车型
	CarType: {
		list: portUrl + '/CarType/' + 'list' 
	},
	// 科目
	SubjectType: {
		list: portUrl + '/SubjectType/' + 'list' 
	},
	PracticeType: {
		list: portUrl + '/PracticeType/' + 'list' 
	},
	// 枚举
	Enums: {
		getenum: portUrl +  '/Enum/' + "getenum"
	},
	// 登录
	login: {
		token:  portUrl +  '/login/' + "gettoken",
		phone:  portUrl +  '/login/' + "phone",
		weblogin:  portUrl +  '/login/' + "weblogin",
		create:  portUrl +  '/login/' + "create",
	},
	// 
	Practice: {
		list: portUrl + '/Practice/' + 'list' ,
		next: portUrl + '/Practice/' + 'next' ,
		random: portUrl + '/Practice/' + 'Random' ,
		testlist:  portUrl + '/Practice/' + 'testlist' ,
		count: portUrl + '/Practice/' + 'count' 
	},
	config: {
		home:  portUrl + '/config/' + 'Home' ,
		getfist:  portUrl + '/config/' + 'getfist' ,
		jurisdiction:  portUrl + '/config/' + 'jurisdiction' ,
		createjurisdiction: portUrl + '/config/' + 'createjurisdiction'
	},
	user: {
		list: portUrl + '/user/' + 'list' ,
	}
}

module.exports = {
	api
}