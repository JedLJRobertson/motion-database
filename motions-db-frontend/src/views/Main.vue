<template>
  <div>
    <div class="row">
      <div class="col-xl">
        <div class="content-card">
          <h1> Motions </h1>
          <span v-if="this.$route.name === 'home'" class='mr-3'>Motion Search</span>
          <router-link to='/' class='mr-3' v-else>Motion Search</router-link>

          <span v-if="this.$route.name === 'categories'" class='mr-3'>Categories</span>
          <router-link to='/category/' class='mr-3' v-else>Categories</router-link>

          <span v-if="this.$route.name === 'tags'" class='mr-3'>Tags</span>
          <router-link to='/tag/' class='mr-3' v-else>Tags</router-link>

          <span v-if="this.$route.name === 'tournaments'" class='mr-3'>Tournaments</span>
          <router-link to='/tournament/' class='mr-3' v-else>Tournaments</router-link>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-xl">
        <div class='content-card'>
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>
</template>
