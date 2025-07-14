import './assets/main.css';
// Importing Bootstrap
// This imports the minified CSS of Bootstrap to use its ready-made UI styles (grid system, buttons, cards, etc.).
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';

// This imports createApp, which is a Vue 3 function used to initialize and mount the Vue app.
import { createApp } from 'vue';
// This imports the root Vue component from the file App.vue.
// This component is the starting point of your app, acting like a container for all other components.
import App from './App.vue';
// This imports your custom router configuration (likely from a router/index.js or router.js file).
// This enables page navigation using Vue Router (e.g., SPA-style navigation between /home, /about).
import router from './router';

// These lines are importing:
// library: A central place to register icons for use in Vue.
// FontAwesomeIcon: A Vue component that lets you use <font-awesome-icon /> in your templates.
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

// These are specific icons from FontAwesome’s free solid icon set.
// You only import what you need (not the whole library).
import {
  faNetworkWired,
  faFileLines,
  faUser,
  faLaptopCode,
  faClipboardCheck
} from '@fortawesome/free-solid-svg-icons';

library.add(faNetworkWired, faFileLines, faUser, faLaptopCode, faClipboardCheck);

const app = createApp(App);
// Globally registers the <font-awesome-icon /> component so you can use it in any template without importing it every time.
app.component('font-awesome-icon', FontAwesomeIcon);
app.use(router);
app.mount('#app');
