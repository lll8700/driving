import request from '@/utils/request'
import api from './api'
export function fetchList(query) {
  return request({
    url: api.api.Practice.list,
    method: 'post',
    data: query
  })
}
