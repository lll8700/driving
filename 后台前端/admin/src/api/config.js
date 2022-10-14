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

export function getEnum(className) {
  return request({
    url: api.api.Enums.getenum + '?className=' + className,
    method: 'get'
  })
}

export function createSub(query) {
  return request({
    url: api.api.SubjectType.create,
    method: 'post',
    data: query
  })
}
export function deleteSub(query) {
  return request({
    url: api.api.SubjectType.delete + '?id=' + query.id,
    method: 'post',
    data: query
  })
}

export function createCar(query) {
  return request({
    url: api.api.CarType.create,
    method: 'post',
    data: query
  })
}
export function deleteCar(query) {
  return request({
    url: api.api.CarType.delete + '?id=' + query.id,
    method: 'post',
    data: query
  })
}

