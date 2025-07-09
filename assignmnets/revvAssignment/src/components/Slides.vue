
<script setup lang="ts">
import car1 from '../images/car.jpg';
import car2 from '../images/car2.png';
import car3 from '../images/car4.jpg';

const carouselItems = [
  {
    image: car1,
    title: '2025 Vehicle Auction Schedule',
    description: 'Don’t miss your chance to bid on high-quality vehicles.',
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
</script>

<template>
  <div id="revvCarousel" class="carousel slide mb-4" data-bs-ride="carousel">
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
</template>


<style scoped>
.carousel-slide {
  height: 500px;
  background-size: cover;
  background-position: center;
  position: relative;
}
.carousel-caption {
  position: absolute;
  bottom: 30%;
  left: 50%;
  transform: translateX(-50%);
  color: white;
  background: rgba(0, 0, 0, 0.5);
  padding: 1rem;
  border-radius: 1rem;
  text-align: center;
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
</style>