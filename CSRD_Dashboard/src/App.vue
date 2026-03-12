<script setup lang="ts">
import { ref } from 'vue'
import NavBar from '@/components/layout/NavBar.vue'
import SideBar from '@/components/layout/SideBar.vue'
import FooterLayout from '@/components/layout/FooterLayout.vue'

const sidebarOpen = ref(false)
</script>

<template>
  <NavBar @toggle-sidebar="sidebarOpen = !sidebarOpen" />
  <SideBar :is-open="sidebarOpen" @close="sidebarOpen = false" />
  <main>
    <router-view v-slot="{ Component }">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </main>
  <FooterLayout />
</template>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
