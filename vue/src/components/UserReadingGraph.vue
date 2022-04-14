<script>
import {Bar} from "vue-chartjs";

export default {
    extends: Bar,
    props:{
        label:{
            type: String
        },
        chartData:{
            type: Array
        },
        options: {
            type: Object
        }
    },
    mounted(){
      const book = this.chartData.map(d => d.loggedBook.title);
      const minutes = this.chartData.map(d => d.timeRead);

      this.renderChart(
          {
              labels: book,
              datasets:[
                  {
                      label: this.label,
                      data: minutes,
                      backgroundColor: "rgba(15, 165, 219, 0.9)"
                  }
              ]
          },
          this.options
      )
    }
}
</script>