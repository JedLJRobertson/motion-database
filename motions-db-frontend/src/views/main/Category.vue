<template>
  <div class="home mt-2">
    <h2> Category: {{ categoryName }} </h2>
    {{ categoryDescription }} <p>
    <router-link class="a" to="/"> Return to Search </router-link>
    <div class="form-check">
      <input type='checkbox' class='form-check-input' id='difficulty-easy'
        v-model='includeExplicit' />
      <label class='form-check-label' for='difficulty-easy'>
        Include explicit motions.
      </label>
    </div>
    <hr>
    <motion-search-view v-bind:query='query'> </motion-search-view>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import MotionSearchView from '@/components/MotionSearchView.vue';
import ApiRequest from '@/util/apiRequest';

import { API_MOTION_SEARCH, API_GET_CATEGORIES, API_CATEGORY_QUERY } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      categoryName: '',
      categoryDescription: '',
      includeExplicit: false,
    };
  },
  components: {
    MotionSearchView,
  },
  methods: {
    async load() {
      const response = await ApiRequest.Get(API_CATEGORY_QUERY + this.$route.params.id);
      this.$data.categoryName = response.name;
      this.$data.categoryDescription = response.description;
    },
  },
  beforeRouteUpdate(to, from, next) {
    this.load();
  },
  computed: {
    query() {
      return {
        categories: [parseInt(this.$route.params.id, 10)],
        explicitMode: this.$data.includeExplicit ? 1 : 0,
      };
    },
  },
  mounted() {
    this.load();
  },
});
</script>
