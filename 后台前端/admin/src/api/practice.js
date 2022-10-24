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
    data: query,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

export function deleteItem(query) {
  return request({
    url: api.api.Practice.delete + '?id=' + query.id,
    method: 'post',
    data: query
  })
}
export function create(query) {
  return request({
    url: api.api.Practice.create,
    method: 'post',
    data: query
  })
}
