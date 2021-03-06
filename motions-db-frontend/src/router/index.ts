import Vue from 'vue';
import VueRouter from 'vue-router';
import { TITLE_TERMINATOR } from '@/util/config';

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
        path: '/motion/:id',
        name: 'motion',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Motion.vue'),
        meta: {
          title: `Motion ${TITLE_TERMINATOR}`,
        },
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
        meta: {
          title: `All Categories ${TITLE_TERMINATOR}`,
        },
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
        meta: {
          title: `Tag Search ${TITLE_TERMINATOR}`,
        },
      },
      {
        path: '/tournament/:id',
        name: 'tournament',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Tournament.vue'),
      },
      {
        path: '/tournament/',
        name: 'tournaments',
        component: () => import(/* webpackChunkName: "about" */ '../views/main/Tournaments.vue'),
        meta: {
          title: `Tournaments ${TITLE_TERMINATOR}`,
        },
      },
    ],
  },
  {
    path: '*',
    name: 'notfound',
    component: () => import(/* webpackChunkName: "login" */ '../views/NotFound.vue'),
    meta: {
      title: `Page Not Found ${TITLE_TERMINATOR}`,
    },
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
