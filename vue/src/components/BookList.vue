<template>
  <div class="bookList">
    <div
      class="card cardStyling"
      style="width: 18rem"
      v-for="book of allMyBooks"
      v-bind:key="book.logID"
    >
      <img
        v-if="book.isbn"
        v-bind:src="
          'http://covers.openlibrary.org/b/isbn/' + book.isbn + '-M.jpg'
        "
      />
      <div class="card-body">
        <h5 class="card-title">{{ book.title }}</h5>
        <p class="card-text">
          {{ book.authorFirstName }} {{ book.authorLastName }}
        </p>
        <a href="#" class="btn btn-primary">Add to library</a>
      </div>
    </div>
  </div>
</template>

<script>
import BookService from "../services/BookService.js";

export default {
  name: "BookList",
  computed: {
    allMyBooks() {
      return this.$store.state.userHistory;
    },
  },
  data() {
    return {
      myBooks: {
        bookId: 0,
        title: "",
        authorFirstName: "",
        authorLastName: "",
        isbn: 0,
        totalTime: 0,
        isCompleted: "",
      },
    };
  },
  created() {
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
    getPersonalLibrary() {
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

<style>
h1 {
  font-weight: 300;
}
</style>