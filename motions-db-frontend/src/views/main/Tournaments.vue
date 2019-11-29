<template>
  <div class="home mt-2">
    <h2> All Categories </h2>
      <router-link class="a" to="/"> Return to Search </router-link>
    <hr>
    <router-link v-bind:to="'/category/' + category.id"
      v-for='category of categories' v-bind:key='category.id'
      class='list_item mb-3' tag="div">
      <h3 class='mb-0'>
        {{ category.name }}
      </h3>
      {{ category.description }}
    </router-link>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';

import { API_MOTION_SEARCH, API_CATEGORY_QUERY } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      categories: [],
    };
  },
  methods: {
    async load() {
      const response = await ApiRequest.Get(API_CATEGORY_QUERY);
      this.$data.categories = response;
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
}

.list_item:hover {
  background-color: #eeeeee;
}
</style>
