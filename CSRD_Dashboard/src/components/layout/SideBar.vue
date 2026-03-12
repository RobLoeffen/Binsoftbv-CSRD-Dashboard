<script setup lang="ts">
defineProps<{ isOpen?: boolean }>()
defineEmits<{ close: [] }>()
</script>

<template>
  <aside class="bg-[#070a06]" :class="{ 'is-open': isOpen }">
    <p>Sidebar</p>
  </aside>
</template>

<style scoped>
aside {
  height: 100vh;
  position: sticky;
  top: 0;
}
</style>
