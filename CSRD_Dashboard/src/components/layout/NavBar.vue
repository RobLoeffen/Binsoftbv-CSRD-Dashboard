<script setup lang="ts">
import { useRouter } from 'vue-router'

const emit = defineEmits<{ 'toggle-sidebar': [] }>()

const route = useRouter()

function redirectToHome() {
  route.push('/')
}
</script>

<template>
  <nav
    class="bg-[var(--vt-c-dashboard-background)] p-2 pt-4 flex items-center justify-between gap-4"
  >
    <div id="search-bar" class="flex flex-1 items-center justify-center relative">
      <input
        type="text"
        placeholder="search..."
        class="w-full bg-white px-4 py-2 text-black rounded-full focus:outline-none"
      />
      <button
        type="submit"
        class="absolute top-0 right-0 mt-1 mr-2 bg-[var(--vt-c-widget-background)] text-white p-2 rounded-full cursor-pointer shadow-md"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 24 24"
          width="24"
          height="24"
          fill="none"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        >
          <circle cx="11" cy="11" r="7"></circle>
          <line x1="16.65" y1="16.65" x2="21" y2="21"></line>
        </svg>
      </button>
    </div>
    <div class="nav-content p-4 flex gap-4">
      <p class="text-white cursor-pointer" @click="redirectToHome">Link 1</p>
      <p class="text-white cursor-pointer" @click="redirectToHome">Link 2</p>
      <p class="text-white cursor-pointer" @click="redirectToHome">Link 2</p>
      <p class="text-white cursor-pointer" @click="redirectToHome">Link 2</p>
    </div>
    <button
      id="hamburger"
      class="align-left"
      aria-label="Toggle menu"
      @click="emit('toggle-sidebar')"
    >
      <span></span>
      <span></span>
      <span></span>
    </button>
  </nav>
</template>

<style scoped>
nav {
  top: 0;
  z-index: 999;
}

#hamburger {
  display: none;
  flex-direction: column;
  gap: 5px;
  background: none;
  border: none;
  cursor: pointer;
  padding: 8px;
}

#hamburger span {
  display: block;
  width: 24px;
  height: 2px;
  background-color: currentColor;
}

@media (max-width: 900px) {
  .nav-content {
    display: none;
  }

  #hamburger {
    display: flex;
  }
}
</style>
