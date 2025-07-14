import './assets/main.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import {
    faNetworkWired,
    faFileLines,
    faUser,
    faLaptopCode,
    faClipboardCheck
} from '@fortawesome/free-solid-svg-icons';

library.add(faNetworkWired, faFileLines, faUser, faLaptopCode, faClipboardCheck);

const app = createApp(App);
app.component('font-awesome-icon', FontAwesomeIcon);
app.use(router);
app.mount('#app');