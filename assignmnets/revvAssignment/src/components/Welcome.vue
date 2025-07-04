<script setup lang="ts">
import { useRoute } from 'vue-router';
import Header from './Header.vue';
import Footer from './Footer.vue';
const route = useRoute();
const name = route.query.name ?? 'Guest';
</script>

<template>
      <Header />
  <div class="welcome-wrapper">
  
    <div class="welcome-card shadow">
      <h1 class="greeting">Welcome, {{ name }}</h1>
      <p class="tagline">We're glad to have you back 🎉</p>
    </div>
   
  </div>
   <Footer />
</template>

<style scoped>
.welcome-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 80vh;
  background: linear-gradient(to right, #f0f4ff, #e5ecff);
}

.welcome-card {
  background-color: white;
  padding: 3rem 4rem;
  border-radius: 1rem;
  text-align: center;
  max-width: 600px;
}

.greeting {
  font-size: 2.5rem;
  color: #003399;
  margin-bottom: 1rem;
}

.tagline {
  font-size: 1.25rem;
  color: #555;
}
</style>
