<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import Header from './Header.vue';
import Footer from './Footer.vue';
import axios from 'axios';

interface Car {
  id: string;
  brand: string;
  model: string;
  year: number;
  place: string;
  number: number;
  date: string;
  userId: string;
  createdAt?: string;
  updatedAt?: string;
  image?: string;
}

const cars = ref<Car[]>([]);
const router = useRouter();
const error = ref('');
const name = ref('');
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
  const token = localStorage.getItem('token');
  const userStr = localStorage.getItem('loggedInUser');
  if (userStr) {
    const user = JSON.parse(userStr);
    const email = user.email || '';
    name.value = email.includes('@') ? email.split('@')[0] : 'Guest';
  }
  try {
    const response = await axios.get('http://localhost:5015/api/Car', {
      headers: { Authorization: `Bearer ${token}` }
    });
    cars.value = response.data;
  } catch (err: any) {
    if (err.response?.status === 401) {
      alert('Session expired. Please login again.');
      localStorage.removeItem('token');
      router.push('/login');
    } else {
      error.value = 'Failed to load cars.';
    }
  }
});

function editCar(carId: string) {
  router.push({ name: 'CarEdit', params: { id: carId } });
}

async function deleteCar(carId: string) {
  const token = localStorage.getItem('token');
  if (!confirm('Are you sure to delete this car?')) return;

  try {
    await axios.delete(`http://localhost:5015/api/Car/${carId}`, {
      headers: { Authorization: `Bearer ${token}` }
    });
    cars.value = cars.value.filter(c => c.id !== carId);
  } catch (err: any) {
    alert(err.response?.data?.message || 'Failed to delete car.');
  }
}

function goToAddCar() {
  router.push({ name: 'AddCar' }); 
}
const baseUrl = import.meta.env.VITE_BASE_API_URL.replace('/api', '');

</script>

<template>
  <Header />
  <div class="container my-5">
    <div class="welcome-card shadow mb-4">
      <h1 class="greeting">Welcome, {{ name }}</h1>
      <p class="tagline">We're glad to have you back 🎉</p>
    </div>
    <div class="text-end mb-4">
      <button class="btn btn-success" @click="goToAddCar">
        + Add Car
      </button>
    </div>
    <div v-if="paginatedCars.length === 0" class="text-center mb-4">
      <p>No cars available.</p>
    </div>

    <div class="row g-4 mb-4">
      <div class="col-md-4" v-for="car in paginatedCars" :key="car.id">
        <div class="card h-100 shadow-sm">
<img :src="`${baseUrl}/images/${car.image}`" class="card-img-top" alt="Car Image" />
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
            <button class="btn btn-primary me-2" @click="editCar(car.id)">Edit</button>
            <button class="btn btn-danger" @click="deleteCar(car.id)">Delete</button>
          </div>
        </div>
      </div>
    </div>

    <nav class="pagination-nav mb-4">
      <ul class="pagination justify-content-center">
        <li class="page-item" :class="{ disabled: currentPage === 1 }">
          <a class="page-link" href="#" @click.prevent="goToPage(currentPage - 1)">«</a>
        </li>
        <li class="page-item" v-for="page in paginationRange" :key="page"
          :class="{ active: page === currentPage, disabled: page === '...' }">
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

.btn-danger {
  background-color: #dc3545;
  border-color: #dc3545;
}

.btn-danger:hover {
  background-color: #bb2d3b;
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
