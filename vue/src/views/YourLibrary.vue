<!-- FAMILY LIBRARY -->
<template>
  <main>
    <div
      class="modal fade"
      id="staticBackdrop"
      data-bs-backdrop="static"
      data-bs-keyboard="false"
      tabindex="-1"
      aria-labelledby="staticBackdropLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="staticBackdropLabel">
              Immerse in the story!
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">Add book to personal reading list?</div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-danger"
              data-bs-dismiss="modal"
            >
              On Second thought...
            </button>
            <button
              type="button"
              class="btn btn-success"
              data-bs-dismiss="modal"
              v-on:click="addToPersonalLibrary"
            >
              Let's go!
            </button>
          </div>
        </div>
      </div>
    </div>
    <div id="familyLibrary">
      <h1 class="text-white" >The Family Library</h1>
      <button
        v-if="!isAddBookVisible"
        v-on:click.prevent="isAddBookVisible = true"
        id="addABookBtn"
      >
        Add a Book
      </button>

      <form
        id="addBookForm"
        class="row g-3"
        v-show="isAddBookVisible"
        v-on:submit.prevent="addABook"
      >
        <div class="col-12" id="bookTitleBar">
          <label for="inputTitle" class="form-label">Title</label>
          <input
            type="text"
            class="form-control"
            id="inputTitle"
            v-model="newBook.title"
          />
        </div>
        <div class="col-md-4">
          <label for="inputAuthorFirstName" class="form-label"
            >Author's First Name</label
          >
          <input
            type="text"
            class="form-control"
            id="inputAuthorFirstName"
            v-model="newBook.authorFirstName"
          />
        </div>
        <div class="col-md-4">
          <label for="inputAuthorLastName" class="form-label"
            >Author's Last Name</label
          >
          <input
            type="text"
            class="form-control"
            id="inputAuthorLastName"
            v-model="newBook.authorLastName"
          />
        </div>
        <div class="col-md-4">
          <label for="inputISBN" class="form-label">ISBN</label>
          <input
            type="text"
            class="form-control"
            id="inputISBN"
            v-model.number="newBook.isbn"
          />
        </div>
        <input
          type="submit"
          value="Save"
          class="col-md-2"
          id="saveBook"
          v-on:click="isAddBookVisible = false"
        />
        <input
          type="button"
          value="Cancel"
          class="col-md-2"
          id="cancelBook"
          v-on:click="isAddBookVisible = false; clearForm()"
        />
      </form>
      <div class="bookList">
        <div
          class="card cardStyling"
          style="width: 18rem"
          v-for="book of allBooks"
          v-bind:key="book.bookId"
        >
          <img
            v-if="book.isbn"
            v-bind:src="'http://covers.openlibrary.org/b/isbn/' + book.isbn + '-M.jpg'"
            id="bookcover"
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
              data-bs-target="#staticBackdrop"
              v-on:click="pBookId = book.bookId"
            >
              Start Your Journey
            </button>
          </div>
        </div>
      </div>

      
    </div>
  </main>
</template>

<script>
import BookService from "../services/BookService.js";

export default {
  name: "yourLibrary",
  components: {
    // BookList
  },
  computed: {
    allBooks() {
      return this.$store.state.books;
    },
  },
  data() {
    return {
      isAddBookVisible: false,
      pBookId: 0,
      newBook: {
        bookId: null,
        title: "",
        authorFirstName: "",
        authorLastName: "",
        isbn: null,
      },
    };
  },
  created() {
    BookService.getFamilyBooks(this.$store.state.user.userId)
      .then((response) => {
        console.log(response);
        this.$store.commit("SET_FAMILY_BOOKS", response.data);
      })
      .catch((response) => {
        console.error(response);
      });
  },
  methods: {
    addABook() {
      BookService.post(this.$store.state.user.userId, this.newBook)
        .then((response) => {
          console.log(response);
          this.$store.commit("SET_FAMILY_BOOKS", response.data);
        })
        .catch((response) => {
          console.error(response);
        });
        this.newBook.bookId = null;
        this.newBook.title = "";
        this.newBook.authorFirstName = "";
        this.newBook.authorLastName = "";
        this.newBook.isbn = null;
    },
    addToPersonalLibrary() {
      BookService.postPersonalBook(this.$store.state.user.userId, this.pBookId)
        .then((response) => {
          console.log(response);
          this.$store.commit("SET_PERSONAL_LIBRARY", response.data);
        })
        .catch((response) => {
          console.error(response);
        });
    },
    clearForm() {
      this.newBook.bookId = null;
      this.newBook.title = "";
      this.newBook.authorFirstName = "";
      this.newBook.authorLastName = "";
      this.newBook.isbn = null;
    }
  },
};
</script>

<style scoped>
#familyLibrary {
  height: 100%;
  background-image: url("../assets/Background.jpg");
  background-size: cover;
}
#addBookForm{
  background-color: #939799;
  max-width: 1200px;
  margin: 0 auto;
  border-radius: 10px;
  padding: 10px 20px 10px 20px;
}
#addBookForm > div > label {
  color: white;
  font-size: 1.2em;
  margin-bottom: 4px;
  
}
#addBookForm > #bookTitleBar {
  margin-top: 0px;
}
#saveBook, #cancelBook {
  margin: 20px 5px 0px 5px;
  background-color: #0d6efd;
  color: white;
  font-size: 1.1em;
  border-radius: 5px;
}


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
  overflow-y: auto;
}
#addABookBtn {
  position: relative;
  left: 50px;
  font-size: 1.2em;
  padding: 2px 10px;
  font-style: oblique;
  font-weight: bold;
  border-radius: 3px;
  border: solid 2px white;
}
</style>