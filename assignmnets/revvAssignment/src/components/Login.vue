
<script setup lang="ts">
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import { useForm, useField } from 'vee-validate';
import { required, email as emailRule } from '@vee-validate/rules';
import Header from './Header.vue';
import Footer from './Footer.vue';

const router = useRouter();

const users = [
  { name: 'Rose Mary', email: 'rose@gmail.com', password: 'Rose@1234' },
  { name: 'Joseph Thomas', email: 'admin@example.com', password: 'Admin@123' }
];

// Setup vee-validate form
const { handleSubmit, errors } = useForm();
const { value: emailValue } = useField('email', [required, emailRule]);
const { value: rememberValue } = useField('remember');
const passwordValue = ref('');
const passwordErrors = ref<string[]>([]);

// Password validation function
const validatePasswordRules = (value: string): string[] => {
  const errs: string[] = [];
  if (!value) errs.push('Password is required.');
  if (value.length < 8) errs.push('At least 8 characters');
  if (!/([A-Za-z].*){4}/.test(value)) errs.push('At least 4 letters');
  if (!/\d/.test(value)) errs.push('At least one number');
  if (!/[!@#$%^&*]/.test(value)) errs.push('At least one special character');
  return errs;
};

// Validate password manually on blur and submit
const validatePasswordManually = () => {
  passwordErrors.value = validatePasswordRules(passwordValue.value);
};

// Submit handler
const onSubmit = handleSubmit((values) => {
  validatePasswordManually();

  // If password has errors, do not submit
  if (passwordErrors.value.length > 0) return;

  const matchedUser = users.find(
    user => user.email === values.email && user.password === passwordValue.value
  );

  if (matchedUser) {
    localStorage.setItem('loggedInUser', JSON.stringify(matchedUser));// locally stored the name 
    router.push({ path: '/welcome', query: { email: values.email, name: matchedUser.name } });
  } else {
    alert('Invalid email or password');
  }
});
</script>
<template>
  <div class="login-page">
    <Header />

    <div class="login-container">
      <div class="login-box shadow">
        <h2 class="text-center text-primary mb-4 border-bottom pb-2">Log in</h2>

        <form @submit.prevent="onSubmit">
          <!-- Email Field -->
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input v-model="emailValue" type="text" class="form-control" placeholder="Email" />
            <span class="error-msg">{{ errors.email }}</span>
          </div>

          <!-- Password Field -->
          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <input
              v-model="passwordValue"
              type="password"
              class="form-control"
              placeholder="Password"
              @blur="validatePasswordManually"
            />
            
            <div class="error-msg" v-if="passwordErrors.length > 0">
              <div v-for="(msg, i) in passwordErrors" :key="i">{{ msg }}</div>
            </div>
          </div>

          <!-- Remember Me -->
          <div class="form-check mb-3">
            <input v-model="rememberValue" type="checkbox" class="form-check-input" id="remember" />
            <label class="form-check-label" for="remember">Remember Me</label>
          </div>

          <!-- Submit Button -->
          <button type="submit" class="btn btn-login w-100">Log in</button>
        </form>

        <!-- Links -->
        <div class="mt-3 text-center">
          <a href="#" class="text-decoration-none text-primary">Forgot Password?</a>
        </div>
        <div class="text-center mt-2">
          Don’t have an account?
          <a href="#" class="text-success fw-semibold text-decoration-none">Register</a>
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
