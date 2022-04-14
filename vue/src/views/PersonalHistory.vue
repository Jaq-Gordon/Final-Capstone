<!-- THIS IS PERSONAL LIBRARY -->
<template>
  <main>
    <div
      class="modal fade"
      id="exampleModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              Record Reading Activity
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <form id="selectForm">
              <div class="mb-3">
                <label for="timeRead" class="col-form-label"
                  >Session time in minutes:</label
                >
                <input
                  type="number"
                  class="form-control"
                  id="timeRead"
                  placeholder="minutes"
                  v-model.number="newReadingLog.totalTime"
                />
                <div class="mb-3">
                  <label for="formatSelect" class="form-label">Format:</label>
                  <select
                    class="form-control"
                    id="formatSelect"
                    v-model="newReadingLog.formatType"
                  >
                    <option>Paperback</option>
                    <option>Ebook</option>
                    <option>Audiobook</option>
                    <option>Read-Aloud (Reader)</option>
                    <option>Read-Aloud (Listener)</option>
                    <option>Other</option>
                  </select>
                </div>
              </div>
              <div class="mb-3">
                <label for="message-text" class="col-form-label">Notes:</label>
                <textarea
                  class="form-control"
                  id="message-text"
                  v-model="newReadingLog.note"
                >
                </textarea>
              </div>
              <p>Finish the book?</p>
              <div class="form-check form-check-inline">
                
                <input
                  class="form-check-input"
                  type="radio"
                  name="inlineRadioOptions"
                  id="inlineRadio1"
                  value="1"
                  v-model.number="newReadingLog.isCompleted"
                />
                <label class="form-check-label" for="inlineRadio1">Yes</label>
              </div>
              <div class="form-check form-check-inline">
                <input
                  class="form-check-input"
                  type="radio"
                  name="inlineRadioOptions"
                  id="inlineRadio2"
                  value="0"
                  v-model.number="newReadingLog.isCompleted"
                />
                <label class="form-check-label" for="inlineRadio2">No</label>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
            <button
              type="button"
              class="btn btn-primary"
              v-on:click.prevent="addALog"
              data-bs-dismiss="modal"
            >
              Save changes
            </button>
          </div>
        </div>
      </div>
    </div>
   <!-- <h1>Personal Library</h1>-->
    <div class="bookList">
      <div
        class="card cardStyling"
        style="width: 18rem"
        v-for="book of allBooks"
        v-bind:key="book.personalLibraryId"
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
          <button
            type="button"
            class="btn btn-primary"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
            v-on:click="
              newReadingLog.personalLibraryId = book.personalLibraryId
            "
          >
            Record Reading Activity
          </button>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";

export default {
  name: "personalHistory",
  components: {},
  computed: {
    allBooks() {
      return this.$store.state.userBooks;
    },
  },
  data() {
    return {
      recordActivity: false,
      newReadingLog: {
        personalLibraryID: 0,
        formatType: "",
        totalTime: 0,
        note: "",
        isCompleted: 0,
      },
    };
  },
  created() {
    BookService.getPersonalLibrary(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_PERSONAL_LIBRARY", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
      this.newReadingLog.formatType = "Paperback";
  },
  methods: {
    setPersonalLibraryId(input) {
      this.newReadingLog.personalLibraryID = input;
    },
    addALog() {
      BookService.postLog(this.$store.state.user.userId, this.newReadingLog)
        .then((response) => {
          console.log(response);
          this.$store.commit("SET_READINGLOG", response.data);
        })
        .catch((response) => {
          console.error(response);
        });
      BookService.getUserHistory(this.$store.state.user.userId)
        .then((response) => {
          console.error(response);
          this.$store.commit("SET_USER_HISTOTY", response.data);
        })
        .catch((response) => {
          console.error(response);
        });
        this.newReadingLog.totalTime = 0;
        this.newReadingLog.formatType = "Paperback";
        this.newReadingLog.note = "";
        this.newReadingLog.isCompleted = 0;
    },
  },
};
</script>

<style scoped>
h1 {
  text-align: center;
}
.bookList {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}

.cardStyling {
  border-radius: 10px;
  width: 250px;
  height: 500px;
  margin: 20px;
}
img {
  border-radius: 10px;
  padding: 5px;
}
td,
th {
  padding: 0.5em;
  text-align: center;
}
th {
  border: 2px solid black;
}
main {
  height: 100%;
  background-image: url("../assets/manyBooks2.jpg");
  background-repeat: no-repeat;
  background-size: cover;
}

</style>