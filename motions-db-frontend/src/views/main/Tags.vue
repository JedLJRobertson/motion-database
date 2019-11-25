<template>
  <div class="home mt-2">
    <h2> Search Tags </h2>
    <router-link class="a" to="/"> Return to Motion Search </router-link>
    <input class='input-group' type='text' placeholder="Search for a tag here" v-model='tagSearch'/>
    <hr>
    <span v-if="!tags"> Start typing above to see tags... </span>
    <div v-else>
      <router-link v-for='tag of tags' v-bind:key='tag.id'
        tag='div' class='tag-item'
        v-bind:to='"/tag/" + tag.id'>
        {{ tag.name }}
      </router-link>
    </div>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';

import { API_TAG_QUERY } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      tags: undefined,
      tagSearch: '',
      lastTagQuery: undefined,
    };
  },
  components: {
  },
  methods: {
    async runTagSearch() {
      if (this.$data.tagSearch.length < 2) {
        this.$data.tags = undefined;
        return;
      }

      clearTimeout(this.$data.tagDebounce);
      this.$data.tagDebounce = setTimeout(async () => {
        if (this.$data.lastTagQuery !== this.$data.tagSearch) {
          this.$data.lastTagQuery = this.$data.tagSearch;
          const response = await ApiRequest.Get(API_TAG_QUERY
            + encodeURIComponent(this.$data.tagSearch));
          this.$data.tags = response;
        }
      }, 600);
    },
  },
  watch: {
    tagSearch: 'runTagSearch',
  },
  mounted() {
  },
});
</script>

<style scoped>
.tag-item {
  cursor: pointer;
  margin-bottom: 0.5em;
  width: 100%;
}

.tag-item:hover {
  text-decoration: underline;
}
</style>
