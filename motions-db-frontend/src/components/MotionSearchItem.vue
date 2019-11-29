<template>
  <router-link tag="div" v-bind:to="'/motion/' + motion.id"
      class="motion-item">
    <div v-if='motion.isExplicit' class='btn btn-danger float-right mt-1 mr-2'> Explicit </div>
    <h5> {{ motion.text }} </h5>
    <router-link v-bind:to="'/category/' + motion.categoryId" class='category-link'>
      {{ motion.category }}</router-link>
      <span class='motion-difficulty-easy' v-if='motion.difficulty === 0'> Novice </span>
      <span class='motion-difficulty-medium' v-if='motion.difficulty === 1'>
        Intermediate
      </span>
      <span class='motion-difficulty-hard' v-if='motion.difficulty === 2'> Expert </span>
      <span class='motion-tags'>
        <router-link v-for='tag of motion.tags' v-bind:key='tag.id' v-bind:to="'/tag/' + tag.id"
          class='motion-tag'>{{ tag.name }}</router-link>
      </span>
  </router-link>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';

export default Vue.extend({
  name: 'motionSearchItem',
  props: {
    motion: {},
  },
  components: {
  },
  methods: {
  },
});
</script>
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

.motion-tag {
  color: #888888;
  padding-right: 0.5em;
}
.motion-tag:hover {
  color: #555555;
  text-decoration: underline;
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

.category-link {
  color: #2E3C50;
}
.category-link:hover {
  text-decoration: underline;
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
