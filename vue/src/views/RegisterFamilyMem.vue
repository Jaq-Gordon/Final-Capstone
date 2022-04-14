<template>
  <main>
    <div id="registerFamily">
      <div id="register" class="text-center col-md-10 mx-auto col-lg-5">
        <form
          class="form-register  border rounded-3 bg-light"
          @submit.prevent="register"
        >
          <h1 class="h3 mb-3 font-weight-normal">Add A Family Member</h1>
          <div
            class="alert alert-danger"
            role="alert"
            v-if="registrationErrors"
          >
            {{ registrationErrorMsg }}
          </div>
          <div class="form-group">
            <input
              type="text"
              id="username"
              class="form-control"
              placeholder="Username"
              v-model="fUser.username"
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
              v-model="fUser.password"
              required
            />
          </div>
          <div class="form-group">
            <input
              type="password"
              id="confirmPassword"
              class="form-control"
              placeholder="Confirm Password"
              v-model="fUser.confirmPassword"
              required
            />
          </div>
          <div class="form-group" id="permissionLine">
            <label for="formatSelect" class="permissionText">Permissions:</label>
            <select id="formatSelect" v-model="fUser.role" required>
              <option>Parent</option>
              <option>Child</option>
            </select>
          </div>
          <button class="btn btn-primary" type="submit">Add member!</button>
        </form>
      </div>
    </div>
  </main>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "registerFamilyMem",
  data() {
    return {
      fUser: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "",
        parentId: this.$store.state.user.userId,
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    register() {
      if (this.fUser.password != this.fUser.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .registerNewMember(this.fUser)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                name: "home",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style scoped>
#registerFamily {
  height: 100%;
  background-image: url("../assets/dadandchild.jpg");
  background-repeat: no-repeat;
  background-size: cover;
}
.form-register {
  position: relative;
  top: 100px;
  padding: 20px 25px 20px 5px;
}
.form-control {
  margin: 10px;
}
.permissionText {
  margin-right: 10px;
}
#permissionLine {
  margin-bottom: 10px;
}
.form-control {
  padding: 6px 12px;
}
</style>
