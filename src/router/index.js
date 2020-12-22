import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue';
import Game from "../views/Game";

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/home',
    name: 'Home',
    component: Home
  },
  {
    path: '/play_demo',
    name: 'Game',
    component: Game
  },
];

const router = new VueRouter({
  mode: 'history',
  routes
});

export default router
