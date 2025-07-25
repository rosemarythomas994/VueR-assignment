<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';

interface Car {
  id: string;
  image: string;
  brand: string;
  model: string;
  year: number;
  place: string;
  number: number;
  date: string;
  createdAt: string;
  updatedAt: string;
  userId: string;
}

const route = useRoute();
const router = useRouter();
const carId = route.params.id as string;
const car = ref<Car | null>(null);
const error = ref('');
const newImageFile = ref<File | null>(null);
const previewUrl = ref<string | null>(null);
const user = JSON.parse(localStorage.getItem('loggedInUser') || '{}');

// Load car from API
onMounted(async () => {
  const token = localStorage.getItem('token');
  try {
    const res = await axios.get(`http://localhost:5015/api/Car/${carId}`, {
      headers: { Authorization: `Bearer ${token}` }
    });

    const data = res.data;
    ['createdAt', 'updatedAt', 'date'].forEach((key) => {
      if (data[key]?.includes('T')) data[key] = data[key].split('T')[0];
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

// When new image selected
function onImageChange(e: Event) {
  const input = e.target as HTMLInputElement;
  const file = input?.files?.[0];
  if (file) {
    newImageFile.value = file;
    previewUrl.value = URL.createObjectURL(file);
  }
}

// Save update
async function save() {
  if (!car.value) return;
  const token = localStorage.getItem('token');

  try {
    const formData = new FormData();

    formData.append('ImageFile', newImageFile.value ?? new Blob([], { type: 'image/jpeg' }), newImageFile.value?.name ?? 'empty.jpg');
    formData.append('Brand', car.value.brand);
    formData.append('Model', car.value.model);
    formData.append('Year', car.value.year.toString());
    formData.append('Place', car.value.place);
    formData.append('Number', car.value.number.toString());
    formData.append('Date', car.value.date);
    formData.append('UserId', user.email); // Required

    await axios.put(`http://localhost:5015/api/Car/${car.value.id}`, formData, {
      headers: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'multipart/form-data'
      }
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

  <div class="container my-5">
    <form v-if="car" @submit.prevent="save" class="mx-auto" style="max-width: 600px;">
      <h2 class="text-center mb-4">Edit Car</h2>

      <div v-if="error" class="alert alert-danger text-center">{{ error }}</div>

      <!-- Current image -->
      <div class="mb-3">
        <label class="form-label">Current Image</label><br />
        <img
          v-if="car.image"
          :src="`http://localhost:5058/images/${car.image}`"
          class="img-thumbnail"
          style="max-height: 200px;"
        />
      </div>

      <!-- Replace image -->
      <div class="mb-3">
        <label class="form-label">Replace Image (optional)</label>
        <input
          type="file"
          class="form-control"
          accept="image/*"
          @change="onImageChange"
        />
        <img
          v-if="previewUrl"
          :src="previewUrl"
          class="img-thumbnail mt-2"
          style="max-height: 200px;"
        />
      </div>

      <!-- Fields -->
      <div class="mb-3">
        <label class="form-label">Brand</label>
        <input class="form-control" v-model="car.brand" required />
      </div>

      <div class="mb-3">
        <label class="form-label">Model</label>
        <input class="form-control" v-model="car.model" required />
      </div>

      <div class="mb-3">
        <label class="form-label">Year</label>
        <input class="form-control" type="number" v-model.number="car.year" required />
      </div>

      <div class="mb-3">
        <label class="form-label">Date</label>
        <input class="form-control" type="date" v-model="car.date" />
      </div>

      <div class="mb-3">
        <label class="form-label">Place</label>
        <input class="form-control" v-model="car.place" />
      </div>

      <div class="mb-3">
        <label class="form-label">Registration Number</label>
        <input class="form-control" type="number" v-model.number="car.number" />
      </div>

      <div class="mb-3">
        <label class="form-label">User ID (Email)</label>
        <input class="form-control" :value="user.email" disabled />
      </div>

      <div class="mb-3">
        <label class="form-label">Created At</label>
        <input class="form-control" type="date" v-model="car.createdAt"disabled />
      </div>

      <div class="mb-3">
        <label class="form-label">Updated At</label>
        <input class="form-control" type="date" v-model="car.updatedAt" disabled />
      </div>

      <div class="d-flex justify-content-between mt-4">
        <button class="btn btn-success" type="submit">Save</button>
        <button class="btn btn-secondary" type="button" @click="cancel">Cancel</button>
      </div>
    </form>
  </div>

</template>

<style scoped>
.container {
  max-width: 700px;
}
</style>
