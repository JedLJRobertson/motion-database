<template>
  <div class="home mt-2">
    <h2> Tag: {{ tagName }} </h2>
    Synonyms: {{ tagSynonyms.join(", ") }} <p/>
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

import { API_TAG_QUERY_ID, TITLE_TERMINATOR } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      tagName: '',
      tagSynonyms: [],
      includeExplicit: false,
      queryReady: false,
    };
  },
  components: {
    MotionSearchView,
  },
  methods: {
    async load(nextRoute) {
      try {
        const response = await ApiRequest.Get(API_TAG_QUERY_ID + nextRoute.params.id);
        this.$data.tagName = response.name;
        this.$data.tagSynonyms = response.synonyms;
        document.title = response.name + TITLE_TERMINATOR;
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
    this.load(to);
    next();
  },
  computed: {
    query() {
      return {
        tags: [parseInt(this.$route.params.id, 10)],
        explicitMode: this.$data.includeExplicit ? 1 : 0,
      };
    },
  },
  mounted() {
    this.load(this.$route);
  },
});
</script>
