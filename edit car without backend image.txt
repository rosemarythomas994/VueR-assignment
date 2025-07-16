<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';
import Header from './Header.vue';
import Footer from './Footer.vue';

interface Car {
  id: string;
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
const router = useRouter();
const carId = route.params.id as string;
const car = ref<Car | null>(null);
const error = ref('');

// Fetch car by ID from API
onMounted(async () => {
  const token = localStorage.getItem('token');
  try {
    const response = await axios.get(`http://localhost:5058/api/Car/${carId}`, {
      headers: { Authorization: `Bearer ${token}` }
    });

    const data = response.data;
    ['createdAt', 'updatedAt', 'date'].forEach(field => {
      if (data[field]?.includes('T')) data[field] = data[field].split('T')[0];
    });

    car.value = data;
  } catch (err: any) {
    if (err.response?.status === 401) {
      alert('Session expired. Please login again.');
      localStorage.removeItem('token');
      router.push('/login');
    } else {
      error.value = err.response?.data?.message || 'Failed to load car.';
    }
  }
});

async function save() {
  if (!car.value) return;

  if (!car.value.brand.trim() || !car.value.model.trim()) {
    error.value = 'Brand and Model are required.';
    return;
  }

  try {
    const token = localStorage.getItem('token');
    await axios.put(`http://localhost:5058/api/Car/${car.value.id}`, car.value, {
      headers: { Authorization: `Bearer ${token}` }
    });

    router.push({ name: 'Welcome' });
  } catch (err: any) {
    error.value = err.response?.data?.message || 'Failed to update car.';
  }
}

function cancel() {
  router.push({ name: 'Welcome' });
}
</script>

<template>
  <Header />
  <div class="container my-5">
    <form v-if="car" @submit.prevent="save" class="mx-auto" style="max-width: 600px;">
      <h2 class="text-center mb-4">Edit Car</h2>
      <div v-if="error" class="alert alert-danger text-center">{{ error }}</div>
 <div class="form-group mb-2">
        <label>Image File Name</label>
        <input class="form-control" v-model="car.image" />
      </div>
      <div class="form-group mb-2">
        <label>Brand</label>
        <input class="form-control" v-model="car.brand" required />
      </div>

      <div class="form-group mb-2">
        <label>Model</label>
        <input class="form-control" v-model="car.model" required />
      </div>

      <div class="form-group mb-2">
        <label>Year</label>
        <input class="form-control" type="number" v-model.number="car.year" required />
      </div>

      <div class="form-group mb-2">
        <label>Date</label>
        <input class="form-control" type="date" v-model="car.date" />
      </div>

      <div class="form-group mb-2">
        <label>Place</label>
        <input class="form-control" v-model="car.place" />
      </div>

      <div class="form-group mb-2">
        <label>Number</label>
        <input class="form-control" type="number" v-model.number="car.number" />
      </div>

      <div class="form-group mb-2">
        <label>User ID</label>
        <input class="form-control" v-model="car.userId" />
      </div>

       <div class="form-group mb-2">
        <label>Created At</label>
        <input class="form-control" type="date" v-model="car.createdAt" />
      </div>

      <div class="form-group mb-2">
        <label>Updated At</label>
        <input class="form-control" type="date" v-model="car.updatedAt" />
      </div>

      <div class="d-flex justify-content-between mt-4">
        <button class="btn btn-success" type="submit">Save</button>
        <button class="btn btn-secondary" @click="cancel">Cancel</button>
      </div>
    </form>
  </div>
  <Footer />
</template>
