import request from '@/utils/request'
import api from './api'
export function getCarTypeList(query = {}) {
  return request({
    url: api.api.CarType.list,
    method: 'post',
    data: query
  })
}

export function getSubjectTypeList(query = {}) {
  return request({
    url: api.api.SubjectType.list,
    method: 'post',
    data: query
  })
}
