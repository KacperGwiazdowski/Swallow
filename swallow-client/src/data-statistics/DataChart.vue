<template>
  <div class="small">
    <line-chart :chart-data="datacollection" :options="options"></line-chart>
  </div>
</template>

<script>
  import LineChart from './LineChart.js'

  export default {
    components: {
      LineChart
    },
    props: ["data"],
    data () {
      return {
        datacollection: null,
        options: {
        scales: {
            yAxes: [
                {
                    ticks: {
                        beginAtZero: true
                    }
                }]
        },
        responsive: true,
        maintainAspectRatio: false
        }
      }
    },
    mounted () {
      this.fillData()
    },
    methods: {
      fillData () {
        this.datacollection = {
          labels: this.data.map(x => x.datetime),
          datasets: [
            {
              label: 'Data',
              backgroundColor: '#9DD1F1',
              data: this.data.map(x => x.value),
            }
          ]
        }
      }
    }
  }
</script>

<style>
  .small {
  }
</style>