<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import { useShapeDistribution } from '@/composables/useShapeDistribution'

const route = useRoute()
const router = useRouter()

const materialName = computed(() => route.params.materialName as string)

const { distribution, loading, error, fetchDistribution } = useShapeDistribution()

onMounted(() => {
  fetchDistribution(materialName.value)
})

const chartConfig = computed(() => {
  if (!distribution.value) return null
  const labels = distribution.value.shapeCounts.map((sc) => sc.shapeType)
  const values = distribution.value.shapeCounts.map((sc) => sc.count)
  return createPieChartConfig(labels, values, distribution.value.materialName)
})

function handlePieClick(params: { dataIndex: number }) {
  const shapeType = distribution.value?.shapeCounts[params.dataIndex]?.shapeType
  if (shapeType) {
    router.push(
      `/ecoparts?material=${encodeURIComponent(materialName.value)}&shape=${encodeURIComponent(shapeType)}`,
    )
  }
}

const goBack = () => {
  router.back()
}
</script>

<template>
  <div class="min-h-screen w-full p-4 sm:p-6 md:p-8 flex items-center justify-center">
    <section
      class="w-full max-w-[1250px] rounded-3xl bg-[var(--vt-c-grey)] p-4 sm:p-6 flex flex-col gap-4 sm:gap-6"
      aria-label="Dashboard content area"
    >
      <header class="relative flex items-center justify-between">
        <div
          class="absolute inset-0 bg-gradient-to-r from-purple-500/20 to-blue-500/20 blur-xl rounded-lg"
        ></div>
        <h1 class="text-3xl font-mono font-bold text-white relative z-10">
          Shapes – {{ materialName }}
        </h1>
        <button
          @click="goBack"
          class="px-4 py-2 bg-[var(--vt-c-grey-soft)] text-white rounded-lg hover:bg-opacity-80 cursor-pointer transition-colors font-mono relative z-10 shadow-lg border border-white/5 hover:border-white/10 hover:shadow-xl"
        >
          ← Back
        </button>
      </header>

      <p v-if="loading" class="text-center text-white font-mono">Loading shape distribution...</p>

      <p v-else-if="error" class="text-center text-red-400 font-mono">{{ error }}</p>

      <p
        v-else-if="!distribution || distribution.shapeCounts.length === 0"
        class="text-center text-gray-400 font-mono"
      >
        No shape data found for {{ materialName }}.
      </p>

      <section
        v-else-if="chartConfig"
        class="h-[450px] w-full rounded-2xl bg-[var(--vt-c-grey-soft)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl"
        aria-label="Shape distribution pie chart"
      >
        <ChartWrapper :option="chartConfig" @click="handlePieClick" />
      </section>
    </section>
  </div>
</template>
