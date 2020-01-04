import Vue from "vue";
import Vuex from "vuex";

import { alert } from "./auth/alert.module";
import { account } from "./auth/account.module";
import { users } from "./auth/users.module";

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    alert,
    account,
    users
  }
});
