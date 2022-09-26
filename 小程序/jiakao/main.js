import App from './App'
import {
	post,
	get
} from './api/http'

import {request} from './api/request'
import {
	api
} from './api/api.js'

// #ifndef VUE3
import Vue from 'vue'
Vue.config.productionTip = false

//定义全局变量
Vue.prototype.$post = post;
Vue.prototype.$get = get;
Vue.prototype.$api = api;
Vue.prototype.$http = request;

App.mpType = 'app'
const app = new Vue({
	...App
})



app.$mount()
// #endif

// #ifdef VUE3
import {
	createSSRApp
} from 'vue'

export function createApp() {
	const app = createSSRApp(App)

	return {
		app
	}
}
// #endif
