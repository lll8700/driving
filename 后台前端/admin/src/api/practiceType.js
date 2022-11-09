import request from '@/utils/request'
import api from './api'
export function getTypeList(query = {}) {
  return request({
    url: api.api.PracticeType.list,
    method: 'post',
    data: query
  })
}
export function createType(query) {
  return request({
    url: api.api.PracticeType.create,
    method: 'post',
    data: query
  })
}
export function deleteType(query) {
  return request({
    url: api.api.PracticeType.delete + '?id=' + query.id,
    method: 'post',
    data: query
  })
}

