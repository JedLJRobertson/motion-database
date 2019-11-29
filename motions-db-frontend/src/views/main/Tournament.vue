<template>
  <div class="home mt-2">
    <h2> Tournament: {{ tournamentName }} </h2>
    {{ tournamentDescription }} <p/>
    <router-link class="a" to="/"> Return to Search </router-link>
    <h3> Tournaments, Rounds and Motions </h3>
    <div v-if='tournament'>
      <tournament-instance v-for='tournamentInstance of tournament.tournaments'
        v-bind:key='tournamentInstance.id' v-bind:tournamentInst="tournamentInstance">
      </tournament-instance>
    </div>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';
import TournamentInstance from '@/components/TournamentInstance.vue';

import { API_CATEGORY_QUERY, TITLE_TERMINATOR, API_TOURNAMENT_QUERY } from '@/util/config';

export default Vue.extend({
  name: 'tournament',
  data() {
    return {
      tournamentName: '',
      tournamentDescription: '',
      tournament: undefined,
    };
  },
  components: {
    TournamentInstance,
  },
  methods: {
    async load() {
      try {
        const response = await ApiRequest.Get(API_TOURNAMENT_QUERY + this.$route.params.id);
        this.$data.tournamentName = response.name;
        document.title = response.name + TITLE_TERMINATOR;
        this.$data.tournamentDescription = response.description;
        this.$data.tournament = response;
      } catch (error) {
        if (error.statusCode === 404) {
          this.$data.tournamentName = 'Not Found';
        } else {
          throw error;
        }
      }
    },
  },
  beforeRouteUpdate(to, from, next) {
    this.load();
  },
  mounted() {
    this.load();
  },
});
</script>
