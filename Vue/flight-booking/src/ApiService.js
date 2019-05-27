import Vue from 'vue'
import axios from 'axios'


export default {
  async execute(method, resource, data, client) {
    //const accessToken = await Vue.prototype.$auth.getAccessToken()
    return client({
      method,
      url: resource,
      data,
    //  headers: {
    //    Authorization: `Bearer ${accessToken}`
    //  }
    }).then(req => {
      return req.data
    })
  }
}
