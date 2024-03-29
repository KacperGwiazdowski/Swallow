import { authHeader } from "../_helpers";
import config from "../config";

export const userService = {
  login,
  logout,
  register,
  getAll,
  activateUserAccount,
  deactivateUserAccount
};

function login(username, password) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password })
  };
  return fetch(config.backendUrl + "/User/SignIn", requestOptions)
    .then(handleResponseWithoutReload)
    .then(user => {
      if (user.token) {
        localStorage.setItem("user", JSON.stringify(user));
      }
      return user;
    });
}

function logout() {
  localStorage.removeItem("user");
}

function register(user) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user)
  };

  return fetch(config.backendUrl + "/User/AddNewUser", requestOptions).then(
    handleResponseWithoutReload
  );
}

function getAll() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };
  return fetch(config.backendUrl + "/Admin/GetAllUsers", requestOptions).then(
    handleResponse
  );
}

function activateUserAccount(userId) {
  const requestOptions = {
    method: "PUT",
    headers: { ...authHeader(), "Content-Type": "application/json" }
  };
  return fetch(
    config.backendUrl + `/Admin/ActivateUserAccount/${userId}`,
    requestOptions
  ).then(handleResponse);
}

function deactivateUserAccount(userId) {
  const requestOptions = {
    method: "PUT",
    headers: { ...authHeader(), "Content-Type": "application/json" }
  };
  return fetch(
    config.backendUrl + `/Admin/DectivateUserAccount/${userId}`,
    requestOptions
  ).then(handleResponse);
}


function handleResponseWithoutReload(response) {
  return response.text().then(text => {
    console.log(response);
    const data = text && JSON.parse(text);
    console.log(data);
    if (!response.ok) {
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        logout();
        location.reload(true);
      }
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
