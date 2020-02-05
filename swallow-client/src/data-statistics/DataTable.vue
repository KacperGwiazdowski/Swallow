<template>
  <div>
    <div class="content">
      {{isAdmin}}
      <table class="table">
        <thead class="thead-light">
          <tr>
            <th>Datetime</th>
            <th>Value</th>
          </tr>
        </thead>
        <tr v-for="record in data" :key="record.datetime">
          <td>{{record.datetime}}</td>
          <td>{{record.value}}</td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script>
import { authHeader, isAdmin } from "../_helpers";
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
  name: "DataTable",
  props: ["selectedSensorId", "selectedDate"],
  data() {
    return {
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
