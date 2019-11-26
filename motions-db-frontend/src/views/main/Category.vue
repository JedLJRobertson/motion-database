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
    <motion-search-view v-bind:query='query' v-if='queryReady'> </motion-search-view>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import MotionSearchView from '@/components/MotionSearchView.vue';
import ApiRequest from '@/util/apiRequest';

import { API_CATEGORY_QUERY, TITLE_TERMINATOR } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      categoryName: '',
      categoryDescription: '',
      includeExplicit: false,
      queryReady: false,
    };
  },
  components: {
    MotionSearchView,
  },
  methods: {
    async load() {
      try {
        const response = await ApiRequest.Get(API_CATEGORY_QUERY + this.$route.params.id);
        this.$data.categoryName = response.name;
        document.title = response.name + TITLE_TERMINATOR;
        this.$data.categoryDescription = response.description;
        this.$data.queryReady = true;
      } catch (error) {
        if (error.statusCode === 404) {
          this.$data.categoryName = 'Not Found';
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
