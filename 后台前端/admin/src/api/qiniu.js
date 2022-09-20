import request from '@/utils/request'

export function getToken() {
  return request({
    url: '/api/login/token', // 假地址 自行替换
    method: 'get'
  })
}
