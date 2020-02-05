<template>
  <div class="wrapper">
    <Navbar></Navbar>
    <div class="admin-content" >
      <button class="btn btn-primary" v-on:click="updateStations()" >Update stations</button>
      <button class="btn btn-primary" v-on:click="updateSensors()">Update sensors</button>
      <table v-if="users.items" class="table">
      <thead class="thead-light">
        <tr>
          <th>Username</th>
          <th></th>
        </tr>
      </thead>
      <tr v-for="user in users.items"
          :key="user.id">
        <td> {{user.username}}</td>
        <td v-if="!user.isActive"><button class="btn btn-primary" v-on:click="activateAndReloadUsers(user.id)">Activate account</button></td>
        <td v-if="user.isActive"><button class="btn btn-primary" v-on:click="deactivateUser(user.id)">Deactivate account</button></td>
      </tr>
    </table>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import Navbar from "../components/navigation/Navbar";
import { authHeader } from "../_helpers";
import config from "../config";
import * as userActons from "../_services/user.service"

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
  components: { Navbar},
  data() {return{requestOptions: { method: "POST", headers: authHeader() }}},
  methods: {
    getUpdateResponse(response){
      console.log(response)
    },
    deactivateUser(id){
      userActons.userService.deactivateUserAccount(id).then(this.getAllUsers());
    },
    updateStations(){
      return fetch(
        config.backendUrl +
          `/MeasurmentStation/UpdateStations`,
        this.requestOptions
      ).then(handleResponse).then(this.getUpdateResponse)
    },
     updateSensors(){
      return fetch(
        config.backendUrl +
          `/Sensor/UpdateSensors`,
        this.requestOptions
      )
    },
    ...mapActions("users", {
      getAllUsers: "getAll",
      activateUser: "activateUser"
    }),
    activateAndReloadUsers(id){
      this.activateUser(id);
      this.getAllUsers();
    }
  },
  computed: {
    ...mapState({
      users: state => state.users.all
    })
  },
  created() {
    this.getAllUsers();
    console.log(this.users);
  }
  // ...mapState({
  //   users: state => state.users
  // })
};
</script>

<style scoped>
.admin-content {
  margin: 20px;
}

.btn {
  margin: 10px;
}

.table{
  width: 50%;
}


</style>
