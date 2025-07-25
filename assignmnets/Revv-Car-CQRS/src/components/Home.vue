<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';


import Cards from './Cards.vue';

import Section from './Section.vue';
import Slides from './Slides.vue'; // Assuming you meant this as 'HeroSection'

// Reactive car list
const cars = ref<any[]>([]);

// On mount, fetch cars from API
// onMounted(async () => {
//   try {
//     const response = await axios.get('https://localhost:5015/api');
//     cars.value = response.data;
//     console.log('Cars from API:', cars.value);
//   } catch (error) {
//     console.error('Error fetching cars:', error);
//   }
// });
</script>

<template>
  <div>

    <Slides /> <!-- renamed from HeroSection -->
    <Cards />
    <Section />

    <!-- 🚗 Displaying car list from backend -->
    <div style="padding: 20px">
      
      <ul>
        <li v-for="car in cars" :key="car._id">
          {{ car.brand }} - {{ car.model }} ({{ car.year }})
        </li>
      </ul>
    </div>


  </div>
</template>

<style scoped>
h2 {
  margin-bottom: 10px;
}
</style>
