<template>
  <div class='tournament'>
    <h4> {{ tournamentInst.name }} </h4>
    {{ tournamentInst.year }} in {{ tournamentInst.location }} <p/>
    <span class='link' v-on:click='viewRounds = true' v-if='!viewRounds'>
      View Rounds ({{ tournamentInst.rounds.length }})
    </span>
    <div v-else>
      <span class='link' v-on:click='viewRounds = false'> Hide Rounds </span>
    </div>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';

export default Vue.extend({
  name: 'tournamentInstance',
  props: {
    tournamentInst: {},
  },
  data() {
    return {
      viewRounds: false,
    };
  },
  components: {
  },
  methods: {
  },
});
</script>
<style>
.tournament {
  padding-left: 1em;
  border-left: 5px solid #dddddd;
  margin-bottom: 1em;
}
</style>
