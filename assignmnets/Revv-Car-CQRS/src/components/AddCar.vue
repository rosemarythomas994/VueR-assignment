<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import Header from './Header.vue';
import Footer from './Footer.vue';

const router = useRouter();
const errorMessage = ref('');
const user = JSON.parse(localStorage.getItem('loggedInUser') || '{}');
const token = localStorage.getItem('token');

// File input
const imageFile = ref<File | null>(null);

// Form data
const form = ref({
    brand: '',
    model: '',
    year: new Date().getFullYear(),
    place: '',
    number: 0,
    date: '',
    userId: user.email || ''
});

function onFileChange(e: Event) {
    const target = e.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
        imageFile.value = target.files[0];
    }
}

async function saveNewCar() {
    errorMessage.value = '';

    if (!form.value.brand || !form.value.model || !imageFile.value) {
        errorMessage.value = 'Image, Brand, and Model are required.';
        return;
    }

    const formData = new FormData();
    formData.append('ImageFile', imageFile.value); 
    formData.append('Brand', form.value.brand);
    formData.append('Model', form.value.model);
    formData.append('Year', form.value.year.toString());
    formData.append('Place', form.value.place);
    formData.append('Number', form.value.number.toString());
    formData.append('Date', form.value.date);
    formData.append('UserId', form.value.userId);

    try {
        await axios.post('http://localhost:5015/api/Car', formData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });

        router.push('/welcome');
    } catch (err: any) {
  let msg = 'Failed to add car.';
  if (axios.isAxiosError(err)) {
    if (err.response?.data?.errors) {
      const errors = err.response.data.errors as Record<string, string[]>;
      console.error('Validation errors:', errors);
      msg = Object.entries(errors)
        .map(([key, val]) => `${key}: ${val.join(', ')}`)
        .join(' | ');
    } else if (err.response?.data?.message) {
      msg = err.response.data.message;
    }
  }
  errorMessage.value = msg;
}
}

function goToAllCars() {
    router.push('/all-cars');
}
function goBackToWelcome() {
    router.push('/welcome');
}

</script>

<template>
    <Header />
    <div class="container my-5">
        <h2 class="mb-4">Add New Car</h2>

        <form @submit.prevent="saveNewCar" class="card p-4 shadow-sm">
            <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>

            <div class="mb-3">
                <label class="form-label">Image</label>
                <input class="form-control" type="file" accept="image/*" @change="onFileChange" required />
            </div>

            <div class="mb-3">
                <label class="form-label">Brand</label>
                <input class="form-control" v-model="form.brand" required />
            </div>

            <div class="mb-3">
                <label class="form-label">Model</label>
                <input class="form-control" v-model="form.model" required />
            </div>

            <div class="mb-3">
                <label class="form-label">Year</label>
                <input class="form-control" type="number" v-model="form.year" min="1900" max="2099" required />
            </div>

            <div class="mb-3">
                <label class="form-label">Place</label>
                <input class="form-control" v-model="form.place" required />
            </div>

            <div class="mb-3">
                <label class="form-label">Registration Number</label>
                <input class="form-control" type="number" v-model="form.number" required />
            </div>

            <div class="mb-3">
                <label class="form-label">Date</label>
                <input class="form-control" type="date" v-model="form.date" required />
            </div>

        
<div class="d-flex gap-2 justify-content-start">
    <button type="submit" class="btn btn-success">Save</button>
    <button type="button" class="btn btn-secondary" @click="goToAllCars">View All Cars</button>
    <button type="button" class="btn btn-outline-danger" @click="goBackToWelcome">Cancel</button>
</div>

        </form>
    </div>
    <Footer />
</template>

<style scoped>
.container {
    max-width: 600px;
}
</style>
