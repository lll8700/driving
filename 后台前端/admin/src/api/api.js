// 这里存放所有api路径

// const portUrl = 'http://106.14.209.175:801/api' // 正式地址
// const portUrl = 'https://localhost:44312/api' // 本地地址
const portUrl = '/api'
var api = {
  // 车型
  CarType: {
    list: portUrl + '/CarType/' + 'list',
    create: portUrl + '/CarType/' + 'create',
    delete: portUrl + '/CarType/' + 'delete',
    update: portUrl + '/CarType/' + 'update'
  },
  // 科目
  SubjectType: {
    list: portUrl + '/SubjectType/' + 'list',
    create: portUrl + '/SubjectType/' + 'create',
    delete: portUrl + '/SubjectType/' + 'delete',
    update: portUrl + '/SubjectType/' + 'update'
  },
  // 枚举
  Enums: {
    getenum: portUrl + '/Enum/' + 'getenum'
  },
  // 登录
  login: {
    token: portUrl + '/login/' + 'token',
    phone: portUrl + '/login/' + 'phone',
    weblogin: portUrl + '/login/' + 'weblogin',
    info: portUrl + '/login/' + 'info'
  },
  //
  Practice: {
    list: portUrl + '/Practice/' + 'list',
    next: portUrl + '/Practice/' + 'next',
    random: portUrl + '/Practice/' + 'Random',
    excel: portUrl + '/Practice/' + 'excel',
    zip: portUrl + '/Practice/' + 'zip',
    delete: portUrl + '/Practice/' + 'delete',
    create: portUrl + '/Practice/' + 'create'
  },
  user: {
    list: portUrl + '/user/' + 'list',
    create: portUrl + '/user/' + 'create',
    update: portUrl + '/user/' + 'update',
    delete: portUrl + '/user/' + 'delete'
  },
  config: {
    getfist: portUrl + '/config/' + 'getfist',
    update: portUrl + '/config/' + 'update'
  }
}

module.exports = {
  api
}
