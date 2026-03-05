<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { useRoute } from 'vue-router'
import { useEcopartDetail } from '@/composables/useEcopartDetail'
import PageLayout from './layout/PageLayout.vue'
import WidgetCard from './layout/WidgetCard.vue'
import BackButton from './layout/BackButton.vue'

const route = useRoute()

const ecopartId = route.params.ecopartId as string

const { ecopart, loading, error, fetchEcopartDetail } = useEcopartDetail()

onMounted(() => {
  fetchEcopartDetail(ecopartId)
})

const showCO2 = ref(false)
const showDimensions = ref(false)

const isRectangular = computed(() => ecopart.value?.shape.shapeType.toLowerCase() === 'rectangular')

const isCylinder = computed(() => ecopart.value?.shape.shapeType.toLowerCase() === 'cylinder')

const toCm = (value: number | undefined) => (value !== undefined ? (value * 100).toFixed(1) : '-')

const toGrams = (value: number) => (value * 1000).toFixed(2)
</script>

<template>
  <PageLayout :title="ecopart?.name ?? '...'">
    <template #actions>
      <BackButton />
    </template>

    <p v-if="loading" class="text-center text-[var(--vt-c-primary-text)] font-mono py-12">
      Loading...
    </p>
    <p v-else-if="error" class="text-center text-red-400 font-mono py-12">{{ error }}</p>

    <template v-else-if="ecopart">
      <WidgetCard class="p-6">
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
      </WidgetCard>

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
                <span class="text-4xl font-bold text-green-300">{{ ecopart.carbonFootprint }}</span>
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

      <WidgetCard class="overflow-hidden">
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
                <template v-if="isRectangular">
                  <li class="flex justify-between text-gray-300">
                    <span class="text-gray-400">Lengte:</span>
                    <span class="font-semibold"
                      >{{ toCm(ecopart.shape.dimensions.Length) }} cm</span
                    >
                  </li>
                  <li class="flex justify-between text-gray-300">
                    <span class="text-gray-400">Breedte:</span>
                    <span class="font-semibold">{{ toCm(ecopart.shape.dimensions.Width) }} cm</span>
                  </li>
                  <li class="flex justify-between text-gray-300">
                    <span class="text-gray-400">Hoogte:</span>
                    <span class="font-semibold"
                      >{{ toCm(ecopart.shape.dimensions.Height) }} cm</span
                    >
                  </li>
                </template>

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
      </WidgetCard>
    </template>
  </PageLayout>
</template>
