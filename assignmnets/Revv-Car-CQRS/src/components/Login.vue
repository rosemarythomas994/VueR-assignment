<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useForm, useField } from 'vee-validate';
import { required, email as emailRule } from '@vee-validate/rules';
import axios from '../api'
import { useAuthStore } from '../stores/authStore'; // <-- Pinia store


const router = useRouter();
const authStore = useAuthStore(); // <-- initialize store

const { handleSubmit, errors } = useForm();
const { value: emailValue } = useField('email', [required, emailRule]);
const passwordValue = ref('');
const passwordErrors = ref<string[]>([]);
const rememberValue = ref(false);
const showPassword = ref(false);
const validatePasswordRules = (value: string): string[] => {
  const errs: string[] = [];
  if (!value) errs.push('Password is required.');
  if (value.length < 8) errs.push('At least 8 characters');
  if (!/([A-Za-z].*){4}/.test(value)) errs.push('At least 4 letters');
  if (!/\d/.test(value)) errs.push('At least one number');
  if (!/[!@#$%^&*]/.test(value)) errs.push('At least one special character');
  return errs;
};
function togglePasswordVisibility() {
  showPassword.value = !showPassword.value;
}
const validatePasswordManually = () => {
  passwordErrors.value = validatePasswordRules(passwordValue.value);
};

const onSubmit = handleSubmit(async (values) => {
  validatePasswordManually();
  if (passwordErrors.value.length > 0) return;

  try {
    const response = await axios.post('http://localhost:5015/api/Login/login', {
      username: values.email,
      password: passwordValue.value,
    });
    const { token, username } = response.data;
        authStore.login(token, values.email);

    router.push({ path: '/welcome' });
  } catch (err: any) {
    alert(err.response?.data?.message || 'Invalid email or password');
  }
});
</script>

<template>
  <div class="login-page">


    <div class="login-container">
      <div class="login-box shadow">
        <h2 class="text-center text-primary mb-4 border-bottom pb-2">Log in</h2>

        <form @submit.prevent="onSubmit">
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input v-model="emailValue" type="text" class="form-control" placeholder="Email" />
            <span class="error-msg">{{ errors.email }}</span>
          </div>

          <!-- <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input v-model="passwordValue" type="password" class="form-control" placeholder="Password"
              @blur="validatePasswordManually" />
            <div class="error-msg" v-if="passwordErrors.length > 0">
              <div v-for="(msg, i) in passwordErrors" :key="i">{{ msg }}</div>
            </div>
          </div> -->

          <div class="mb-3">
  <label for="password" class="form-label">Password</label>
  <div class="input-group position-relative">
    <input
      v-model="passwordValue"
      :type="showPassword ? 'text' : 'password'"
      class="form-control pe-5"
      placeholder="Password"
      @blur="validatePasswordManually"
    />

    <!-- Eye Toggle Icon -->
    <span class="eye-toggle" @click="togglePasswordVisibility">
      <!-- Open Eye SVG -->
      <svg v-if="!showPassword" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512">
        <path fill="currentColor"
          d="M572.52 241.4C518.78 135.5 407.15 64 288 64S57.22 135.5 3.48 241.4a48.27 48.27 0 0 0 0 29.2C57.22 376.5 168.85 448 288 448s230.78-71.5 284.52-177.4a48.27 48.27 0 0 0 0-29.2zM288 400c-97.05 0-188.94-55-238.2-144C99.06 167 190.95 112 288 112s188.94 55 238.2 144C476.94 345 385.05 400 288 400zm0-272a128 128 0 1 0 128 128A128.14 128.14 0 0 0 288 128z"/>
      </svg>

      <!-- Closed Eye (Slash) SVG -->
      <svg v-else xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512">
        <path fill="currentColor"
          d="M320 400c-141.4 0-257.6-93.1-303.5-224C62.4 45.1 178.6-48 320-48s257.6 93.1 303.5 224C577.6 306.9 461.4 400 320 400zM320 80c-44.2 0-80 35.8-80 80 0 17.7 5.8 34.1 15.4 47.4l113.9 113.9C365.9 312.2 349.5 320 320 320c-44.2 0-80-35.8-80-80 0-10.3 2-20.1 5.6-29.2L107.7 101.7c-6.2-6.2-6.2-16.4 0-22.6s16.4-6.2 22.6 0l432 432c6.2 6.2 6.2 16.4 0 22.6s-16.4 6.2-22.6 0L348.3 346.3c-17.1 10.1-36.9 15.7-58.3 15.7z"/>
      </svg>
    </span>
  </div>

  <!-- Validation Errors -->
  <div class="error-msg" v-if="passwordErrors.length > 0">
    <div v-for="(msg, i) in passwordErrors" :key="i">{{ msg }}</div>
  </div>
</div>




          <div class="form-check mb-3">
            <input v-model="rememberValue" type="checkbox" class="form-check-input" id="remember" />
            <label class="form-check-label" for="remember">Remember Me</label>
          </div>

          <button type="submit" class="btn btn-login w-100">Log in</button>
        </form>

        <div class="mt-3 text-center">
          <a href="#" class="text-decoration-none text-primary">Forgot Password?</a>
        </div>
        <div class="text-center mt-2">
          Don’t have an account?
          <router-link to="/register" class="text-success fw-semibold text-decoration-none">Register</router-link>
        </div>
      </div>
    </div>

 
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
  line-height: 1.4;
}

.btn-login {
  background-color: #003399;
  color: white;
}

.btn-login:hover {
  background-color: #002080;
}

.input-group {
  position: relative;
}

.eye-toggle {
  position: absolute;
  top: 50%;
  right: 14px;
  transform: translateY(-50%);
  cursor: pointer;
  width: 16px;
  height: 16px;
  color: #b3bbc2;
  display: flex;
  align-items: center;
  justify-content: center;
  pointer-events: auto;
}

.eye-toggle svg {
  width: 100%;
  height: 100%;
}


</style>