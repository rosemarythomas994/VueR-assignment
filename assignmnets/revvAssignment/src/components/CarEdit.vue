<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
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
    __v: string;
    date: string;
    userId: string;
}

const route = useRoute(); // to get carId from URL
const router = useRouter(); // to navigate between pages
const carId = route.params.id as string; // extract :id from route

const car = ref<Car | null>(null); // will store the car to be edited
const error = ref(''); // error message (if any)


// Retrieves the full list of cars from localStorage.
// Finds the car whose _id matches the carId from the URL.
onMounted(() => {
    const stored = localStorage.getItem('cars');
    if (stored) {
        const cars = JSON.parse(stored) as Car[];
        const found = cars.find(c => c._id === carId);
        if (found) {
            const cloned = JSON.parse(JSON.stringify(found));

            // Convert full ISO to just YYYY-MM-DD for <input type="date">
            ['createdAt', 'updatedAt', 'date'].forEach(field => {
                if (cloned[field] && cloned[field].includes('T')) {
                    cloned[field] = cloned[field].split('T')[0];  // get yyyy-mm-dd
                }
            });
            // Handles US format MM/DD/YYYY by converting it to YYYY-MM-DD.
            if (cloned.date && cloned.date.includes('/')) {
                const [month, day, year] = cloned.date.split('/');
                // Pad month and day to 2 digits
                const mm = month.padStart(2, '0');
                const dd = day.padStart(2, '0');
                cloned.date = `${year}-${mm}-${dd}`;
            }
            // Sets the car value for form binding.
            car.value = cloned;
        } else {
            error.value = 'Car not found.';
        }
    } else {
        error.value = 'No car data available.';
    }
});


function save() {
    if (!car.value) return;

    // validation
    if (!car.value.brand.trim() || !car.value.model.trim()) {
        error.value = 'Brand and Model are required.';
        return;
    }
    if (car.value.year < 1900 || car.value.year > new Date().getFullYear() + 1) {
        error.value = 'Year must be between 1900 and next year.';
        return;
    }

    const stored = localStorage.getItem('cars'); //Reads the cars array stored in the browser’s localStorage.
    if (stored) {
        const cars = JSON.parse(stored) as Car[]; //If cars exist in storage, parse the JSON string into an array of Car objects.
        const index = cars.findIndex(c => c._id === car.value?._id); // find index
        if (index !== -1) {
            car.value.updatedAt = new Date().toISOString();
            cars[index] = { ...car.value }; //creates a shallow copy (to avoid reference issues).
            localStorage.setItem('cars', JSON.stringify(cars));// back to string
        }
    }

    router.push({ name: 'Welcome' });
}

function cancel() {
    router.push({ name: 'Welcome' });
}
</script>

<template>
  <Header />
  <div class="container my-5 d-flex justify-content-center">
    <form v-if="car" @submit.prevent="save" class="w-100" style="max-width: 600px;">
      <h2 class="text-center mb-4">Edit Car</h2>
      <div v-if="error" class="alert alert-danger text-center">{{ error }}</div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Brand</label>
        <div class="col-sm-8">
          <input class="form-control" v-model="car.brand" required />
        </div>
      </div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Model</label>
        <div class="col-sm-8">
          <input class="form-control" v-model="car.model" required />
        </div>
      </div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Year</label>
        <div class="col-sm-8">
          <input class="form-control" type="number" v-model.number="car.year" required />
        </div>
      </div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Created</label>
        <div class="col-sm-8">
          <input class="form-control" type="date" v-model="car.createdAt" />
        </div>
      </div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Updated</label>
        <div class="col-sm-8">
          <input class="form-control" type="date" v-model="car.updatedAt" />
        </div>
      </div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Place</label>
        <div class="col-sm-8">
          <input class="form-control" v-model="car.place" />
        </div>
      </div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Reg Number</label>
        <div class="col-sm-8">
          <input class="form-control" type="number" v-model.number="car.number" />
        </div>
      </div>

      <div class="form-group mb-2 row">
        <label class="col-sm-4 col-form-label">Date</label>
        <div class="col-sm-8">
          <input class="form-control" type="date" v-model="car.date" />
        </div>
      </div>

      <div class="form-group mb-4 row">
        <label class="col-sm-4 col-form-label">User ID</label>
        <div class="col-sm-8">
          <input class="form-control" v-model="car.userId" />
        </div>
      </div>

      <div class="form-group d-flex justify-content-center gap-3">
        <button class="btn btn-success" type="submit">Save</button>
        <button class="btn btn-secondary" type="button" @click="cancel">Cancel</button>
      </div>
    </form>
  </div>
  <Footer />
</template>



<style scoped>
.container {
    max-width: 600px;
}

.alert {
    padding: 0.75rem;
    background-color: #f8d7da;
    color: #721c24;
    border-radius: 0.25rem;
}
</style>
