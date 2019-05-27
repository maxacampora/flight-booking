import Vue from 'vue'
import Router from 'vue-router'
import Hello from '@/components/Hello'
import CityRecords from '@/components/CityRecords'
import AirlineRecords from '@/components/AirlineRecords'
import AirportRecords from '@/components/AirportRecords'
import FlightRecords from '@/components/FlightRecords'
import WeightRecords from '@/components/WeightRecords'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/cities',
      name: 'CityRecords',
      component: CityRecords,
    },
    {
  	  path: '/airlines',
  	  name: 'AirlineRecords',
  	  component: AirlineRecords,
  	},
    {
  	  path: '/airports',
  	  name: 'AirportRecords',
  	  component: AirportRecords,
  	},
    {
  	  path: '/flights',
  	  name: 'FlightRecords',
  	  component: FlightRecords,
  	}
    ,
    {
  	  path: '/weight',
  	  name: 'WeightRecords',
  	  component: WeightRecords,
  	}
  ]
})
