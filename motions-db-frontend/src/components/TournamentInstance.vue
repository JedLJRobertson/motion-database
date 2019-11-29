<template>
  <div class='tournament'>
    <h4> {{ tournamentInst.name }} </h4>
    {{ tournamentInst.year }} in {{ tournamentInst.location }} <p/>
    <span class='link' v-on:click='viewRounds = true' v-if='!viewRounds'>
      View Rounds ({{ tournamentInst.rounds.length }})
    </span>
    <div v-else>
      <div v-for='round of tournamentInst.rounds' v-bind:key='round.id'>
        <h5> {{ round.name }} </h5>
        <div class='ml-1'>
          <motion-search-item v-for='motion of round.motions'
            v-bind:key='motion.id' v-bind:motion='motion'/>
        </div>
      </div>
      <span class='link' v-on:click='viewRounds = false'> Hide Rounds </span>
    </div>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';

import MotionSearchItem from '@/components/MotionSearchItem.vue';

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
    MotionSearchItem,
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
