<template>
  <div class="error-card">
    <h1 style='color: #AA0000;'> Error </h1>
    <h2 style='color: #FF8C00;'> 404 Not Found </h2>
    This page isn't here 😔. <router-link to='/' > Go home </router-link>
  </div>
</template>

<script>

export default {
  name: 'notfound',
  components: {
  },
};
</script>
