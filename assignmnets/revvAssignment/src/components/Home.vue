<script setup >
import Header from './Header.vue';
import Footer from './Footer.vue';
import Slides from './Slides.vue';
import Cards from './Cards.vue';
import Section from './Section.vue';

</script>
<template>
  <div >
    <Header />
    <Slides />
    <Cards />
    <Section />
    <Footer />
  </div>
</template>


<style scoped>
.page-container {
  max-width: 1200px;
  margin: 0 auto;
}
</style>
