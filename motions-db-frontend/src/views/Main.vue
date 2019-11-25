<template>
  <div>
    <div class="row">
      <div class="col-xl">
        <div class="content-card">
          <h1> Motions </h1>
          <router-link to='/' class='mr-3'>Motion Search</router-link>
          <router-link to='/category/' class='mr-3'>Categories</router-link>
          <router-link to='/tag/' class='mr-3'>Tags</router-link>
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
