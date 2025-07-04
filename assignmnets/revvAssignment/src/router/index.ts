import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/components/Home.vue';
import Login from '@/components/Login.vue';
import Welcome from '@/components/Welcome.vue';

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/login', name: 'Login', component: Login },
  { path: '/welcome', component: Welcome },
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
