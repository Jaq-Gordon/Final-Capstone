<template>
<main>
  <div id="registrationPage">
  <div id="register" class="text-center col-md-10 mx-auto col-lg-5">
    <form class="form-register p-4 p-md-5 border rounded-3 bg-light" @submit.prevent="register">
      <img src="../assets/Immerse Logo.png" alt="immerse">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <div class="form-group">
        <input
          type="text"
          id="username"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
          required
          autofocus />
      </div>
      <div class="form-group">
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
          required />
      </div>
      <div class="form-group">
        <input
          type="password"
          id="confirmPassword"
          class="form-control"
          placeholder="Confirm Password"
          v-model="user.confirmPassword"
          required />
      </div>
      <button class="btn btn-primary col-md-5" type="submit">
        Create Account
      </button>
      <div class="form-group">
        <router-link :to="{ name: 'login' }">Have an account?</router-link>
      </div>
    </form>
  </div>
  </div>
</main>
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'parent',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                name: 'login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style scoped>
  #registrationPage{
    padding-top: 3rem;
    height: 100%;
    background-color: #777;
    background-size: cover;
    background-image: url("../assets/hipster.jpg");
  }
  .form-group {
    margin: 8px;
  }
  .btn {
    margin-top: 10px;
  }
</style>
