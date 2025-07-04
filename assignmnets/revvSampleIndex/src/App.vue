
<script setup lang="ts">
import revvLogo from '@/images/revv.png';
import car1 from '@/images/car.jpg';
import car2 from '@/images/car2.png';
import car3 from '@/images/car4.jpg';
import api from '@/images/api.png';
import tech from '@/images/tech.png';
import early from '@/images/early.png';

const carouselItems = [
  {
    image: car1,
    title: '2025 Vehicle Auction Schedule',
    description: 'Don’t miss your chance to bid on thousands of high-quality vehicles.',
    button: 'View Auction Schedule →',
    link: '#'
  },
  {
    image: car2,
    title: 'Bid on Premium Used Cars',
    description: 'Explore our wide inventory of premium pre-owned vehicles.',
    button: 'Start Bidding →',
    link: '#'
  },
  {
    image: car3,
    title: 'Fast, Transparent, Reliable',
    description: 'Enjoy a seamless auction experience with trusted inspections.',
    button: 'Learn More →',
    link: '#'
  }
];

const greenCards = [
  {
    icon: 'fa-solid fa-network-wired',
    title: 'AI-Driven',
    description: 'We streamline and simplify your processes with cutting-edge AI.'
  },
  {
    icon: 'fa-solid fa-file-lines',
    title: 'Data-Driven',
    description: 'We ensure transparent and informed decision-making using data.'
  },
  {
    icon: 'fa-solid fa-person',
    title: 'Customer Focused',
    description: 'Guiding your remarketing needs with powerful insights.'
  }
];

const purpleCards = [
  {
    icon: 'fa-solid fa-network-wired',
    title: 'API Options',
    description: 'Connect your apps directly with leading auctions.',
    image: api
  },
  {
    icon: 'fa-solid fa-laptop-code',
    title: 'Tech-Driven Work Flow',
    description: 'Integrate technology into your workflow for efficiency.',
    image: tech
  },
  {
    icon: 'fa-solid fa-clipboard-check',
    title: 'Early-Stage Condition Reporting',
    description: 'Faster vehicle condition reports to optimize strategy.',
    image: early
  }
];
</script>

<template>
  <div class="page-container">
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg navbar-light bg-white shadow-sm py-3">
      <div class="container d-flex justify-content-between align-items-center">
        <RouterLink class="navbar-brand" to="/">
          <img :src="revvLogo" alt="Logo" height="50" />
        </RouterLink>
        <div class="d-flex align-items-center gap-4">
          <a href="https://revv.vin/about-us" class="text-dark fw-medium text-decoration-none">About us</a>
          
          <RouterLink to="/login" class="login-btn">Login</RouterLink>
        </div>
      </div>
    </nav>

    <!-- Carousel -->
    <div id="revvCarousel" class="carousel slide" data-bs-ride="carousel">
      <div class="carousel-inner">
        <div
          v-for="(item, index) in carouselItems"
          :key="index"
          :class="['carousel-item', { active: index === 0 }]"
        >
          <div class="carousel-slide" :style="{ backgroundImage: `url(${item.image})` }">
            <div class="carousel-caption">
              <h1>{{ item.title }}</h1>
              <p>{{ item.description }}</p>
              <a :href="item.link" class="yellow-btn">{{ item.button }}</a>
            </div>
          </div>
        </div>
      </div>
      <button class="carousel-control-prev" type="button" data-bs-target="#revvCarousel" data-bs-slide="prev">
        <span class="carousel-control-prev-icon"></span>
      </button>
      <button class="carousel-control-next" type="button" data-bs-target="#revvCarousel" data-bs-slide="next">
        <span class="carousel-control-next-icon"></span>
      </button>
    </div>

    <!-- Feature Cards -->
    <section class="features-section">
      <div class="container">
        <h2 class="text-center mb-4">
          Optimize auction decisioning and liquidity with our AI-powered tool
        </h2>
        <div class="row g-4">
          <div class="col-lg-4" v-for="(feature, index) in greenCards" :key="index">
            <div class="card green-card p-4 text-start h-100">
              <div class="icon mb-3">
                <i :class="feature.icon"></i>
              </div>
              <h3>{{ feature.title }}</h3>
              <p>{{ feature.description }}</p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Purple Cards -->
    <section class="features-section">
      <div class="container">
        <h2 class="text-center">
          Maximize net returns at auction using REVV’s comprehensive solution
        </h2>
        <div class="row g-4 mt-4">
          <div class="col-lg-4" v-for="(card, index) in purpleCards" :key="index">
            <div class="card purple-card p-4 h-100 text-start">
              <div class="icon text-purple mb-3">
                <i :class="card.icon"></i>
              </div>
              <h3>{{ card.title }}</h3>
              <p>{{ card.description }}</p>
              <img :src="card.image" :alt="card.title" class="img-fluid rounded" />
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Footer -->
    <footer class="text-white py-4" style="background-color: #000066">
      <div class="container d-flex flex-column flex-md-row justify-content-between align-items-start">
        <div class="text-center text-md-start mb-3 mb-md-0">
          <RouterLink to="/">
            <img :src="revvLogo" alt="REVV Logo" height="40" class="mb-3" />
          </RouterLink>
          <div class="d-flex justify-content-center justify-content-md-start gap-2 mb-2">
            <a href="https://play.google.com/store/apps/details?id=com.revv.vin" target="_blank">
              <img src="https://upload.wikimedia.org/wikipedia/commons/7/78/Google_Play_Store_badge_EN.svg" alt="Google Play" height="40" />
            </a>
            <a href="https://apps.apple.com/us/app/revv-ai/id6740824555" target="_blank">
              <img src="https://developer.apple.com/assets/elements/badges/download-on-the-app-store.svg" alt="App Store" height="40" />
            </a>
          </div>
          <p class="small mb-0">© 2025 REVV. All rights reserved.</p>
        </div>
        <div class="text-center text-md-end">
          <a href="#" class="d-block text-white text-decoration-none mb-1">About us</a>
          <a href="#" class="d-block text-white text-decoration-none mb-1">Privacy policy</a>
          <a href="#" class="d-block text-white text-decoration-none">Terms of use</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<style scoped>
@import 'https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css';
@import 'https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css';

/* Container for overall layout */
.page-container {
  max-width: 1200px;
  margin: 0 auto;
  
}

/* General styling */
body {
  font-family: sans-serif;
  background-color: #f3f4f6;
  color: #1f2937;
  margin: 0;
  padding: 0;
}

a {
  text-decoration: none;
  color: inherit;
}

.login-btn {
  background-color: #1e3a8a;
  color: white;
  padding: 0.375rem 1rem;
  border-radius: 8px;
  font-size: 0.875rem;
  font-weight: 600;
}

.login-btn:hover {
  background-color: #1e40af;
}

.carousel-slide {
  height: 500px;
  background-size: cover;
  background-position: center;
  position: relative;
  width: 100%;
}

.carousel-caption {
  position: absolute;
  bottom: 30%;
  left: 50%;
  transform: translateX(-50%);
  color: white;
  text-align: center;
  max-width: 700px;
  background-color: rgba(0, 0, 0, 0.5);
  padding: 1.5rem;
  border-radius: 1rem;
}

.yellow-btn {
  background-color: #facc15;
  padding: 0.75rem 1.5rem;
  border-radius: 8px;
  font-weight: bold;
  color: black;
}

.yellow-btn:hover {
  background-color: #eab308;
}

.features-section {
  padding: 4rem 1rem;
  background: white;
  text-align: center;
}

.card {
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  text-align: left;
  cursor: pointer;
  transition: all 0.3s;
}

.card img {
  width: 100%;
  border-radius: 0.5rem;
  margin-top: 1rem;
}

.green-card {
  background-color: #d1fae5;
}

.green-card:hover {
  background-color: #065f46;
  color: white;
}

.purple-card {
  background-color: #f3e8ff;
}

.purple-card:hover {
  background-color: #6b21a8;
  color: white;
}

.icon {
  font-size: 2rem;
  margin-bottom: 1rem;
}

footer {
  background-color: #000066;
  color: white;
  padding: 2rem 1rem;
}
</style>
