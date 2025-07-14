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
    const { value: emailValue } = useField('username', [required, emailRule]);
    const { value: roleValue } = useField('role', [required]);
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
            await axios.post('/api/Login/register', {
                username: values.username,
                password: passwordValue.value,
                role: values.role,
            });
            alert('Registration successful! Please log in.');
            router.push('/login');
        } catch (err: any) {
            alert(err.response?.data || 'Registration failed');
        }
    });
</script>

<template>
    <div class="register-page">
        <Header />
        <div class="register-container">
            <h2>Register</h2>
            <form @submit.prevent="onSubmit">
                <div class="form-group">
                    <label for="username">Email</label>
                    <input v-model="emailValue" type="email" id="username" name="username" class="form-control" :class="{ 'is-invalid': errors.username }" />
                    <div v-if="errors.username" class="invalid-feedback">{{ errors.username }}</div>
                </div>
                <div class="form-group">
                    <label for="role">Role</label>
                    <input v-model="roleValue" type="text" id="role" name="role" class="form-control" :class="{ 'is-invalid': errors.role }" />
                    <div v-if="errors.role" class="invalid-feedback">{{ errors.role }}</div>
                </div>
                <div class="form-group">
                    <label for="password">Password</label>
                    <input v-model="passwordValue" type="password" id="password" name="password" class="form-control" @input="validatePasswordManually" :class="{ 'is-invalid': passwordErrors.length > 0 }" />
                    <div v-if="passwordErrors.length > 0" class="invalid-feedback">
                        <div v-for="error in passwordErrors" :key="error">{{ error }}</div>
                    </div>
                </div>
                <button type="submit" class="btn btn-primary">Register</button>
            </form>
        </div>
        <Footer />
    </div>
</template>

<style scoped>
    .register-page {
        min-height: 100vh;
        display: flex;
        flex-direction: column;
    }

    .register-container {
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