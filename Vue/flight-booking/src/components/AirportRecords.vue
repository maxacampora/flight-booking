<template>
<div class="container-fluid mt-4">
  <h1 class="h1">Aereoporti</h1>
  <b-alert :show="loading" variant="info">Caricamento...</b-alert>
  <b-row>
    <b-col>
      <table class="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Codice</th>
            <th>Città</th>
            <th>&nbsp;</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="record in records" :key="record.id">
            <td>{{ record.id }}</td>
            <td>{{ record.name }}</td>
            <td>{{ record.code }}</td>
            <td>{{ record.city.name }}</td>
            <td class="text-right">
              <a href="#" @click.prevent="updateAirportRecord(record)">Modifica</a> -
              <a href="#" @click.prevent="deleteAirportRecord(record.id)">Cancella</a>
            </td>
          </tr>
        </tbody>
      </table>
    </b-col>
    <b-col lg="3">
      <b-card :title="(model.id ? 'Modifica Aereoporto ID#' + model.id : 'Nuovo Aereoporto')">
        <form @submit.prevent="createAirportRecord">
          <b-form-group label="Nome">
            <b-form-input type="text" v-model="model.name"></b-form-input>
          </b-form-group>
          <b-form-group label="Codice">
            <b-form-input rows="4" v-model="model.code" type="text"></b-form-input>
          </b-form-group>
          <b-form-group label="Città">
            <div rows="8">
              <!--<vue-bootstrap-typeahead class="mb-4" v-model="query" :data="cities" :serializer="item => item.login" @hit="selectedCity = $event" placeholder="Cerca città" />-->
              <vue-bootstrap-typeahead ref="cityAutocomplete" class="mb-4" v-model="query" :data="cities" :serializer="city => city.name" @hit="selectedCity = $event" placeholder="Cerca città" />
            </div>
            <!--<b-form-input rows="8" v-model="model.city" type="text"></b-form-input>-->
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
import api from '@/AirportApiService';
import cityApi from '@/CityApiService';

export default {
  data() {
    return {
      loading: false,
      records: [],
      model: {},
      query: '',
      selectedCity: null,
      cities: []
    };
  },
  async created() {
    this.getAll()
  },
  watch: {
    // When the query value changes, fetch new results from
    // the API - in practice this action should be debounced
    async query(newQuery) {
      console.log(newQuery);
      //this.cities = await cityApi.getLike(`?q=${newQuery}`);
      try {
        this.cities = await cityApi.getLike(`?q=${newQuery}`);
      } catch (e) {
        console.log(e);
      } finally {}
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
        this.records = await api.getAll();
        //console.log(this.records);
      } finally {
        this.loading = false
      }
    },
    async updateAirportRecord(airportRecord) {
      // you use Object.assign() to create a new (separate) instance
      this.model = Object.assign({}, airportRecord)
    },
    async createAirportRecord() {
      const isUpdate = !!this.model.id;
      this.model.city = this.selectedCity;
      if (isUpdate) {
        await api.update(this.model.id, this.model)
      } else {
        await api.create(this.model)
      }

      // Clear the data inside of the form
      this.model = {};
      this.query = '';
      this.selectedCity = null;
      this.cities = [];
      this.$refs.cityAutocomplete.inputValue = ''
      // Fetch all records again to have latest data
      await this.getAll()
    },
    async deleteAirportRecord(id) {
      if (confirm('Are you sure you want to delete this record?')) {
        // if you are editing a weight record you deleted, remove it from the form
        if (this.model.id === id) {
          this.model = {};
          this.query = '';
          this.selectedCity = null;
          this.cities = [];
          this.$refs.cityAutocomplete.inputValue = ''
        }

        await api.delete(id)
        await this.getAll()
      }
    }
  }
}
</script>
