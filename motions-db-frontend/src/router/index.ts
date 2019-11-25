import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const routes = [
  {
    path: '/login',
    name: 'login',
    component: () => import(/* webpackChunkName: "login" */ '../views/Login.vue'),
  },
  {
    path: '/',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Main.vue'),
    children: [
      {
        path: '/',
        name: 'home',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Home.vue'),
      },
      {
        path: '/category/:id',
        name: 'category',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Category.vue'),
      },
      {
        path: '/category',
        name: 'categories',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Categories.vue'),
      },
      {
        path: '/tag/:id',
        name: 'tag',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Tag.vue'),
      },
      {
        path: '/tag/',
        name: 'tags',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Tags.vue'),
      },
    ],
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
