import { createRouter,createWebHistory } from "vue-router";
import login from "./components/login.vue";

const routes =[
    {path: '/login', component:login},
   
]
export const router = createRouter({
    history: createWebHistory(),
    routes,
})