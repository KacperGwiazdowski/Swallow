<template>
  <div>
    <div class="content">
      <table class="table">
        <thead class="thead-light">
          <tr>
            <th>Datetime</th>
            <th>Value</th>
            <th v-if="isAdmin">Edit</th>
          </tr>
        </thead>
        <tr v-for="record in data" :key="record.datetime">
          <td>{{presentDateTime(record.datetime)}}</td>
          <td>{{record.value}}</td>
          <td v-if="isAdmin">
            <button 
              class="btn btn-primary" 
              type="button" 
              data-toggle="modal" 
              data-target="#exampleModal" 
              v-on:click="selectRecord(record)">Edit
            </button>
          </td>
        </tr>
      </table>
    </div>
    <DataEdit v-bind:dataRecord="selectedRecord" ></DataEdit>
  </div>
</template>

<script>
import DataEdit from "./DataEdit"
import { authHeader, isAdmin } from "../_helpers";
import config from "../config";
import moment from "moment"
function handleResponse(response) {
  return response.text().then(text => {
    var data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        location.reload(true);
      }
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }
    return data;
  });
}

export default {
  name: "DataTable",
  components: {DataEdit},
  props: ["selectedSensorId", "selectedDate"],
  data() {
    return {
      selectedRecord: null,
      requestOptions: { method: "GET", headers: authHeader() },
      data: [],
      isAdmin: isAdmin()
    };
  },
  watch: {
    selectedDate: {
      immediate: true,
      handler(val, oldVal) {
        this.getData();
      }
    }
  },
  methods: {
    selectRecord(record){
      this.selectedRecord = record;
    },
    presentDateTime(dateTime){
    var date = new Date(dateTime);
  
  return moment(date).format("DD.MM.YYYY hh:mm a");
},
    describeData(data) {
      this.data = data;
    },
    getData() {
      return fetch(
        config.backendUrl +
          `/MeasurmentData/GetSinceDate/?sinceDate=${this.selectedDate}&sensorId=${this.selectedSensorId}`,
        this.requestOptions
      )
        .then(handleResponse)
        .then(this.describeData);
    }
  }
};
</script>

<style scoped>
</style>
