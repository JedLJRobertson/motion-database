<template>
  <div class="home mt-2">
    <h2> Motion </h2>
    <h3> {{ motionText }} </h3>
    <router-link class="a" to="/"> Return to Search </router-link>
    <hr>
    <div v-if='motion'>
      <div class='badge badge-danger buffer mb-3' v-if='motion.suitability == 1'> Mature </div>
      <div class='badge badge-secondary buffer mb-3' v-if='motion.suitability == 2'>
        Uncategorised Maturity </div>

      <div v-if='motion.sourceCredit'>
        Motion Sourced Courtesy of: {{ motion.sourceCredit }} </div>
      Language: {{ getLanguage() }}
      <p/>

      Motion Difficulty:
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
      <p/>
      Motion Categories:
      <div v-for='category of motion.categories' v-bind:key='category.id'>
        <router-link v-bind:to="'/category/' + category.id"> {{ category.name }}
        </router-link>
        -
        <span class='category-desc'> {{ category.description }} </span>
      </div> <p/>
      Motion Tags: <br/>
      <span v-for='(tag, num) of motion.tags' v-bind:key='tag.id'>
        <router-link v-bind:to="'/tag/' + tag.id" class='tag-link' tag='span'
          >{{ tag.name }}{{ isLastTag(num) ? '' : ','}}</router-link>
      </span>
      <p/>
      <div v-if="motion.infoSlides && motion.infoSlides.length > 0">
        Motion Infoslides: <br/>
        <div class='info-slide' v-for='slide of motion.infoSlides' v-bind:key='slide.id'>
          {{ slide.text }}
        </div>
      </div>
      <p/>
      <div v-if="motion.rounds && motion.rounds.length > 0">
        Known Debates of this Motion: <br/>
        <router-link v-bind:to="'/tournament/' + round.tournamentParentId"
          tag='div' class='round' v-for='round of motion.rounds' v-bind:key='round.id'>
          <b> {{ round.tournamentYear }} </b>
          {{ round.tournamentName }}
          <i> {{ round.round }} </i>
        </router-link>
      </div>
    </div>
  </div>
</template>


<script lang='ts'>
// @ is an alias to /src
import Vue from 'vue';
import ApiRequest from '@/util/apiRequest';

import { API_MOTION_QUERY } from '@/util/config';

export default Vue.extend({
  name: 'home',
  data() {
    return {
      motionText: undefined,
      motion: undefined,
    };
  },
  components: {
  },
  methods: {
    async load(nextRoute) {
      try {
        const response = await ApiRequest.Get(API_MOTION_QUERY + nextRoute.params.id);
        this.$data.motion = response;
        this.$data.motionText = response.text;
      } catch (error) {
        if (error.statusCode === 404) {
          this.$data.motionName = 'Not Found';
        } else {
          throw error;
        }
      }
    },
    isLastTag(tagNumInList) {
      return tagNumInList === this.motion.tags.length - 1;
    },
    getLanguage() {
      switch (this.$data.motion.language) {
        case 'en':
          return 'English';
        default:
          return this.$data.motion.language;
      }
    },
  },
  beforeRouteUpdate(to, from, next) {
    this.load(to);
    next();
  },
  computed: {
  },
  mounted() {
    this.load(this.$route);
  },
});
</script>

<style scoped>
.category-desc {
  color: #63686e;
}
.buffer {
  margin-right: 100%;
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

.info-slide {
  padding: 0.5em;
  margin-left: -0.5em;
  margin-right: -0.5em;
  background-color: #eeeeee;
  margin-top: 0.5em;
}

.round {
  padding: 0.5em;
  margin-left: -0.5em;
  margin-right: -0.5em;
  background-color: #eeeeee;
  margin-top: 0.5em;

  cursor: pointer;
}

.round:hover {
  background-color: #bbbbbb;
}
</style>
