<template>
  <div class="motion-item">
    <div v-if='motion.suitability == 1' class='btn btn-danger float-right mt-1 mr-2'> Mature </div>
    <div v-if='motion.suitability == 2' class='badge badge-secondary float-right mt-1 mr-2'>
      Uncategorised Maturity </div>
    <router-link tag="h5" class='motion-text'
    v-bind:to="'/motion/' + motion.id"> {{ motion.text }} </router-link>

    <span v-for='(cat, num) of motion.categories' v-bind:key="cat.id" class='mr-1'>
      <router-link v-bind:to="'/category/' + cat.id" class='category-link' tag='a'
      >{{ cat.name }}{{ isLastCategory(num) ? '' : ','}}</router-link>
    </span>

    <span class='motion-difficulty-uncat' v-if='motion.difficulty === 0'>
      Uncategorised Difficulty </span>
    <span class='motion-difficulty-easy' v-if='motion.difficulty === 1'>
      Novice Schools
    </span>
    <span class='motion-difficulty-easy' v-if='motion.difficulty === 2'>
      Novice University
    </span>
    <span class='motion-difficulty-medium' v-if='motion.difficulty === 3'> Intermediate </span>
    <span class='motion-difficulty-hard' v-if='motion.difficulty === 4'> Advanced </span>

    <span v-for='(tag, num) of motion.tags' v-bind:key='tag.id'>
      <router-link v-bind:to="'/tag/' + tag.id" class='tag-link' tag='span'
        >{{ tag.name }}{{ isLastTag(num) ? '' : ','}}</router-link>
    </span>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';

export default Vue.extend({
  name: 'motionSearchItem',
  props: {
    motion: {},
  },
  data() {
    return {
      categoryList: '',
    };
  },
  watch: {
  },
  methods: {
    isLastCategory(catNumInList) {
      return catNumInList === this.motion.categories.length - 1;
    },
    isLastTag(tagNumInList) {
      return tagNumInList === this.motion.tags.length - 1;
    },
  },
  mounted() {
  },
});
</script>
<style scoped>
.motion-item {
  width: 100%;


  padding: 6px;
  margin-left: -6px;
}
.motion-item:hover {
  background-color: #eeeeee;
}

.motion-text {
  cursor: pointer;
}
.motion-text:hover {
  text-decoration: underline;
}

.tag-link {
  color: #888888;
  margin-right: 0.25em;
  cursor: pointer;
}
.tag-link:hover {
  color: #555555;
  text-decoration: underline;
}

h5 {
  color: #111111;
  margin-bottom: 0;
}
hr {
  width: 30%;
  margin-left: 10%;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}

.motion-difficulty-uncat {
  color: #777777;
}
.motion-difficulty-easy {
  color: #006f3c;
}
.motion-difficulty-medium {
  color: #264b96;
}
.motion-difficulty-hard {
  color: #bf212f;
}

.category-link {
  color: #2E3C50;
}
.category-link:hover {
  text-decoration: underline;
}
</style>
