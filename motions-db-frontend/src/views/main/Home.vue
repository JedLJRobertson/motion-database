<template>
  <div class="home mt-2">
    <h2> 🔍 Search Emojis </h2>
    <div class='w-100'>
      <vue-tags-input
        placeholder="Search for Categories"
        v-model="categorySearch"
        :tags="selectedCategories"
        :autocomplete-items="filteredCategories"
        :add-only-from-autocomplete="true"
        @tags-changed="newSelection => selectedCategories = newSelection"
      />
      Results will be returned with any of the selected categories.
    </div>
    <div class='w-100'>
      <vue-tags-input
        placeholder="Search for Tags"
        class='mt-2 w-100'
        v-model="tagSearch"
        :tags="selectedTags"
        :autocomplete-items="filteredTags"
        :add-only-from-autocomplete="true"
        @tags-changed="updateTagSearch"
      />
      <div class="form-check">
        <input type='checkbox' class='form-check-input' id='tags-exclusive'
          v-model='tagsExclusive' />
        <label class='form-check-label' for='tags-exclusive'>
          Search exclusively (all tags must be present on motions, otherwise only 1 tag needed).
        </label>
      </div>
      <div class="form-group mt-2">
        <select class="form-control" v-model='explicit'>
          <option default value="0">Age Appropriate</option>
          <option value="1">Include Explicit Motions</option>
          <option value="2">Explicit Motions Only</option>
        </select>
      </div>
    </div>
    <div class='btn btn-primary' style='cursor: pointer;' v-on:click='runSearch'> Search </div>
    <hr>
    {{ queryDescription }}
    <motion-list-summary :motions="motions"> </motion-list-summary>
  </div>
</template>

<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import VueTagsInput from '@johmun/vue-tags-input';
import MotionListSummary from '@/components/MotionListSummary.vue';
import ApiRequest from '@/util/apiRequest';

import { API_MOTION_SEARCH, API_GET_CATEGORIES, API_TAG_QUERY } from '../../util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      motions: undefined,
      categorySearch: '',
      categories: [],
      selectedCategories: [],
      tagSearch: '',
      selectedTags: [],
      filteredTags: [],
      tagDebounce: null,
      lastTagQuery: undefined,
      tagsExclusive: false,
      query: {},
      explicit: 0,
    };
  },
  components: {
    MotionListSummary,
    VueTagsInput,
  },
  methods: {
    async runQuery() {
      const response = await ApiRequest.Post(API_MOTION_SEARCH, this.$data.query);
      this.$data.motions = response.results;
    },
    async getCategories() {
      const response = await ApiRequest.Get(API_GET_CATEGORIES);
      this.$data.categories = response;
    },
    updateTagSearch(newTags : any) {
      this.$data.selectedTags = newTags;
      this.$data.filteredTags = [];
      this.$data.lastTagQuery = undefined;
    },
    async runTagSearch() {
      if (this.$data.tagSearch.length < 2) return;

      clearTimeout(this.$data.tagDebounce);
      this.$data.tagDebounce = setTimeout(async () => {
        if (this.$data.lastTagQuery !== this.$data.tagSearch) {
          this.$data.lastTagQuery = this.$data.tagSearch;
          const response = await ApiRequest.Get(API_TAG_QUERY
            + encodeURIComponent(this.$data.tagSearch));
          this.$data.filteredTags = response.map((tag: any) => ({ id: tag.id, text: tag.name }));
        }
      }, 600);
    },
    async runSearch() {
      this.$data.query = {
        categories: this.$data.selectedCategories.map(cat => cat.id),
        tags: this.$data.selectedTags.map(tag => tag.id),
        allTags: this.$data.tagsExclusive,
        explicitMode: parseInt(this.$data.explicit, 10),
      };
      this.runQuery();
    },
  },
  computed: {
    filteredCategories() {
      return this.$data.categories.filter(
        (cat : any) => cat.name.startsWith(this.$data.categorySearch),
      ).map((category: any) => ({ id: category.id, text: category.name }));
    },
  },
  watch: {
    tagSearch: 'runTagSearch',
  },
  mounted() {
    this.runQuery();
    this.getCategories();
    this.runTagSearch();
  },
});
</script>
<style>
.vue-tags-input {
  max-width: 100% !important;
}
.ti-input {
  width: 100% !important;
}
</style>
