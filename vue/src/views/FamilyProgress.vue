<template>
<div class="container"  id="familyProgress">
  <body>
    
    <div id="foo">
    <div class="card" v-if="$store.state.user.role == 'parent'">
      <h5 class="card-header">The Family</h5>
      <div class="card-body">
        <h5 class="card-title">Select a family member to check</h5>
          <form>
          <select
            id="childSelector"
            class="form-control" name="Select Child" 
            @change="onChange"
            v-model="selectedChild"
            v-bind="childID"
          >
            <option :value="this.$store.state.user.userId" selected >Your History</option>
            <option v-for="child in allChildren" v-bind:key="child.userId" :value="child.userId"
            >{{child.username}}</option>
          </select>
          </form>
      </div>    
    </div>  
    <user-reading-graph :label="title" :chartData="this.$store.state.readingLog" :options="chartOptions"></user-reading-graph>
    
    <div class="container-fluid">
      <h2>Reading History</h2>
      <table class="table table-hover">
        <thead>
          <tr class="d-flex">
            <th class="col-2">Title</th>
            <th class="col-2">Author</th>
            <th class="col-1">Time Read</th>
            <th class="col-1">Format Type</th>
            <th class="col-6">Notes</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="activity of allActivity" v-bind:key="activity.logID" class="d-flex">
            <td class="col-2">{{ activity.loggedBook.title }}</td>
            <td class="col-2">
              {{ activity.loggedBook.authorFirstName }}
              {{ activity.loggedBook.authorLastName }}
            </td>
            <td class="col-1">{{ activity.timeRead }}</td>
            <td class="col-1">{{ activity.formatType }}</td>
            <td class="col-6">{{ activity.note }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    </div>
  </body>
</div>
</template>
<script>
import BookService from "../services/BookService.js";
import UserReadingGraph from "../components/UserReadingGraph.vue"

export default {
  name: "familyProgress",
  components:{
    UserReadingGraph
  },
  computed: {
    allChildren() {
      return this.$store.state.userChildren;
    },
    allActivity() {
      return this.$store.state.readingLog;
    },
  },
  data() {
    return {
      childID: 0,
      selectedChild: undefined,
      title: "Your Reading Times",
      familyChild: {
        userId: 0,
        username: "string",
        role: "string",
        familyId: 0,
        testProp: 0,
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      }
    };
  },
  created() {
    BookService.getFamilyChildren(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_FAMILY_CHILDREN", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
      
  },
  methods: {
    onChange() {
           BookService.getChildReadingLogs(this.selectedChild)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_READINGLOG", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
    BookService.getUserHistory(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_USER_HISTORY", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
    },
  },
};
</script>
<style scoped>
#familyProgress{
  height: 100%;
  background-image: url("../assets/flower-book(2).jpg");
  background-size: cover;
}
</style>