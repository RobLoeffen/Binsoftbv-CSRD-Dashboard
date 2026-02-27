<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useEcopartDetail } from '@/composables/useEcopartDetail'

const route = useRoute()
const router = useRouter()

const ecopartId = route.params.ecopartId as string

const { ecopart, loading, error, fetchEcopartDetail } = useEcopartDetail()

onMounted(() => {
  fetchEcopartDetail(ecopartId)
})

const showCO2 = ref(false)
const showDimensions = ref(false)

const isRectangular = computed(() => ecopart.value?.shape.shapeType.toLowerCase() === 'rectangular')

const isCylinder = computed(() => ecopart.value?.shape.shapeType.toLowerCase() === 'cylinder')

const goBack = () => {
  router.back()
}

// Convert meters to centimeters
const toCm = (value: number | undefined) => (value !== undefined ? (value * 100).toFixed(1) : '-')

// Convert kg to grams
const toGrams = (value: number) => (value * 1000).toFixed(2)
</script>

<template>
  <div class="min-h-screen w-full p-4 sm:p-6 md:p-8 flex items-center justify-center">
    <section
      class="w-full max-w-[1250px] rounded-3xl bg-[var(--vt-c-grey)] p-4 sm:p-6 flex flex-col gap-4 sm:gap-6"
      aria-label="Ecopart detail"
    >
      <!-- Header -->
      <header class="relative flex items-center justify-between">
        <div
          class="absolute inset-0 bg-gradient-to-r from-purple-500/20 to-blue-500/20 blur-xl rounded-lg"
        ></div>
        <h1 class="text-3xl font-mono font-bold text-white relative z-10">
          {{ ecopart?.name ?? '...' }}
        </h1>
        <button
          @click="goBack"
          class="px-4 py-2 bg-[var(--vt-c-grey-soft)] text-white rounded-lg hover:bg-opacity-80 cursor-pointer transition-colors font-mono relative z-10 shadow-lg border border-white/5 hover:border-white/10 hover:shadow-xl"
        >
          ← Back
        </button>
      </header>

      <!-- Loading state -->
      <p v-if="loading" class="text-center text-white font-mono py-12">Loading...</p>

      <!-- Error state -->
      <p v-else-if="error" class="text-center text-red-400 font-mono py-12">{{ error }}</p>

      <template v-else-if="ecopart">
        <!-- Material Info Card -->
        <section
          class="rounded-2xl bg-[var(--vt-c-grey-soft)] p-6 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300"
        >
          <div class="flex flex-col gap-4">
            <div class="flex items-center gap-3 flex-wrap">
              <span
                class="text-sm font-semibold text-purple-300 bg-purple-500/20 px-3 py-1 rounded font-mono"
              >
                {{ ecopart.material.name }}
              </span>
              <span
                class="text-sm font-semibold text-blue-300 bg-blue-500/20 px-3 py-1 rounded font-mono"
              >
                {{ ecopart.shape.shapeType }}
              </span>
            </div>
            <div class="pt-2 border-t border-white/10 grid grid-cols-2 gap-2">
              <span class="text-sm text-gray-400 font-mono"
                >Dichtheid: <span class="text-white">{{ ecopart.material.density }} g/cm³</span></span
              >
              <span class="text-sm text-gray-400 font-mono"
                >Emissie factor:
                <span class="text-white">{{ ecopart.material.emissionFactor }}</span></span
              >
            </div>
          </div>
        </section>

        <!-- CO₂ Carbon Footprint (Expandable) -->
        <section
          class="rounded-2xl bg-gradient-to-br from-green-900/30 to-green-800/20 shadow-lg border border-green-500/20 hover:border-green-500/40 transition-all duration-300 overflow-hidden"
        >
          <button
            @click="showCO2 = !showCO2"
            class="w-full p-6 text-left flex items-center justify-between cursor-pointer hover:bg-green-500/5 transition-colors"
          >
            <h2 class="text-xl font-mono font-bold text-green-400 flex items-center gap-2">
              <span>CO₂ voetafdruk</span>
            </h2>
            <span
              class="text-2xl text-green-400 transition-transform duration-300"
              :class="{ 'rotate-180': showCO2 }"
            >
              ▼
            </span>
          </button>

          <transition
            enter-active-class="transition-all duration-300 ease-out"
            leave-active-class="transition-all duration-300 ease-in"
            enter-from-class="max-h-0 opacity-0"
            enter-to-class="max-h-96 opacity-100"
            leave-from-class="max-h-96 opacity-100"
            leave-to-class="max-h-0 opacity-0"
          >
            <div v-show="showCO2" class="px-6 pb-6">
              <div class="pt-4 border-t border-green-500/20 flex flex-col gap-3">
                <div class="flex items-baseline gap-2">
                  <span class="text-4xl font-bold text-green-300">{{
                    ecopart.carbonFootprint
                  }}</span>
                  <span class="text-xl text-gray-300">kg CO₂eq</span>
                </div>
                <p class="text-sm text-gray-400 font-mono">
                  Berekening: {{ ecopart.mass }} kg × {{ ecopart.material.emissionFactor }} emissie
                  factor
                </p>
              </div>
            </div>
          </transition>
        </section>

        <!-- Dimensions & Mass (Expandable) -->
        <section
          class="rounded-2xl bg-[var(--vt-c-grey-soft)] shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 overflow-hidden"
        >
          <button
            @click="showDimensions = !showDimensions"
            class="w-full p-6 text-left flex items-center justify-between cursor-pointer hover:bg-white/5 transition-colors"
          >
            <h2 class="text-xl font-mono font-bold text-white">Dimensies & Massa</h2>
            <span
              class="text-2xl text-gray-400 transition-transform duration-300"
              :class="{ 'rotate-180': showDimensions }"
            >
              ▼
            </span>
          </button>

          <transition
            enter-active-class="transition-all duration-300 ease-out"
            leave-active-class="transition-all duration-300 ease-in"
            enter-from-class="max-h-0 opacity-0"
            enter-to-class="max-h-96 opacity-100"
            leave-from-class="max-h-96 opacity-100"
            leave-to-class="max-h-0 opacity-0"
          >
            <div v-show="showDimensions" class="px-6 pb-6">
              <div class="pt-4 border-t border-white/10">
                <ul class="space-y-3 font-mono">
                  <!-- Rectangular: Length, Width, Height -->
                  <template v-if="isRectangular">
                    <li class="flex justify-between text-gray-300">
                      <span class="text-gray-400">Lengte:</span>
                      <span class="font-semibold"
                        >{{ toCm(ecopart.shape.dimensions.Length) }} cm</span
                      >
                    </li>
                    <li class="flex justify-between text-gray-300">
                      <span class="text-gray-400">Breedte:</span>
                      <span class="font-semibold"
                        >{{ toCm(ecopart.shape.dimensions.Width) }} cm</span
                      >
                    </li>
                    <li class="flex justify-between text-gray-300">
                      <span class="text-gray-400">Hoogte:</span>
                      <span class="font-semibold"
                        >{{ toCm(ecopart.shape.dimensions.Height) }} cm</span
                      >
                    </li>
                  </template>

                  <!-- Cylinder: Radius, Height only -->
                  <template v-else-if="isCylinder">
                    <li class="flex justify-between text-gray-300">
                      <span class="text-gray-400">Straal:</span>
                      <span class="font-semibold"
                        >{{ toCm(ecopart.shape.dimensions.Radius) }} cm</span
                      >
                    </li>
                    <li class="flex justify-between text-gray-300">
                      <span class="text-gray-400">Hoogte:</span>
                      <span class="font-semibold"
                        >{{ toCm(ecopart.shape.dimensions.Height) }} cm</span
                      >
                    </li>
                  </template>

                  <li class="flex justify-between text-gray-300 pt-2 border-t border-white/10">
                    <span class="text-gray-400">Massa:</span>
                    <span class="font-semibold text-green-400">{{ toGrams(ecopart.mass) }} g</span>
                  </li>
                </ul>
              </div>
            </div>
          </transition>
        </section>
      </template>
    </section>
  </div>
</template>
