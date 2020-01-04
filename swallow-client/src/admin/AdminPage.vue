<template>
  <div>
    <Navbar></Navbar>
    <div class="content">
      <!-- <h1>Hi {{ firstName }}!</h1> -->
      <div>Users:</div>
      <ul v-if="users.items">
        <li
          v-for="user in users.items"
          :key="user.id"
        >{{user.id}} {{user.username}} {{user.isActive}}</li>
      </ul>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import Navbar from "../components/navigation/Navbar";
export default {
  components: { Navbar },
  methods: {
    ...mapActions("users", {
      getAllUsers: "getAll"
    })
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
.content {
  margin: 20px;
}
</style>
