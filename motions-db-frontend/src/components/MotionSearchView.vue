<template>
  <div>
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

export default Vue.extend({
  name: 'MotionSearchView',
  props: {
    query: {},
  },
  data: () => ({
    motions: undefined,
  }),
  components: {
    MotionSearchItem,
  },
  methods: {
    async runQuery() {
      const response = await ApiRequest.Post(API_MOTION_SEARCH, this.$props.query);
      this.$data.motions = response.results;
    },
  },
  watch: {
    query: 'runQuery',
  },
  mounted() {
    this.runQuery();
  },
});
</script>
