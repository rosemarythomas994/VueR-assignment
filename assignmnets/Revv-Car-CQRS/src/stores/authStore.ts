import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || '',
    userEmail: localStorage.getItem('loggedInUser')
      ? JSON.parse(localStorage.getItem('loggedInUser')!).email
      : '',
  }),

  getters: {
    isLoggedIn: (state) => !!state.token,
    username: (state) => state.userEmail?.split('@')[0] || '',
  },

  actions: {
    login(token: string, email: string) {
      this.token = token;
      this.userEmail = email;
      localStorage.setItem('token', token);
      localStorage.setItem('loggedInUser', JSON.stringify({ email }));
    },

    logout() {
      this.token = '';
      this.userEmail = '';
      localStorage.removeItem('token');
      localStorage.removeItem('loggedInUser');
    }
  }
});
