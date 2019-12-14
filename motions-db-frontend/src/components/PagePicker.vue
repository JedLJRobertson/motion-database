<template>
  <div>
    <div class='cur-page-bar mt-2'>
      <div class='btn btn-outline-secondary mr-2 clickable'
        v-on:click='changePage(currentPage - 1)'> &lt; </div>
      <div class='btn btn-outline-secondary mr-2 clickable'
        v-on:click='changePage(1)' v-if='currentPage !== 1'> 1 </div>
      <div class='btn mr-2 clickable'
        v-for='page of quickPageRange' v-bind:key='page'
         v-on:click='changePage(page)'
         v-bind:class='{
          "pg-selected": page == currentPage,
          "btn-secondary": page == currentPage,
          "btn-outline-secondary": page != currentPage
        }'>
        {{ page }} </div>
      <div class='btn btn-outline-secondary mr-2 clickable'
        v-on:click='changePage(lastPage)' v-if='currentPage !== lastPage'> {{ lastPage }} </div>
      <div class='btn btn-outline-secondary clickable'
        v-on:click='changePage(currentPage + 1)'> &gt; </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

export default Vue.extend({
  name: 'PagePicker',
  props: {
    numPages: { type: Number },
    currentPage: { type: Number },
    lastPage: { type: Number },
  },
  data: () => ({
    quickPageRange: [1, 2],
  }),
  methods: {
    changePage(nextPage) {
      if (nextPage < 1 || nextPage > this.lastPage) {
        return;
      }
      this.$emit('change-page', nextPage);
    },
    calculateQuickPageRange() {
      const start = this.currentPage === 1 ? 1 : Math.max(2, this.currentPage - 2);
      const end = Math.min(this.lastPage, this.currentPage + 2);

      this.quickPageRange = [];
      for (let i = start; i <= end; i += 1) {
        this.quickPageRange.push(i);
      }
    },
  },
  watch: {
    currentPage: 'calculateQuickPageRange',
    lastPage: 'calculateQuickPageRange',
  },
  mounted() {
    this.calculateQuickPageRange();
  },
  computed: {
  },
});
</script>
<style scoped>
.pg-selected {
  cursor: default !important;
}
</style>
