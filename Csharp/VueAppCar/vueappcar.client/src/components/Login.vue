<script setup lang="ts">
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  import { useForm, useField } from 'vee-validate';
  import { required, email as emailRule } from '@vee-validate/rules';
  import axios from 'axios';
  import Header from './Header.vue';
  import Footer from './Footer.vue';

  const router = useRouter();
  const { handleSubmit, errors } = useForm();
  const { value: emailValue } = useField('email', [required, emailRule]);
  const passwordValue = ref('');
  const passwordErrors = ref<string[]>([]);

  const validatePasswordRules = (value: string): string[] => {
    const errs: string[] = [];
    if (!value) errs.push('Password is required.');
    if (value.length < 8) errs.push('At least 8 characters');
    if (!/([A-Za-z].*){4}/.test(value)) errs.push('At least 4 letters');
    if (!/\d/.test(value)) errs.push('At least one number');
    if (!/[!@#$%^&*]/.test(value)) errs.push('At least one special character');
    return errs;
  };

  const validatePasswordManually = () => {
    passwordErrors.value = validatePasswordRules(passwordValue.value);
  };

  const onSubmit = handleSubmit(async (values) => {
    validatePasswordManually();
    if (passwordErrors.value.length > 0) return;

    try {
      const response = await axios.post('/api/Login/login', {
        username: values.email,
        password: passwordValue.value,
      });
      const { token, username } = response.data;
      localStorage.setItem('token', token);
      localStorage.setItem('loggedInUser', JSON.stringify({ name: username }));
      router.push({ path: '/welcome', query: { name: username } });
    } catch (err: any) {
      alert(err.response?.data?.message || 'Invalid email or password');
    }
  });
</script>

<template>
  <div class="login-page">
    <Header />
    <div class="login-container">
      <h2>Login</h2>
      <form @submit.prevent="onSubmit">
        <div class="form-group">
          <label for="email">Email</label>
          <input v-model="emailValue" type="email" id="email" name="email" class="form-control" :class="{ 'is-invalid': errors.email }" />
          <div v-if="errors.email" class="invalid-feedback">{{ errors.email }}</div>
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <input v-model="passwordValue" type="password" id="password" name="password" class="form-control" @input="validatePasswordManually" :class="{ 'is-invalid': passwordErrors.length > 0 }" />
          <div v-if="passwordErrors.length > 0" class="invalid-feedback">
            <div v-for="error in passwordErrors" :key="error">{{ error }}</div>
          </div>
        </div>
        <div class="form-check">
          <input type="checkbox" id="remember" name="remember" class="form-check-input" />
          <label for="remember" class="form-check-label">Remember me</label>
        </div>
        <button type="submit" class="btn btn-primary">Login</button>
      </form>
    </div>
    <Footer />
  </div>
</template>

<style scoped>
  .login-page {
    min-height: 100vh;
    display: flex;
    flex-direction: column;
  }

  .login-container {
    flex: 1;
    max-width: 400px;
    margin: 0 auto;
    padding: 2rem;
  }

  .form-group {
    margin-bottom: 1rem;
  }

  .invalid-feedback {
    color: #dc3545;
    font-size: 0.875rem;
  }

  .btn-primary {
    background-color: #1e3a8a;
    border-color: #1e3a8a;
  }

    .btn-primary:hover {
      background-color: #1e40af;
      border-color: #1e40af;
    }
</style>
