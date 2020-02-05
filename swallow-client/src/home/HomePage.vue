<template>
  <div>
    <Navbar></Navbar>
    <div class="content">
      <div>Welocme to home page.</div>
      <div>You're logged in!</div>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import Navbar from "../components/navigation/Navbar";
export default {
  components: { Navbar },
  methods: {
    ...mapActions("account", ["logout"]),
    ...mapActions("users", ["getAll"])
  }
};
</script>

<style scoped>
.content {
  margin: 20px;
}
</style>
