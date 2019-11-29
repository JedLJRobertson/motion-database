<template>
  <div class="home mt-2">
    <h2> All Tournaments </h2>
      <router-link class="a" to="/"> Return to Motion Search </router-link> <p/>
      Descriptions:
      <span v-if='showDescriptions' v-on:click='showDescriptions = false' class='link'> Hide </span>
      <span v-else v-on:click='showDescriptions = true' class='link'> Show </span>
    <hr>
    <router-link v-bind:to="'/tournament/' + tournament.id"
      v-for='tournament of tournaments' v-bind:key='tournament.id'
      class='list_item mb-3' tag="div">
      <h3 class='mb-0'>
        {{ tournament.name }}
      </h3>
      <div v-if='showDescriptions'>
        {{ tournament.description }}
      </div>
    </router-link>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';

import { API_TOURNAMENT_QUERY } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      tournaments: [],
      showDescriptions: false,
    };
  },
  methods: {
    async load() {
      const response = await ApiRequest.Get(API_TOURNAMENT_QUERY);
      this.$data.tournaments = response;
    },
  },
  mounted() {
    this.load();
  },
});
</script>

<style scoped>
.list_item {
  cursor: pointer;
  padding: 0.5em;
  margin-left: -0.5em;
  margin-right: -0.5em;
}

.list_item:hover {
  background-color: #eeeeee;
}
</style>
