<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app">
    <!-- If you start to get random styling you don't like, remove container from this div -->
    <header>
    <tool-bar ></tool-bar>
    </header>
    <router-view id="routerView"/> 
  </div>
</template>

<script>
import Activity from "./views/Activity.vue";
import ToolBar from "./components/ToolBar.vue";
export default {
  components: { ToolBar },
  name: "app",
  component: {
    Activity
    //BookList,
  },
};
</script>

<!-- Application-Wide Styles go here.  -->
<style>
.nav-item {
  text-decoration: none;
  color:beige;
}

#app {
  justify-content: cover;
  height: 100vh;
  width: 100vw;
  max-height: 100%;
  overflow-x: auto;
  overflow-y: auto;
  background-color: #d7d7db;
  /*background-image: url("assets/Background.jpg");*/
  background-size: cover;
  font-family: 'Atkinson Hyperlegible', sans-serif;
}

#routerView{
  height:95vh
}

#routerView > h1{
   background-color: #d7d7db;
  
}

</style>
