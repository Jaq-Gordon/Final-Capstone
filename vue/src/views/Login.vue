<template>
  <main>
    <div id="logInPage">
      <div id="login" class="text-center  col-lg-4">
        <form
          class="form-signin p-4 p-md-5 border rounded-3 bg-light"
          @submit.prevent="login"
        >
          <img src="../assets/Immerse Logo.png" alt="immerse" />
          <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
          <div
            class="alert alert-danger"
            role="alert"
            v-if="invalidCredentials"
          >
            Invalid username and password!
          </div>

          <div class="alert alert-danger" role="alert" v-if="networkError">
            Network error!
          </div>

          <div
            class="alert alert-success"
            role="alert"
            v-if="this.$route.query.registration"
          >
            Thank you for registering, please sign in.
          </div>
          <div class="form-group">
            <input
              type="text"
              id="username"
              class="form-control"
              placeholder="Username"
              v-model="user.username"
              required
              autofocus
            />
          </div>
          <div class="form-group">
            <input
              type="password"
              id="password"
              class="form-control"
              placeholder="Password"
              v-model="user.password"
              required
            />
          </div>
          
          <button class="btn btn-primary col-md-5" type="submit">Sign in</button>
          <div class="form-group">
            <router-link :to="{ name: 'register' }"
              >Need an account?</router-link
            >
          </div>
        </form>
      </div>
    </div>
  </main>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
      networkError: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push({ name: "home" });
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response == null || response.status === 500) {
            this.networkError = true;
          } else if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>
<style scoped>
#logInPage {
  padding-top: 3rem;
  height: 100%;
  background-color: #777;
  background-size: cover;
  background-image: url("../assets/bookStack.jpg");
}
#login {
  position: absolute;
  right:200px;
  top: 120px;
  
}
.form-group {
  padding: 8px;
}
.btn {
  margin-top: 15px;
}
</style>
