<template>
<div class="container-fluid mt-4">
  <h1 class="h1">Voli</h1>
  <b-alert :show="loading" variant="info">Caricamento...</b-alert>
  <b-row>
    <b-col>
      <table class="table table-striped">
        <thead>
          <tr>
            <th>Codice Volo</th>
            <th>Aereoporto Partenza</th>
            <th>Aereoporto Arrivo</th>
            <th>Data Partenza</th>
            <th>Data Arrivo</th>
            <th>Compagnia Aerea</th>
            <th>Numero posti</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="record in records" :key="record.id">
            <td>{{ record.flightCode }}</td>
            <td>{{ record.departure.code }}</td>
            <td>{{ record.arrival.code }}</td>
            <td>{{ record.departureDate }}</td>
            <td>{{ record.arrivalDate }}</td>
            <td>{{ record.airline.name }}</td>
            <td>{{ record.numOfSeat }}</td>
            <td class="text-right">
              <a href="#" @click.prevent="updateFlightRecord(record)">Modifica</a> -
              <a href="#" @click.prevent="deleteAirlineRecord(record.id)">Cancella</a>
            </td>
          </tr>
        </tbody>
      </table>
    </b-col>
    <b-col lg="3">
      <b-card :title="(model.id ? 'Modifica Volo ID#' + model.id : 'Nuovo Volo')">
        <form @submit.prevent="createFlightRecord">
          <b-form-group label="Codice Volo">
            <b-form-input type="text" v-model="model.flightCode"></b-form-input>
          </b-form-group>
          <b-form-group label="Aereoporto Partenza">
            <div>
              <vue-bootstrap-typeahead ref="departureAutocomplete" class="mb-4" v-model="depQuery" :data="departures" :serializer="airport => airport.name" @hit="selectedDeparture = $event" placeholder="Cerca aereoporto partenza" />
            </div>
          </b-form-group>

          <b-form-group label="Aereoporto Destinazione">
            <div>
              <vue-bootstrap-typeahead ref="arrivalAutocomplete" class="mb-4" v-model="arrQuery" :data="arrivals" :serializer="airport => airport.name" @hit="selectedArrival = $event" placeholder="Cerca aereoporto destinazione" />
            </div>
          </b-form-group>
          <b-form-group label="Data Partenza">
            <b-form-input raws="4" v-model="model.departureDate" type="datetime-local"></b-form-input>
          </b-form-group>
          <!--
          <b-form-group label="Ora Partenza">
            <b-form-input raws="4" v-model="model.departureTime" type="time"></b-form-input>
          </b-form-group>
          -->
          <b-form-group label="Data Arrivo">
            <b-form-input raws="4" v-model="model.arrivalDate" type="datetime-local"></b-form-input>
          </b-form-group>
          <!--
          <b-form-group label="Ora Arrivo">
            <b-form-input raws="4" v-model="model.arrivalTime" type="time"></b-form-input>
          </b-form-group>
          -->
          <b-form-group label="Compagnia Aerea">
            <div>
              <vue-bootstrap-typeahead ref="airlineAutocomplete" class="mb-4" v-model="airQuery" :data="airlines" :serializer="airline => airline.name" @hit="selectedAirline = $event" placeholder="Cerca compagnia aerea" />
            </div>
          </b-form-group>
          <b-form-group label="Numero Posti">
            <b-form-input v-model="model.numOfSeat" type="number"></b-form-input>
          </b-form-group>
          <div>
            <b-btn type="submit" variant="success">Salva Record</b-btn>
          </div>
        </form>
      </b-card>
    </b-col>
  </b-row>
</div>
</template>

<script>
import api from '@/FlightApiService';
import airportApi from '@/AirportApiService';
import airlineApi from '@/AirlineApiService';



export default {
  data() {
    return {
      loading: false,
      records: [],
      model: {},
      // for departure airport autocomplete
      depQuery: '',
      selectedDeparture: null,
      departures: [],
      // for departure airport autocomplete
      arrQuery: '',
      selectedArrival: null,
      arrivals: [],
      // for departure airline autocomplete
      airQuery: '',
      selectedAirline: null,
      airlines: []
    };
  },
  async created() {
    this.getAll()
  },
  watch: {
    // When the depQuery value changes, fetch new results from
    // the API
    async depQuery(newQuery) {
      //console.log(newQuery);
      try {
        this.departures = await airportApi.getLike(`?q=${newQuery}`);
      } catch (e) {
        console.log(e);
      } finally {
        //console.log(this.departures);
      }
    },
    async arrQuery(newQuery) {
      //console.log(newQuery);
      try {
        this.arrivals = await airportApi.getLike(`?q=${newQuery}`);
      } catch (e) {
        console.log(e);
      } finally {
        //console.log(this.arrivals);
      }
    },
    async airQuery(newQuery) {
      //console.log(newQuery);
      try {
        this.airlines = await airlineApi.getLike(`?q=${newQuery}`);
      } catch (e) {
        console.log(e);
      } finally {
        //console.log(this.airlines);
      }
    }
  },
  filters: {
    stringify(value) {
      return JSON.stringify(value, null, 2)
    }
  },
  methods: {
    async getAll() {
      this.loading = true

      try {
        this.records = await api.getAll()
      } finally {
        this.loading = false
      }
    },
    async updateFlightRecord(airlineRecord) {
      // you use Object.assign() to create a new (separate) instance
      this.model = Object.assign({}, airlineRecord)
    },
    async createFlightRecord() {
      const isUpdate = !!this.model.id;
      this.model.airline = this.selectedAirline;
      this.model.departure = this.selectedDeparture;
      this.model.arrival = this.selectedArrival;
      //console.log(this.model.departureTime);
      //console.log(this.model.arrivalTime);
      if (isUpdate) {
        await api.update(this.model.id, this.model)
      } else {
        await api.create(this.model)
      }

      // Clear the data inside of the form
      this.model = {};
      this.depQuery = '';
      this.selectedDeparture = null;
      this.departures = [];
      this.$refs.departureAutocomplete.inputValue = '';

      // for departure airport autocomplete
      this.arrQuery = '';
      this.selectedArrival = null;
      this.arrivals = [];
      this.$refs.arrivalAutocomplete.inputValue = ''

      // for departure airline autocomplete
      this.airQuery = '';
      this.selectedAirline = null;
      this.airlines = [];
      this.$refs.airlineAutocomplete.inputValue = ''

      // Fetch all records again to have latest data
      await this.getAll()
    },
    async deleteAirlineRecord(id) {
      if (confirm('Are you sure you want to delete this record?')) {
        // if you are editing a weight record you deleted, remove it from the form
        if (this.model.id === id) {
          this.model = {}
        }

        await api.delete(id)
        await this.getAll()
      }
    }
  }
}
</script>
