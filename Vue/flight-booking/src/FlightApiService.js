import Vue from 'vue'
import axios from 'axios'

import api from '@/ApiService';

const client = axios.create({
  baseURL: 'http://localhost:5000/api/Flights',
  json: true
})

export default {
  async execute(method, resource, data) {
    return api.execute(method, resource, data, client);
  },
  getAll() {
    return this.execute('get', '/')
  },
  create(data) {
    return this.execute('post', '/', data)
  },
  update(id, data) {
    return this.execute('put', `/${id}`, data)
  },
  delete(id) {
    return this.execute('delete', `/${id}`)
  }
}
