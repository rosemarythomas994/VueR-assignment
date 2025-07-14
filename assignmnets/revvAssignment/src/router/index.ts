import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/components/Home.vue';
import Login from '@/components/Login.vue';
import Welcome from '@/components/Welcome.vue';
import CarEdit from '@/components/CarEdit.vue';
import AddCar from '@/components/AddCar.vue';
import Register from '@/components/Register.vue';

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
      { path: '/register', name: 'Register', component: Register },
    { path: '/add', name: 'AddCar', component: AddCar },


];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
