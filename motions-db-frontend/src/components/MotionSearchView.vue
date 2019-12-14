<template>
  <div>
    <div class='w-100'>
      <div class='per-page-bar mb-2'>
        Results Per Page:
        <div class='btn btn-secondary mr-2 ml-2' v-if='perPage === 10'> 10 </div>
        <div class='btn btn-outline-secondary mr-2 ml-2 clickable' v-on:click='perPage = 10'
          v-else> 10 </div>
        <div class='btn btn-secondary mr-2' v-if='perPage === 25'> 25 </div>
        <div class='btn btn-outline-secondary mr-2 clickable' v-on:click='perPage = 25'
          v-else> 25 </div>
        <div class='btn btn-secondary mr-2' v-if='perPage === 50'> 50 </div>
        <div class='btn btn-outline-secondary mr-2 clickable' v-on:click='perPage = 50'
          v-else> 50 </div>
      </div>
      <page-picker
        v-bind:currentPage='currentPage'
        v-bind:lastPage='lastPage'
        v-on:change-page='startingAt = ($event - 1) * perPage' />
    </div>
    <p/>
    <div v-if='motions && motions.length > 0'>
      <motion-search-item v-for='motion of motions'
        v-bind:key='motion.id' v-bind:motion='motion' />
    </div>
    <div v-else-if="motions">
      No motions were found.
    </div>
    <div v-else>
      Enter a query to search for motions.
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import ApiRequest from '@/util/apiRequest';
import { API_MOTION_SEARCH } from '@/util/config';
import MotionSearchItem from '@/components/MotionSearchItem.vue';
import PagePicker from '@/components/PagePicker.vue';

export default Vue.extend({
  name: 'MotionSearchView',
  props: {
    query: {},
  },
  data: () => ({
    motions: undefined,
    perPage: 25,
    startingAt: 0,
    numMotions: 0,
  }),
  components: {
    MotionSearchItem,
    PagePicker,
  },
  methods: {
    async runQuery() {
      this.rationalisePageValues();

      const ourQuery = this.$props.query;
      ourQuery.startFrom = this.startingAt;
      ourQuery.numberResults = this.perPage;
      const response = await ApiRequest.Post(API_MOTION_SEARCH, ourQuery);
      this.$data.motions = response.results;
      this.$data.numMotions = response.countResults;
    },
    rationalisePageValues() {
      if (this.startingAt < 0) {
        this.startingAt = 0;
      }
    },
  },
  watch: {
    query: 'runQuery',
    perPage: 'runQuery',
    startingAt: 'runQuery',
  },
  mounted() {
    this.runQuery();
  },
  computed: {
    currentPage() {
      return Math.floor(this.$data.startingAt / this.$data.perPage) + 1;
    },
    lastPage() {
      return Math.ceil(this.$data.numMotions / this.$data.perPage);
    },
  },
});
</script>
