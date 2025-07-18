<script setup lang="ts">
import { RouterLink } from 'vue-router';
import logo from '../images/revv.png';
</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-white shadow-sm py-3 mb-2">
    <div class="container d-flex justify-content-between align-items-center">
      <RouterLink class="navbar-brand" to="/">
        <img :src="logo" alt="Logo" height="50" />
      </RouterLink>
      <div class="d-flex align-items-center gap-4">
        <a href="https://revv.vin/about-us" class="text-dark fw-medium text-decoration-none">About us</a>
        <RouterLink to="/login" class="login-btn">User Login</RouterLink>
        <RouterLink to="/register" class="login-btn">User Register</RouterLink>
      </div>
    </div>
  </nav>
</template>



<style >
.login-btn {
  background-color: #1e3a8a;
  color: white;
  padding: 0.375rem 1rem;
  border-radius: 8px;
  font-size: 0.875rem;
  font-weight: 600;
}
.login-btn:hover {
  background-color: #1e40af;
}
</style>