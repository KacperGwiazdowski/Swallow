import Vue from "vue";
import Router from "vue-router";

import HomePage from "../home/HomePage";
import LoginPage from "../login/LoginPage";
import RegisterPage from "../register/RegisterPage";
import AdminPage from "../admin/AdminPage";
import MapPage from "../map/MapPage";
import NotFound from "../components/NotFound";
import DataStatistics from "../data-statistics/DataStatistics";

Vue.use(Router);

export const router = new Router({
  mode: "history",
  routes: [
    { path: "/", component: HomePage },
    { path: "/login", component: LoginPage },
    { path: "/register", component: RegisterPage },
    { path: "/admin", component: AdminPage },
    { path: "/map", component: MapPage },
    { path: "/data", component: DataStatistics },
    { path: "/NotFound", component: NotFound },
    // otherwise redirect to home
    { path: "/logout", redirect: "/login" },
    { path: "*", redirect: "/NotFound" }
  ]
});

function parseJwt(token) {
  var base64Url = token.split(".")[1];
  var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
  var jsonPayload = decodeURIComponent(
    atob(base64)
      .split("")
      .map(function(c) {
        return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join("")
  );
  return JSON.parse(jsonPayload);
}

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ["/login", "/register"];
  const authRequired = !publicPages.includes(to.path);
  var loggedIn = localStorage.getItem("user");
  if (loggedIn != null) {
    loggedIn = new Date(parseJwt(loggedIn).exp * 1000) > new Date();
  }

  if (loggedIn && (to.path == "/login" || to.path == "/register")) {
    return next("/");
  }

  if (authRequired && !loggedIn && to.path != "/login") {
    return next("/login");
  }
  return next();
});
