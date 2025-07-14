<script setup lang="ts">
  import { ref, computed, onMounted } from 'vue';
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
  const name = ref(route.query.name || 'Guest');
  const cars = ref<Car[]>([]);
  const currentPage = ref(1);
  const pageSize = 6;
  const isAdding = ref(false);
  const errorMessage = ref('');

  const newCar = ref<Car>({
    _id: '',
    createdAt: '',
    updatedAt: '',
    image: '',
    brand: '',
    model: '',
    year: 0,
    place: '',
    number: 0,
    date: '',
    userId: '',
  });

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

  function openAdd() {
    newCar.value = {
      _id: '',
      createdAt: new Date().toISOString().split('T')[0],
      updatedAt: new Date().toISOString().split('T')[0],
      image: '',
      brand: '',
      model: '',
      year: new Date().getFullYear(),
      place: '',
      number: 0,
      date: new Date().toISOString().split('T')[0],
      userId: localStorage.getItem('loggedInUser') ? JSON.parse(localStorage.getItem('loggedInUser')!).name : '',
    };
    errorMessage.value = '';
    isAdding.value = true;
  }

  async function saveNewCar() {
    if (!newCar.value.brand || !newCar.value.model) {
      errorMessage.value = 'Brand and Model are required';
      return;
    }
    if (newCar.value.year < 1900 || newCar.value.year > new Date().getFullYear() + 1) {
      errorMessage.value = 'Year must be between 1900 and next year';
      return;
    }
    if (newCar.value.number <= 0) {
      errorMessage.value = 'Registration number must be positive';
      return;
    }

    try {
      const response = await axios.post('/api/Car', newCar.value, {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
      cars.value.push(response.data);
      isAdding.value = false;
      errorMessage.value = '';
    } catch (err: any) {
      errorMessage.value = err.response?.data?.message || 'Failed to add car';
    }
  }

  async function deleteCar(id: string) {
    if (!confirm('Are you sure you want to delete this car?')) return;
    try {
      await axios.delete(`/api/Car/${id}`, {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
      cars.value = cars.value.filter(c => c._id !== id);
    } catch (err: any) {
      alert(err.response?.data?.message || 'Failed to delete car');
    }
  }

  onMounted(async () => {
    const userStr = localStorage.getItem('loggedInUser');
    if (userStr) {
      const user = JSON.parse(userStr);
      name.value = user.name || 'Guest';
    } else {
      router.push('/login');
    }

    try {
      const response = await axios.get('/api/Car', {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
      cars.value = response.data.map((car: Car) => ({
        ...car,
        createdAt: car.createdAt.split('T')[0],
        updatedAt: car.updatedAt.split('T')[0],
        date: car.date.split('T')[0],
      }));
    } catch (err: any) {
      if (err.response?.status === 401) {
        alert('Session expired. Please log in again.');
        localStorage.removeItem('token');
        localStorage.removeItem('loggedInUser');
        router.push('/login');
      }
    }
  });
</script>

<template>
  <Header />
  <div class="container my-5">
    <div class="welcome-card shadow mb-4">
      <h1 class="greeting">Welcome, {{ name }}</h1>
      <p class="tagline">We're glad to have you back 🎉</p>
      <button class="btn btn-success mt-3" @click="openAdd">Add New Car</button>
    </div>

    <div v-if="paginatedCars.length === 0" class="text-center mb-4">
      <p>No cars available.</p>
    </div>

    <div class="row g-4 mb-4">
      <div class="col-md-4" v-for="car in paginatedCars" :key="car._id">
        <div class="card h-100 shadow-sm">
          <img :src="`/images/${car.image}`" class="card-img-top" alt="Car Image" />
          <div class="card-body">
            <h5 class="card-title">{{ car.brand }} {{ car.model }} ({{ car.year }})</h5>
            <p class="card-text">
              <strong>Created:</strong> {{ car.createdAt || 'N/A' }}<br />
              <strong>Updated:</strong> {{ car.updatedAt || 'N/A' }}<br />
              <strong>Location:</strong> {{ car.place }}<br />
              <strong>Reg No:</strong> {{ car.number }}<br />
              <strong>Date:</strong> {{ car.date }}<br />
              <strong>User ID:</strong> {{ car.userId || 'N/A' }}
            </p>
            <RouterLink class="btn btn-primary mt-2 me-2"
                        :to="{ name: 'CarEdit', params: { id: car._id }, query: { name } }">
              Edit
            </RouterLink>
            <button class="btn btn-danger mt-2" @click="deleteCar(car._id)">Delete</button>
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

    <div v-if="isAdding" class="modal-outer" role="dialog" aria-labelledby="modal-title-add" @click.self="isAdding = false">
      <div class="modal-dialog">
        <button type="button" class="btn-close" @click="isAdding = false" aria-label="Close">×</button>
        <h4 id="modal-title-add" class="mb-3">Add New Car</h4>
        <form @submit.prevent="saveNewCar">
          <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>
          <div class="form-group mb-2">
            <label>Image</label>
            <input class="form-control" type="text" v-model="newCar.image" />
          </div>
          <div class="form-group mb-2">
            <label>Created At</label>
            <input class="form-control" type="date" v-model="newCar.createdAt" />
          </div>
          <div class="form-group mb-2">
            <label>Updated At</label>
            <input class="form-control" type="date" v-model="newCar.updatedAt" />
          </div>
          <div class="form-group mb-2">
            <label>Brand</label>
            <input class="form-control" type="text" v-model="newCar.brand" required />
          </div>
          <div class="form-group mb-2">
            <label>Model</label>
            <input class="form-control" type="text" v-model="newCar.model" required />
          </div>
          <div class="form-group mb-2">
            <label>Year</label>
            <input class="form-control" type="number" v-model.number="newCar.year" required />
          </div>
          <div class="form-group mb-2">
            <label>Place</label>
            <input class="form-control" type="text" v-model="newCar.place" />
          </div>
          <div class="form-group mb-2">
            <label>Reg Number</label>
            <input class="form-control" type="number" v-model.number="newCar.number" required />
          </div>
          <div class="form-group mb-2">
            <label>Date</label>
            <input class="form-control" type="date" v-model="newCar.date" />
          </div>
          <div class="form-group mb-2">
            <label>User ID</label>
            <input class="form-control" type="text" v-model="newCar.userId" />
          </div>
          <div class="form-group mt-3 d-flex justify-content-between">
            <button type="submit" class="btn btn-success">Save</button>
            <button type="button" class="btn btn-secondary" @click="isAdding = false">Cancel</button>
          </div>
        </form>
      </div>
    </div>
  </div>
  <Footer />
</template>

<style scoped>
  .greeting {
    font-size: 2.5rem;
    color: #003399;
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
      background-color: #c82333;
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

  .modal-outer {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.6);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1000;
  }

  .modal-dialog {
    background: white;
    padding: 2rem;
    border-radius: 1rem;
    width: 90%;
    max-width: 500px;
    max-height: 80vh;
    overflow-y: auto;
    position: relative;
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.3);
  }

  .btn-close {
    position: absolute;
    top: 10px;
    right: 10px;
    background: none;
    border: none;
    font-size: 1.2rem;
    cursor: pointer;
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
    background-color: #f8d7da;
    color: #721c24;
  }
</style>
