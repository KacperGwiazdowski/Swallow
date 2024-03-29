import { userService } from "../../_services";

const state = {
  all: {}
};

const actions = {
  getAll({ commit }) {
    commit("getAllRequest");
    userService.getAll().then(
      users => commit("getAllSuccess", users),
      error => commit("getAllFailure", error)
    );
  },

  delete({ commit }, id) {
    commit("deleteRequest", id);
    userService.delete(id).then(
      user => commit("deleteSuccess", id),
      error => commit("deleteFailure", { id, error: error.toString() })
    );
  },

  activateUser({ commit }, id) {
    commit("activateUserRequest", id);
    userService.activateUserAccount(id).then(
      id => commit("activationSuccess", id),
      error => commit("activationFailure", { id, error: error.toString() })
    );
  }
};

const mutations = {
  getAllRequest(state) {
    state.all = { loading: true };
  },
  getAllSuccess(state, users) {
    state.all = { items: users };
  },
  getAllFailure(state, error) {
    state.all = { error };
  },
  activateUserRequest(state){
    state.id = { loading: true };
  },
  activationSuccess(state, id){
    state.id = { items: id}
  },
  activationFailure(state, error){
    state.id = { error };
  },
  deleteRequest(state, id) {
    state.all.items = state.all.items.map(user =>
      user.id === id ? { ...user, deleting: true } : user
    );
  },
  deleteSuccess(state, id) {
    state.all.items = state.all.items.filter(user => user.id !== id);
  },
  deleteFailure(state, { id, error }) {
    state.all.items = state.items.map(user => {
      if (user.id === id) {
        const { deleting, ...userCopy } = user;
        return { ...userCopy, deleteError: error };
      }

      return user;
    });
  }
};

export const users = {
  namespaced: true,
  state,
  actions,
  mutations
};
