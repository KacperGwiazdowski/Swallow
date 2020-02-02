<template>
  <div>
    <Navbar></Navbar>
    <div class="content">
      <span>You're logged in!</span>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import Navbar from "../components/navigation/Navbar";
export default {
  components: { Navbar },
  // computed: {
  //   // ...mapState({
  //   //   account: state => state.account
  //   })
  // },
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
