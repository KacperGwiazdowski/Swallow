import Vue from "vue";
import Router from "vue-router";

import HomePage from "../home/HomePage";
import LoginPage from "../login/LoginPage";
import RegisterPage from "../register/RegisterPage";
import AdminPage from "../admin/AdminPage";

Vue.use(Router);

export const router = new Router({
  mode: "history",
  routes: [
    { path: "/", component: HomePage },
    { path: "/login", component: LoginPage },
    { path: "/register", component: RegisterPage },
    { path: "/admin", component: AdminPage },
    // otherwise redirect to home
    { path: "*", redirect: "/" },
    { path: "/logout", redirect: "/login" }
  ]
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ["/login", "/register"];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");

  if (loggedIn && (to.path == "/login" || to.path == "/register")) {
    return next("/");
  }

  if (authRequired && !loggedIn && to.path != "/login") {
    return next("/login");
  }
  next();
});
