<template>
  <main>
    <h1>Reading History</h1>
    <div class="container">
      <canvas id="testChart"></canvas>
    </div>
    <div class="container-fluid">
      <h2>Your Reading History</h2>
      <table class="table table-hover">
        <thead>
          <tr class="d-flex">
            <th class="col-2">Title</th>
            <th class="col-2">Author</th>
            <!-- firstname lastname concat -->
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
  </main>
</template>

<script>
import BookService from "../services/BookService.js";

export default {
  name: "activity",
  components: {
    // BookList
  },
  computed: {
    allActivity() {
      return this.$store.state.readingLog;
    },
    allMyBooks() {
      return this.$store.state.userHistory;
    },
  },
  data() {
    return {
      showForm: false,
      filter: {
        title: "",
        author: "",
        timeRead: "",
        formatType: "",
      },
      myBooks: {
        bookId: 0,
        title: "",
        authorFirstName: "",
        authorLastName: "",
        totalTime: 0,
        isCompleted: "",
      },
      newLog: {
        logID: 0,
        readerId: null,
        loggedBook: {
          bookId: 0,
          title: "",
          authorFirstName: "",
          authorLastName: "",
          isbn: 0,
        },
        formatType: "",
        timeRead: "",
        notes: "",
      },
      activityLog: [],
    };
  },
  async created() {
    BookService.get(this.$store.state.user.userId)
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
  methods: {
    addALog() {
      BookService.postLog(this.$store.state.user.userId, this.newLog)
        .then((response) => {
          console.log(response);
          this.$store.commit("SET_READINGLOG", response.data);
        })
        .catch((respone) => {
          console.error(respone);
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
div {
  /* background-color: aqua; */
}
h1 {
  text-align: center;
  background-color: #dbdbd7;
}
table {
  background-color: #dbdbd7;
}
.readingActivityTable {
  margin-left: auto;
  margin-right: auto;
}
td,
th {
  padding: 0.5em;
  text-align: center;
}
</style>