<template>
  <div>
    <div class="motion-item" v-for="motion of motions" v-bind:key="motion.id">
      <div v-if='motion.isExplicit' class='btn btn-danger float-right mt-1 mr-2'> Explicit </div>
      <h5> {{ motion.text }} </h5>
      {{ motion.category }}
        <span class='motion-difficulty-easy' v-if='motion.difficulty === 0'> Novice </span>
        <span class='motion-difficulty-medium' v-if='motion.difficulty === 1'> Intermediate </span>
        <span class='motion-difficulty-hard' v-if='motion.difficulty === 2'> Expert </span>
        <span class='motion-tags'>
          {{ motion.tags.map(t => t.name).join(', ') }}
        </span>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

export default Vue.extend({
  props: {
    motions: { type: Array },
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.motion-item {
  width: 100%;

  cursor: pointer;

  padding: 6px;
  margin-left: -6px;
}

.motion-item:hover {
  background-color: #eeeeee;
}

.motion-tags {
  color: #888888;
}

h5 {
  color: #111111;
  margin-bottom: 0;
}
hr {
  width: 30%;
  margin-left: 10%;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

.motion-difficulty-easy {
  color: #006f3c;
}
.motion-difficulty-medium {
  color: #264b96;
}
.motion-difficulty-hard {
  color: #bf212f;
}
</style>
