<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router';
import { useAuthStore } from '../stores/authStore'; // <-- Pinia store
import logo from '../images/revv.png';

const auth = useAuthStore();
const router = useRouter();

function handleLogout() {
  auth.logout();
  router.push('/login');
}
</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-white shadow-sm py-3 mb-2">
    <div class="container d-flex justify-content-between align-items-center">
      <RouterLink class="navbar-brand" to="/">
        <img :src="logo" alt="Logo" height="50" />
      </RouterLink>

      <div class="d-flex align-items-center gap-4">
        <a
          href="https://revv.vin/about-us"
          class="text-dark fw-medium text-decoration-none"
          target="_blank"
        >
          About us
        </a>

        <template v-if="auth.isLoggedIn">
          <span class="text-dark me-2 fw-semibold">
            {{ auth.userEmail.split('@')[0] }}
          </span>
          <button @click="handleLogout" class="login-btn">Logout</button>
        </template>

        <template v-else>
          <RouterLink to="/login" class="login-btn">User Login</RouterLink>
          <RouterLink to="/register" class="login-btn">User Register</RouterLink>
        </template>
      </div>
    </div>
  </nav>
</template>

<style scoped>
.login-btn {
  background-color: #1e3a8a;
  color: white;
  padding: 0.375rem 1rem;
  border-radius: 8px;
  font-size: 0.875rem;
  font-weight: 600;
  border: none;
  transition: background-color 0.2s ease;
}

.login-btn:hover {
  background-color: #1e40af;
}
</style>
