<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';

import { API_MOTION_QUERY } from '@/util/config';

export default Vue.extend({
  watch: {
    $route(to, from) {
      document.title = to.meta.title || 'Motions Database';
    },
  },
});
</script>
<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
