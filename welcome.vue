anver


<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import Header from './Header.vue';
import Footer from './Footer.vue';

import axios from 'axios';

interface Car {
  _id: string;
  createdAt: string;
  updatedAt: string;
  image: string;
  brand: string;
  model: string;
  year: number;
  place: string;
  number: number;
  __v: string;
  date: string;
  userId: string;
}

const route = useRoute();
const name = ref('Guest');
const cars = ref<Car[]>([]);
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
  const userStr = localStorage.getItem('loggedInUser');
  if (userStr) {
    const user = JSON.parse(userStr);
    name.value = user.name || 'Guest';
  }

  try {
    debugger;
    const _token = localStorage.getItem('token');
    const response = await axios.get('http://localhost:5058/api/Car', {
  headers: {
  Authorization: `Bearer ${_token}`
}

});

    cars.value = response.data;
    console.log('Cars from API:', cars.value);
  } catch (error) {
    debugger;
    console.error('Error fetching cars:', error);
  }


});
</script>

<template>
  <Header />

  <div class="container my-5">
    <div class="welcome-card shadow mb-4">
      <h1 class="greeting">Welcome, {{ name }}</h1>
      <p class="tagline">We're glad to have you back 🎉</p>
    </div>

    <div v-if="paginatedCars.length === 0" class="text-center mb-4">
      <p>No cars available.</p>
    </div>

    <div class="row g-4 mb-4">
      <div class="col-md-4" v-for="car in paginatedCars" :key="car._id">
        <div class="card h-100 shadow-sm">
          <img :src="`../images/${car.image}`" class="card-img-top" alt="Car Image" />
          <div class="card-body">
            <h5 class="card-title">{{ car.brand }} {{ car.model }} ({{ car.year }})</h5>
            <p class="card-text">
              <strong>Created:</strong> {{ car.createdAt }}<br />
              <strong>Updated:</strong> {{ car.updatedAt }}<br />
              <strong>Location:</strong> {{ car.place }}<br />
              <strong>Reg No:</strong> {{ car.number }}<br />
              <strong>Date:</strong> {{ car.date }}<br />
              <strong>User ID:</strong> {{ car.userId }}
            </p>
            <RouterLink class="btn btn-primary mt-2" :to="{ name: 'CarEdit', params: { id: car._id }, query: { name } }">
              Edit
            </RouterLink>
            <!-- <RouterLink
  v-if="car._id"
  class="btn btn-primary mt-2"
  :to="{ name: 'CarEdit', params: { id: car._id }, query: { name: name } }"
>
  Edit
</RouterLink> -->

          </div>
        </div>
      </div>
    </div>

    <nav class="pagination-nav mb-4">
      <ul class="pagination justify-content-center">
        <li class="page-item" :class="{ disabled: currentPage === 1 }">
          <a class="page-link" href="#" @click.prevent="goToPage(currentPage - 1)">«</a>
        </li>
        <li class="page-item" v-for="page in paginationRange" :key="page" :class="{ active: page === currentPage, disabled: page === '...' }">
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

<style scoped>
.greeting {
  font-size: 2.5rem;
  color: blue;
}
.tagline {
  font-size: 1.25rem;
  color: #555;
}
.welcome-card {
  background-color: white;
  padding: 2rem;
  border-radius: 1rem;
  text-align: center;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}
.card {
  border: none;
  border-radius: 0.5rem;
}
.card-img-top {
  height: 200px;
  object-fit: cover;
}
.btn-primary {
  background-color: #007bff;
  border-color: #007bff;
}
.btn-primary:hover {
  background-color: #0056b3;
}
.pagination-nav .page-link {
  color: #003399;
}
.pagination-nav .active .page-link {
  background-color: #007bff;
  border-color: #007bff;
  color: white;
}
.pagination-nav .disabled .page-link {
  color: #6c757d;
  pointer-events: none;
}
</style>