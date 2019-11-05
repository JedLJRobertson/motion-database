<template>
  <div class="home mt-2">
    <h2> 🔍 Search Emojis </h2>
    <hr>
    <motion-list-summary :motions="motions"> </motion-list-summary>
  </div>
</template>

<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import MotionListSummary from '@/components/MotionListSummary.vue';
import ApiRequest from '@/util/apiRequest';
import { API_MOTION_SEARCH } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      motions: undefined,
      query: {},
    };
  },
  components: {
    MotionListSummary,
  },
  methods: {
    async runQuery() {
      const response = await ApiRequest.Post(API_MOTION_SEARCH, this.$data.query);
      this.$data.motions = response.results;
    },
  },
  mounted() {
    this.runQuery();
  },
});
</script>
