<template>
  <div>
    <Navbar></Navbar>
    <div class="content">
      <form>
        <div class="form-row">
          <div class="form-group col-md-4">
            <label>Station</label>
            <select
              id="inputState"
              class="form-control"
              v-model="selectedStation"
              v-on:change="getSensors"
            >
              <option
                v-for="station in stations"
                :key="station.id"
                v-bind:value="station.id"
              >{{station.name}}</option>
            </select>
          </div>
          <div class="form-group col-md-4">
            <label>Sensor</label>
            <select id="inputState" class="form-control" v-model="selectedSensor" v-on:change="resetDatetime()">
              <option
                v-for="sensor in sensors"
                :key="sensor.id"
                v-bind:value="sensor.id"
              >{{sensor.param}}</option>
            </select>
          </div>
          <div class="form-group col-md-4">
            <label>Since date</label>
            <input type="date" class="form-control" v-model="selectedDate" />
          </div>
        </div>
      </form>
      <DataTable
        v-bind:selectedSensorId="selectedSensor"
        v-bind:selectedDate="selectedDate"
        v-if="selectedDate"
      ></DataTable>
    </div>
  </div>
</template>

<script>
import Navbar from "../components/navigation/Navbar";
import DataTable from "./DataTable";
import { authHeader } from "../_helpers";
import config from "../config";
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
  components: { Navbar, DataTable },
  name: "DataStatistics",
  data() {
    return {
      stations: [],
      sensors: [],
      selectedStation: 0,
      selectedSensor: 0,
      selectedDate: null,
      requestOptions: { method: "GET", headers: authHeader() }
    };
  },
  methods: {
    resetDatetime(){
      this.selectedDate = null;
    },
    describeStations(data) {
      this.stations = data;
    },
    getStations() {
      return fetch(
        config.backendUrl + "/MeasurmentStation/GetAllStations",
        this.requestOptions
      )
        .then(handleResponse)
        .then(this.describeStations);
    },
    describeSensors(data) {
      this.sensors = data;
    },
    getSensors() {
      this.resetDatetime();
      return fetch(
        config.backendUrl + `/Sensor/GetByStationId/${this.selectedStation}`,
        this.requestOptions
      )
        .then(handleResponse)
        .then(this.describeSensors);
    }
  },
  created() {
    this.getStations();
  }
};
</script>

<style scoped>
.content {
  margin: 20px;
}
</style>
