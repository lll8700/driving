import request from '@/utils/request'
import api from './api'
export function fetchList(query) {
  return request({
    url: api.api.Practice.list,
    method: 'post',
    data: query
  })
}

export function exlcel(query) {
  return request({
    url: api.api.Practice.excel,
    method: 'post',
    data: query
  })
}
export function uploadZip(query) {
  return request({
    url: api.api.Practice.zip,
    method: 'post',
    data: query
  })
}
