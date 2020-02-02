<template>
  <div class="wrapper">
    <Navbar></Navbar>
    <div class="admin-content" >
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
        <td v-if="user.isActive"><button class="btn btn-primary">Deactivate account</button></td>
      </tr>
    </table>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import Navbar from "../components/navigation/Navbar";

export default {
  components: { Navbar},
  methods: {
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


.table{
  width: 50%;
}


</style>
