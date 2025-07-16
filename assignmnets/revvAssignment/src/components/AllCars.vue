<template>
  <Header />
  <div class="container my-5">
    <h2 class="text-primary mb-4">All Cars</h2>

    <div v-if="paginatedCars.length === 0" class="text-center mb-4">
      <p>No cars found.</p>
    </div>

    <div class="row g-4 mb-4">
      <div class="col-md-4" v-for="car in paginatedCars" :key="car.id">
        <div class="card h-100 shadow-sm">
          <img
            :src="`${baseUrl}/images/${car.image}`"
            class="card-img-top"
            alt="Car Image"
          />
          <div class="card-body">
            <h5 class="card-title">{{ car.brand }} {{ car.model }} ({{ car.year }})</h5>
            <p class="card-text">
              <strong>Created:</strong> {{ car.createdAt || '-' }}<br />
              <strong>Updated:</strong> {{ car.updatedAt || '-' }}<br />
              <strong>Location:</strong> {{ car.place }}<br />
              <strong>Reg No:</strong> {{ car.number }}<br />
              <strong>Date:</strong> {{ car.date }}<br />
              <strong>User ID:</strong> {{ car.userId || '-' }}
            </p>
          </div>
        </div>
      </div>
    </div>

    <nav class="pagination-nav mb-4">
      <ul class="pagination justify-content-center">
        <li class="page-item" :class="{ disabled: currentPage === 1 }">
          <a class="page-link" href="#" @click.prevent="goToPage(currentPage - 1)">«</a>
        </li>
        <li
          class="page-item"
          v-for="page in paginationRange"
          :key="page"
          :class="{ active: page === currentPage, disabled: page === '...' }"
        >
          <a class="page-link" href="#" @click.prevent="goToPage(page)">{{ page }}</a>
        </li>
        <li class="page-item" :class="{ disabled: currentPage === totalPages }">
          <a class="page-link" href="#" @click.prevent="goToPage(currentPage + 1)">»</a>
        </li>
      </ul>
    </nav>
  </div>
  <Footer />
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Header from './Header.vue';
import Footer from './Footer.vue';
import axios from '../api';

interface Car {
  id: string;
  brand: string;
  model: string;
  year: number;
  place: string;
  number: string;
  date: string;
  userId: string;
  createdAt?: string;
  updatedAt?: string;
  image?: string;
}

const baseUrl = import.meta.env.VITE_BASE_API_URL.replace('/api', '');

const cars = ref<Car[]>([]);
const error = ref('');
const currentPage = ref(1);
const pageSize = 6;

const totalPages = computed(() => Math.ceil(cars.value.length / pageSize));

const paginatedCars = computed(() => {
  const start = (currentPage.value - 1) * pageSize;
  return cars.value.slice(start, start + pageSize);
});

const paginationRange = computed(() => {
  const range: (number | string)[] = [];
  const total = totalPages.value;

  if (total <= 6) {
    for (let i = 1; i <= total; i++) range.push(i);
  } else {
    if (currentPage.value <= 4) {
      range.push(1, 2, 3, 4, '...', total);
    } else if (currentPage.value >= total - 3) {
      range.push(1, '...', total - 3, total - 2, total - 1, total);
    } else {
      range.push(1, '...', currentPage.value, '...', total);
    }
  }

  return range;
});

function goToPage(page: number | string) {
  if (typeof page === 'number' && page >= 1 && page <= totalPages.value) {
    currentPage.value = page;
  }
}

onMounted(async () => {
  try {
    const response = await axios.get('/Car');
    cars.value = response.data;
  } catch (err: any) {
    console.error(err);
    error.value = err.response?.data?.message || 'Failed to load car data.';
  }
});
</script>

<style scoped>
.card-img-top {
  height: 200px;
  object-fit: cover;
}
</style>
