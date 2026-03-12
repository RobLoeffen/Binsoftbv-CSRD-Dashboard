<script setup lang="ts">
withDefaults(defineProps<{ title?: string }>(), { title: '' })
</script>

<template>
  <div class="w-full p-4 sm:p-3 md:p-8 flex items-start">
    <section
      class="w-full rounded-3xl p-4 sm:p-6 flex flex-col gap-4 sm:gap-6"
      aria-label="Page content area"
    >
      <header class="relative flex items-center justify-between">
        <div class="absolute inset-0 blur-xl rounded-lg" />
        <h1 class="text-3xl font-mono font-bold text-[var(--vt-c-primary-text)] relative z-10">
          <slot name="title">{{ title }}</slot>
        </h1>
        <div class="relative z-10">
          <slot name="actions" />
        </div>
      </header>

      <slot />
    </section>
  </div>
</template>
