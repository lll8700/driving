export const question = [{
		seq: 0,
		type: 1, // 1 单选，2 多选
		title: '架势机动车发生交通事故后当事人故意破坏、伪造现场、毁灭证据的，应当承担什么责任？',
		ansArr: [{
			name:'主要责任',
			rightTag:true,
			seq:'A'
		},{
			name: '次要责任',
			rightTag:false,
			seq:'B',
		},{
			name: '同等责任',
			rightTag:false,
			seq:'C',
		} ,{
			name:'全部责任',
			rightTag:false,
			seq:'D',
		}],
		imgArr: [{url:'./static/home.png'}],
		rightAns: {
			rightVal: 'A',
			tec: '判责：非故意都判责；故意、不礼让的负全责；掉货物的负全责',
			keyVal: '故意',
			analysis: '发生交通事故后当事人逃逸、故意破坏、伪造现场毁灭证据的，均承担全部责任。'
		}
	},
	{
		seq: 1,
		type: 2, // 1 单选，2 多选
		title: '架势机动车发生交通事故后当事人故意破坏、伪造现场、毁灭证据的，应当承担什么责任？',
		ansArr: [{
			name:'主要责任',
			rightTag:true,
			seq:'A'
		},{
			name: '次要责任',
			rightTag:true,
			seq:'B',
		},{
			name: '同等责任',
			rightTag:true,
			seq:'C',
		} ,{
			name:'全部责任',
			rightTag:false,
			seq:'D',
		}],
		imgArr: [{
			url:''
		}],
		rightAns: {
			rightVal: 'A,B,C',
			tec: '判责：非故意都判责；故意、不礼让的负全责；掉货物的负全责',
			keyVal: '故意',
			analysis: '发生交通事故后当事人逃逸、故意破坏、伪造现场毁灭证据的，均承担全部责任。'
		},
	}
]
