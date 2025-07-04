
<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import Header from './Header.vue';
import Footer from './Footer.vue';

const router = useRouter();

const email = ref('');
const password = ref('');
const remember = ref(false);
const emailError = ref('');
const passwordError = ref('');

// Sample user data (frontend only)
interface User {
name: string;
  email: string;
  password: string;
}

const users: User[] = [
  { name: 'Rose Mary', email: 'rose@gmail.com', password: 'Rose@1234' },
  { name: 'Admin John', email: 'admin@example.com', password: 'Admin@123' },
];

const validatePassword = (password: string): string => {
  const minLength = 8;
  const letterRegex = /([A-Za-z].*){4}/;
  const digitRegex = /[0-9]/;
  const specialCharRegex = /[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/;
  const errors = [];

  if (password.length < minLength) errors.push("at least 8 characters");
  if (!letterRegex.test(password)) errors.push("at least 4 letters");
  if (!digitRegex.test(password)) errors.push("at least one digit");
  if (!specialCharRegex.test(password)) errors.push("at least one special character");

  return errors.length ? `Password must contain ${errors.join(', ')}.` : '';
};

const handleSubmit = () => {
  emailError.value = '';
  passwordError.value = '';

  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  let valid = true;

  if (!emailPattern.test(email.value.trim())) {
    emailError.value = 'Please enter a valid email address.';
    valid = false;
  }

  const pwdError = validatePassword(password.value);
  if (pwdError) {
    passwordError.value = pwdError;
    valid = false;
  }

  if (!valid) return;

  // Check against user array
  const matchedUser = users.find(
    user => user.email === email.value && user.password === password.value
  );

  if (matchedUser) {
  router.push({ path: '/welcome', query: { email: email.value, name: matchedUser.name } });
} else {
  passwordError.value = 'Invalid email or password.';
}
};
</script>

<template>
  <div class="login-page">
    <Header />

    <!-- Login Form -->
    <div class="login-container">
      <div class="login-box shadow">
        <h2 class="text-center text-primary mb-4 border-bottom pb-2">Log in</h2>
        <form @submit.prevent="handleSubmit">
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <div class="error-msg" v-if="emailError">{{ emailError }}</div>
            <input v-model="email" type="text" class="form-control" id="email" />
          </div>

          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <div class="error-msg" v-if="passwordError">{{ passwordError }}</div>
            <input v-model="password" type="password" class="form-control" id="password" />
          </div>

          <div class="form-check mb-3">
            <input v-model="remember" type="checkbox" class="form-check-input" id="remember" />
            <label class="form-check-label" for="remember">Remember Me</label>
          </div>

          <button type="submit" class="btn btn-login w-100">Log in</button>

          <div class="mt-3 text-center">
            <a href="#" class="text-decoration-none text-primary">Forgot Password?</a>
          </div>
          <div class="text-center mt-2">
            Don’t have an account?
            <a href="#" class="text-success fw-semibold text-decoration-none">Register</a>
          </div>
        </form>
      </div>
    </div>

    <Footer />
  </div>
</template>


<style scoped>
.login-page {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  background-color: #f8fafc;
}

.login-container {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 2rem 1rem;
  margin: auto;
  width: 100%;
}

.login-box {
  width: 100%;
  max-width: 500px;
  background-color: white;
  padding: 2rem;
  border-radius: 1rem;
}

.error-msg {
  color: red;
  font-size: 0.875rem;
  margin-top: 5px;
}

.btn-login {
  background-color: #003399;
  color: white;
}

.btn-login:hover {
  background-color: #002080;
}

.navbar-brand img {
  max-height: 48px;
}
</style>
