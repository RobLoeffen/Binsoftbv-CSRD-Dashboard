<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter()
</script>

<template>
  <button
    @click="router.back()"
    class="px-4 py-2 bg-[var(--vt-c-widget-background)] text-[var(--vt-c-primary-text)] rounded-lg cursor-pointer transition-all duration-300 font-mono shadow-lg border border-white/5 hover:border-white/10 hover:shadow-xl"
  >
    ← Terug
  </button>
</template>
