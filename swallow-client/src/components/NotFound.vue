<template>
  <div>
    <Navbar></Navbar>
    <div class="content">
      <span>Sorry, page not found.</span>
    </div>
  </div>
</template>

<script>
import Navbar from "../components/navigation/Navbar";
export default {
  components: { Navbar },
  name: "NotFound"
};
</script>

