<template>
    <ul class="nav toolBarStyle">
      <li>
        <a
        href="/"
        class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none"
      >
        <svg class="bi me-2" width="40" height="32">
          <use xlink:href="#bootstrap"></use>
        </svg>
        <span class="fs-4">Immerse</span>
      </a>
      </li>
      <hr />
      <div>
        <ul  id ="navBar" class="nav flex-row mt-1 align-items-center">
          <li class="nav-item">
            <router-link class="nav-item" v-bind:to="{ name: 'home' }">
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#home"></use>
              </svg>
              <i class="fas fa-home"></i>
              Home
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'yourLibrary' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#speedometer2"></use>
              </svg>
              <i class="fas fa-book-open"></i>
              &nbsp;&nbsp;Family Library
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'personalHistory' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#speedometer2"></use>
              </svg>
              <i class="fas fa-book-reader"></i>
              &nbsp;&nbsp;Personal Library
            </router-link>
          </li>
          <!-- <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'activity' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              <i class="fas fa-book-reader"></i>
              &nbsp;&nbsp;Reading History
            </router-link>
          </li> -->
          <li>
          <router-link
              class="nav-item"
              v-bind:to="{ name: 'familyProgress' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              <i class="fas fa-chart-bar"></i>                                    
              &nbsp;&nbsp;Family Progress
            </router-link>
          </li>
          <li>
          <router-link
              class="nav-item"
              v-bind:to="{ name: 'registerFamilyMem' }"
              v-if="$store.state.user.role == 'parent'"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              <i class="fas fa-users"></i>   
              &nbsp;&nbsp;Register Family Member
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'register' }"
              v-if="!$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              &nbsp;&nbsp;Register
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'login' }"
              v-if="!$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              &nbsp;&nbsp;Login
            </router-link>
          </li>
          <li>
            <router-link
              class="nav-item"
              v-bind:to="{ name: 'logout' }"
              v-if="$store.state.token"
            >
              <svg class="bi me-2" width="16" height="16">
                <use xlink:href="#table"></use>
              </svg>
              &nbsp;&nbsp;Logout
            </router-link>
          </li>
        </ul>
      </div>
    </ul>
</template>

<script>
export default {
  name: "ToolBar",
};
</script>

<style>

#navBar > li{
  margin: auto;
}

.toolBarStyle {
  margin-right: 20px;
  background-color: #0fa5db;
}
</style>