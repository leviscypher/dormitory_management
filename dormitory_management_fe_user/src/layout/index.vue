<script setup lang="ts">
import TheHeader from '@/layout/Header/index.vue'
import { RouterView } from 'vue-router'
</script>

<template>
  <main class="main-content main-bg">
    <TheHeader />
    <RouterView />
  </main>
</template>

<style scoped>
.main-bg{
  background: #9999992b;
  min-height: 100vh;
}
</style>
