<template>
<div class="container-fluid mt-4">
  <h1 class="h1">Città</h1>
  <b-alert :show="loading" variant="info">Caricamento...</b-alert>
  <b-row>
    <b-col>
      <table class="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>Città</th>
            <th>Paese</th>
            <th>&nbsp;</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="record in records" :key="record.id">
            <td>{{ record.id }}</td>
            <td>{{ record.name }}</td>
            <td>{{ record.country }}</td>
            <td class="text-right">
              <a href="#" @click.prevent="updateCityRecord(record)">Modifica</a> -
              <a href="#" @click.prevent="deleteCityRecord(record.id)">Cancella</a>
            </td>
          </tr>
        </tbody>
      </table>
    </b-col>
    <b-col lg="3">
      <b-card :title="(model.id ? 'Modifica Città ID#' + model.id : 'Nuova Città')">
        <form @submit.prevent="createCityRecord">
          <b-form-group label="Città">
            <b-form-input type="text" v-model="model.name"></b-form-input>
          </b-form-group>
          <b-form-group label="Paese">
            <b-form-input rows="4" v-model="model.country" type="text"></b-form-input>
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
import api from '@/CityApiService';

export default {
  data() {
    return {
      loading: false,
      records: [],
      model: {}
    };
  },
  async created() {
    this.getAll()
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
    async updateCityRecord(cityRecord) {
      // you use Object.assign() to create a new (separate) instance
      this.model = Object.assign({}, cityRecord)
    },
    async createCityRecord() {
      const isUpdate = !!this.model.id;

      if (isUpdate) {
        await api.update(this.model.id, this.model)
      } else {
        await api.create(this.model)
      }

      // Clear the data inside of the form
      this.model = {}

      // Fetch all records again to have latest data
      await this.getAll()
    },
    async deleteCityRecord(id) {
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
