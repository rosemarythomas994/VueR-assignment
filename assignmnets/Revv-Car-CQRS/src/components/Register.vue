<script setup lang="ts">
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import { useForm, useField } from 'vee-validate';
import { required, email as emailRule } from '@vee-validate/rules';
import axios from 'axios';
import Header from './Header.vue';
import Footer from './Footer.vue';

const router = useRouter();
const { handleSubmit, errors } = useForm();
const { value: username } = useField('username', [required, emailRule]);
const { value: role } = useField('role', [required]);
const password = ref('');
const passwordErrors = ref<string[]>([]);

const validatePassword = (value: string): string[] => {
  const errs: string[] = [];
  if (!value) errs.push('Password is required.');
  if (value.length < 8) errs.push('At least 8 characters.');
  if (!/([A-Za-z].*){4}/.test(value)) errs.push('At least 4 letters.');
  if (!/\d/.test(value)) errs.push('At least one number.');
  if (!/[!@#$%^&*]/.test(value)) errs.push('At least one special character.');
  return errs;
};

const validatePasswordManually = () => {
  passwordErrors.value = validatePassword(password.value);
};

const onSubmit = handleSubmit(async (values) => {
  validatePasswordManually();
  if (passwordErrors.value.length > 0) return;

  try {
    await axios.post('http://localhost:5015/api/Login/register', {
      username: values.username,
      password: password.value,
      role: values.role,
    });
    alert('Registration successful! Please login.');
    router.push('/login');
  } catch (err: any) {
    alert(err.response?.data?.message || 'Registration failed');
  }
});
</script>

<template>
  <div class="login-page">
    <Header />

    <div class="login-container">
      <div class="login-box shadow">
        <h2 class="text-center text-success mb-4 border-bottom pb-2">Register</h2>

        <form @submit.prevent="onSubmit">
          <div class="mb-3">
            <label for="username" class="form-label">Email</label>
            <input v-model="username" type="email" class="form-control" placeholder="Email" />
            <span class="error-msg">{{ errors.username }}</span>
          </div>

          <div class="mb-3">
            <label for="role" class="form-label">Role</label>
            <input v-model="role" type="text" class="form-control" placeholder="Role" />
            <span class="error-msg">{{ errors.role }}</span>
          </div>

          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input
              v-model="password"
              type="password"
              class="form-control"
              placeholder="Password"
              @blur="validatePasswordManually"
            />
            <div class="error-msg" v-if="passwordErrors.length > 0">
              <div v-for="(msg, i) in passwordErrors" :key="i">{{ msg }}</div>
            </div>
          </div>

          <button type="submit" class="btn btn-login w-100">Register</button>
        </form>

        <div class="text-center mt-3">
          Already have an account?
          <router-link to="/login" class="text-primary fw-semibold text-decoration-none">Login</router-link>
        </div>
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
  line-height: 1.4;
}

.btn-login {
  background-color: #003399;
  color: white;
}

.btn-login:hover {
  background-color: #002080;
}
</style>
