import request from '@/utils/request'
import api from './api'
export function login(data) {
  var input = {
    phoneNumber: data.username,
    password: data.password
  }
  return request({
    url: api.api.login.token,
    method: 'post',
    data: input
  })
}

export function getInfo() {
  return request({
    url: api.api.login.info,
    method: 'get'
  })
}

export function logout() {
  return true
}
