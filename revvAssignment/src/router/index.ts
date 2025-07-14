import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/components/Home.vue';
import Login from '@/components/Login.vue';
import Welcome from '@/components/Welcome.vue';
import CarEdit from '@/components/CarEdit.vue';
const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/login', name: 'Login', component: Login },
  { path: '/welcome', name: 'Welcome', component: Welcome },
  {
    path: '/edit/:id',
    name: 'CarEdit',
    component:CarEdit,
    // component: () => import('@/components/CarEdit.vue'),
    props: true,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
