<template>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Id: {{dataRecord.id}}</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div >Value: <input v-model="dataRecord.value" /></div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" v-on:click="updateRecord(dataRecord.id,dataRecord.value)">Save changes</button>
            </div>
            </div>
        </div>
    </div>
</template>

<script>
import { authHeader, isAdmin } from "../_helpers";
import config from "../config";
import moment from "moment"

export default {
  name: "DataEdit",
  props: ["dataRecord"],
  data() {
    return {
        requestOptions: { method: "PATCH", headers: authHeader() },
    };
  },
  methods: {
    updateRecord(id, value){
            return fetch(
                config.backendUrl +
                `/MeasurmentData/UpdateRecord/?id=${id}&value=${value}`,
                this.requestOptions
            )
        }
    }
}
</script>

<style scoped>
</style>
