<script setup lang="ts">
import { RouterLink } from 'vue-router';
import logo from '../images/revv.png';

</script>
<template>
  <footer class="text-white py-4 mb-2" style="background-color: #000066">
    <div class="container d-flex flex-column flex-lg-row justify-content-between align-items-start">
      <div class="text-center text-lg-start mb-3 mb-md-0">
        <RouterLink to="/">
          <img :src="logo" alt="REVV Logo" height="40" class="mb-3" />
        </RouterLink>
        <div class="d-flex justify-content-center justify-content-md-start  mb-2">
          <a href="https://play.google.com/store/apps/details?id=com.revv.vin" target="_blank">
            <img src="https://upload.wikimedia.org/wikipedia/commons/7/78/Google_Play_Store_badge_EN.svg" alt="Google Play" height="40" />
          </a>
          <a href="https://apps.apple.com/us/app/revv-ai/id6740824555" target="_blank">
            <img src="https://developer.apple.com/assets/elements/badges/download-on-the-app-store.svg" alt="App Store" height="40" />
          </a>
        </div>
        <p class="small mb-0">© 2025 REVV. All rights reserved.</p>
      </div>
      <div class="text-center text-md-end">
        <a href="#" class="d-block text-white text-decoration-none mb-1">About us</a>
        <a href="#" class="d-block text-white text-decoration-none mb-1">Privacy policy</a>
        <a href="#" class="d-block text-white text-decoration-none">Terms of use</a>
      </div>
    </div>
  </footer>
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