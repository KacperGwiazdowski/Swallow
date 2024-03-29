import Vue from "vue";
import VeeValidate from "vee-validate";

import { store } from "./_store";
import { router } from "./_helpers";
import App from "./app/App";

import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

Vue.use(VeeValidate);


import vuetify from './plugins/vuetify';

new Vue({
  el: "#app",
  router,
  store,
  vuetify,
  render: h => h(App)
});
