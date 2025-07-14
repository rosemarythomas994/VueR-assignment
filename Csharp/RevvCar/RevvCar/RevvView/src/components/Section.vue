
<script setup lang="ts">
import api from '../images/api.png';
import tech from '../images/tech.png';
import early from '../images/early.png';


const purpleCards = [
  {
    icon: ['fas', 'network-wired'],
    title: 'API Options',
    description: 'Connect your apps directly with leading auctions.',
    image: api
  },
  {
    icon: ['fas', 'laptop-code'],
    title: 'Tech-Driven Work Flow',
    description: 'Integrate technology into your workflow for efficiency.',
    image: tech
  },
  {
    icon: ['fas', 'clipboard-check'],
    title: 'Early-Stage Condition Reporting',
    description: 'Faster vehicle condition reports to optimize strategy.',
    image: early
  }
];
</script>


<template>
  <section class="features-section mb-4">
    <div class="container">
      <h2 class="text-center">
        Maximize net returns at auction using REVV’s comprehensive solution
      </h2>
      <div class="row g-4 mt-4">
        <div class="col-lg-4" v-for="(card, index) in purpleCards" :key="index">
          <div class="card purple-card p-4 h-100 text-start">
            <div class="icon text-purple mb-3">
            <font-awesome-icon :icon="card.icon" class="icon mb-3" />
            </div>
            <h3>{{ card.title }}</h3>
            <p>{{ card.description }}</p>
            <img :src="card.image" :alt="card.title" class="img-fluid rounded" />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
.purple-card {
  background-color: #f3e8ff;
}
.purple-card:hover {
  background-color: #6b21a8;
  color: white;
}
.icon {
  font-size: 2rem;
  color: #6b21a8;
}
.purple-card:hover .icon {
  color: white;
}
</style>
