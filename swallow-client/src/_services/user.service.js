import { authHeader } from "../_helpers";
import config from "../config";

export const userService = {
  login,
  logout,
  register,
  getAll,
  getById,
  update,
  delete: _delete
};

function login(username, password) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json"},
    body: JSON.stringify({ username, password })
  };
  console.log(requestOptions);
  return fetch(config.backendUrl+"/User/SignIn", requestOptions)
    .then(handleResponse)
    .then(user => {
      if (user.token) {
        localStorage.setItem("user", JSON.stringify(user));
      }

      return user;
    });
}

function logout() {
  console.log("wejszlo");
  // remove user from local storage to log user out
  localStorage.removeItem("user");
  console.log(localStorage);
}

function register(user) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user)
  };

  return fetch(config.backendUrl+"/User/AddNewUser", requestOptions).then(
    handleResponse
  );
}

function getAll() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };
  debugger;
  return fetch(config.backendUrl+"/User/GetAllUsers", requestOptions).then(
    handleResponse
  ).then(users => console.log(users))
}

function getById(id) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(`http://localhost:4000/users/${id}`, requestOptions).then(
    handleResponse
  );
}

function update(user) {
  const requestOptions = {
    method: "PUT",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify(user)
  };

  return fetch(`http://localhost:4000/users/${user.id}`, requestOptions).then(
    handleResponse
  );
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(`http://localhost:4000/users/${id}`, requestOptions).then(
    handleResponse
  );
}

function handleResponse(response) {
  return response.text().then(text => {
    console.log(response);
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
      }
      
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
