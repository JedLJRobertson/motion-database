<template>
  <div class="home mt-2">
    <h2> Motion </h2>
    <h3> {{ motionText }} </h3>
    {{ categoryDescription }} <p>
    <router-link class="a" to="/"> Return to Search </router-link>
    <hr>
    <div class='badge badge-danger buffer mb-3' v-if='motion.isExplicit'> Explicit </div>

    Motion Difficulty:
    <span class='motion-difficulty-easy' v-if='motion.difficulty === 0'> Novice </span>
    <span class='motion-difficulty-medium' v-if='motion.difficulty === 1'> Intermediate </span>
    <span class='motion-difficulty-hard' v-if='motion.difficulty === 2'> Expert </span>
    <p/>
    Motion Category:
    <router-link v-bind:to="'/category/' + motion.categoryId"> {{ motion.category }} </router-link>
    <br/>
    <span class='category-desc'> {{ motion.categoryDescription }} </span>
    <p/>
    Motion Tags: <br/>
    <router-link v-for='tag of motion.tags' v-bind:key='tag.id' v-bind:to="'/tag/' + tag.id"
      class='motion-tag'> {{ tag.name }}</router-link>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';

import { API_MOTION_QUERY } from '@/util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      motionText: undefined,
      motion: undefined,
    };
  },
  components: {
  },
  methods: {
    async load() {
      try {
        const response = await ApiRequest.Get(API_MOTION_QUERY + this.$route.params.id);
        this.$data.motion = response;
        this.$data.motionText = response.text;
      } catch (error) {
        if (error.statusCode === 404) {
          this.$data.motionName = 'Not Found';
        } else {
          throw error;
        }
      }
    },
  },
  beforeRouteUpdate(to, from, next) {
    this.load();
  },
  computed: {
  },
  mounted() {
    this.load();
  },
});
</script>

<style scoped>
.category-desc {
  color: #63686e;
}
.buffer {
  margin-right: 100%;
}
.motion-tag {
  color: #888888;
}
.motion-tag:hover {
  color: #555555;
  text-decoration: underline;
}
</style>
