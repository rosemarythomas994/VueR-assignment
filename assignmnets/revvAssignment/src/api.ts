// src/services/api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.BASE_API_URL, // from your .env file
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
