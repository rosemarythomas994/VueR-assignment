<template>
    <div class="home-page">
        <Header />
        <Slides />
        <Cards />
        <Section />
        <Footer />
    </div>
</template>
<script setup lang="ts">
    import Header from './Header.vue';
    import Footer from './Footer.vue';
    import Slides from './Slides.vue';
    import Cards from './Cards.vue';
    import Section from './Section.vue';
</script>
<style scoped>
    .home-page {
        display: flex;
        flex-direction: column;
        min-height: 100vh;
    }
</style>