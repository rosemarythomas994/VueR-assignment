<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import { useRoute, useRouter } from 'vue-router';
  import axios from 'axios';
  import Header from './Header.vue';
  import Footer from './Footer.vue';

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
    date: string;
    userId: string;
  }

  const route = useRoute();
  const router = useRouter();
  const carId = route.params.id as string;
  const car = ref<Car | null>(null);
  const errorMessage = ref('');
  const name = ref(route.query.name || 'Guest');

  onMounted(async () => {
    if (!localStorage.getItem('token')) {
      router.push('/login');
      return;
    }

    try {
      const response = await axios.get(`/api/Car/${carId}`, {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
      const fetchedCar = response.data;
      ['createdAt', 'updatedAt', 'date'].forEach((field) => {
        if (fetchedCar[field] && fetchedCar[field].includes('T')) {
          fetchedCar[field] = fetchedCar[field].split('T')[0];
        }
      });
      car.value = fetchedCar;
    } catch (err: any) {
      if (err.response?.status === 401) {
        alert('Session expired. Please log in again.');
        localStorage.removeItem('token');
        localStorage.removeItem('loggedInUser');
        router.push('/login');
      } else {
        errorMessage.value = err.response?.data?.message || 'Failed to fetch car data';
      }
    }
  });

  async function save() {
    if (!car.value) return;

    if (!car.value.brand.trim() || !car.value.model.trim()) {
      errorMessage.value = 'Brand and Model are required';
      return;
    }
    if (car.value.year < 1900 || car.value.year > new Date().getFullYear() + 1) {
      errorMessage.value = 'Year must be between 1900 and next year';
      return;
    }
    if (car.value.number <= 0) {
      errorMessage.value = 'Registration number must be positive';
      return;
    }

    try {
      await axios.put(`/api/Car/${carId}`, car.value, {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
      router.push({ name: 'Welcome', query: { name: name.value } });
    } catch (err: any) {
      if (err.response?.status === 401) {
        alert('Session expired. Please log in again.');
        localStorage.removeItem('token');
        localStorage.removeItem('loggedInUser');
        router.push('/login');
      } else {
        errorMessage.value = err.response?.data?.message || 'Failed to save car data';
      }
    }
  }

  function cancel() {
    router.push({ name: 'Welcome', query: { name: name.value } });
  }
</script>

<template>
  <div class="edit-page">
    <Header />
    <div class="edit-container">
      <h2>Edit Car</h2>
      <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>
      <div v-if="!car" class="alert alert-info">Loading car data...</div>
      <form v-else @submit.prevent="save">
        <div class="form-group mb-3">
          <label for="image">Image</label>
          <input v-model="car.image" type="text" id="image" class="form-control" />
        </div>
        <div class="form-group mb-3">
          <label for="createdAt">Created At</label>
          <input v-model="car.createdAt" type="date" id="createdAt" class="form-control" />
        </div>
        <div class="form-group mb-3">
          <label for="updatedAt">Updated At</label>
          <input v-model="car.updatedAt" type="date" id="updatedAt" class="form-control" />
        </div>
        <div class="form-group mb-3">
          <label for="brand">Brand</label>
          <input v-model="car.brand" type="text" id="brand" class="form-control" required />
        </div>
        <div class="form-group mb-3">
          <label for="model">Model</label>
          <input v-model="car.model" type="text" id="model" class="form-control" required />
        </div>
        <div class="form-group mb-3">
          <label for="year">Year</label>
          <input v-model.number="car.year" type="number" id="year" class="form-control" required />
        </div>
        <div class="form-group mb-3">
          <label for="place">Place</label>
          <input v-model="car.place" type="text" id="place" class="form-control" />
        </div>
        <div class="form-group mb-3">
          <label for="number">Reg Number</label>
          <input v-model.number="car.number" type="number" id="number" class="form-control" required />
        </div>
        <div class="form-group mb-3">
          <label for="date">Date</label>
          <input v-model="car.date" type="date" id="date" class="form-control" />
        </div>
        <div class="form-group mb-3">
          <label for="userId">User ID</label>
          <input v-model="car.userId" type="text" id="userId" class="form-control" />
        </div>
        <div class="form-group d-flex justify-content-between">
          <button type="submit" class="btn btn-success">Save</button>
          <button type="button" class="btn btn-secondary" @click="cancel">Cancel</button>
        </div>
      </form>
    </div>
    <Footer />
  </div>
</template>

<style scoped>
  .edit-page {
    min-height: 100vh;
    display: flex;
    flex-direction: column;
  }

  .edit-container {
    flex: 1;
    max-width: 500px;
    margin: 2rem auto;
    padding: 2rem;
    background: white;
    border-radius: 1rem;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  }

  .form-group {
    margin-bottom: 1rem;
  }

  .form-control {
    width: 100%;
    padding: 0.5rem;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
    box-sizing: border-box;
    font-size: 1rem;
  }

    .form-control:focus {
      outline: none;
      border-color: #007bff;
      box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
    }

  .alert {
    padding: 0.75rem;
    border-radius: 0.5rem;
  }

  .alert-danger {
    background-color: #f8d7da;
    color: #721c24;
  }

  .alert-info {
    background-color: #d1ecf1;
    color: #0c5460;
  }

  .btn-success {
    background-color: #28a745;
    border-color: #28a745;
  }

    .btn-success:hover {
      background-color: #218838;
    }

  .btn-secondary {
    background-color: #6c757d;
    border-color: #6c757d;
  }

    .btn-secondary:hover {
      background-color: #5a6268;
    }
</style>
