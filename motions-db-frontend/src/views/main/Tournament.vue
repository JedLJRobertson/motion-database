<template>
  <div class="home mt-2">
    <h2> Tournament: {{ tournamentName }} </h2>
    {{ tournamentDescription }} <p/>
    <router-link class="a" to="/"> Return to Search </router-link>
    <div v-if="tournament && tournament.links && tournament.links.length > 0"
      class='mb-3 mt-2'>
      <h4> Useful Links </h4>
      <div v-for='(link, linkid) of tournament.links' v-bind:key="linkid">
        <a v-bind:href='link'> {{ tournament.linkDescriptions[linkid] }} </a> <br/>
      </div>
    </div>
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
    async load(nextRoute) {
      try {
        const response = await ApiRequest.Get(API_TOURNAMENT_QUERY + nextRoute.params.id);
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
    this.load(to);
    next();
  },
  mounted() {
    this.load(this.$route);
  },
});
</script>
