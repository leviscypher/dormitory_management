<script setup lang="ts">
import TheHeader from '@/layout/Header/index.vue'
import TheSidebar from '@/layout/Sidebar/index.vue'
import { RouterView } from 'vue-router'
</script>

<template>
  <The-Sidebar />
  <main class="main-content">
    <The-Header />
    <div class="pt-3 ps-3 pe-3">

      <RouterView />
    </div>
  </main>
</template>

<style scoped></style>
