
<script setup lang="ts">
const greenCards = [
  {
    icon: ['fas', 'network-wired'],
    title: 'AI-Driven',
    description: 'We streamline and simplify your processes with cutting-edge AI.'
  },
  {
    icon: ['fas', 'file-lines'],
    title: 'Data-Driven',
    description: 'We ensure transparent and informed decision-making using data.'
  },
  {
    icon: ['fas', 'user'],
    title: 'Customer Focused',
    description: 'Guiding your remarketing needs with powerful insights.'
  }
];
</script>
<template>
  <section class="features-section mb-4">
    <div class="container">
      <h2 class="text-center mb-4">
        Optimize auction decisioning and liquidity with our AI-powered tool
      </h2>
      <div class="row g-4">
        <div class="col-lg-4" v-for="(feature, index) in greenCards" :key="index">
          <div class="card green-card p-4 text-start h-100">
            <div class="icon mb-3">
              <font-awesome-icon :icon="feature.icon" class="card-icon" />
            </div>
            <h3>{{ feature.title }}</h3>
            <p>{{ feature.description }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>


<style scoped>
.green-card {
  background-color: #d1fae5;
}
.green-card:hover {
  background-color: #065f46;
  color: white;
}
.icon {
  font-size: 2rem;
  margin-bottom: 1rem;
  color: #065f46;
}
.green-card:hover .icon {
  color: white;
}
</style>
