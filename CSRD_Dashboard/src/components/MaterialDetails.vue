<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'

const route = useRoute()
const router = useRouter()

const materialId = computed(() => route.params.materialId as string)

const pieChartData = [
  {
    labels: ['Ecoparts', 'New parts'],
    values: [65, 35],
    title: 'PET',
  },
  {
    labels: ['Ecoparts', 'New parts'],
    values: [70, 30],
    title: 'HDPE',
  },
  {
    labels: ['Ecoparts', 'New parts'],
    values: [60, 40],
    title: 'LDPE',
  },
  {
    labels: ['Ecoparts', 'New parts'],
    values: [75, 25],
    title: 'PP',
  },
  {
    labels: ['Ecoparts', 'New parts'],
    values: [85, 10],
    title: 'PS',
  },
  {
    labels: ['Ecoparts', 'New parts'],
    values: [68, 32],
    title: 'PVC',
  },
]

const chartConfigs = computed(() =>
  pieChartData.map((chartData, chartIndex) =>
    createPieChartConfig(chartData.labels, chartData.values, chartData.title, (materialId) => {
      router.push(`/ecoparts/${chartIndex + 1}-material-${materialId}`)
    }),
  ),
)

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
          Material {{ materialId }}
        </h1>
        <button
          @click="goBack"
          class="px-4 py-2 bg-[var(--vt-c-grey-soft)] text-white rounded-lg hover:bg-opacity-80 cursor-pointer transition-colors font-mono relative z-10 shadow-lg border border-white/5 hover:border-white/10 hover:shadow-xl"
        >
          ← Back
        </button>
      </header>

      <section
        class="w-full rounded-2xl bg-[var(--vt-c-grey-soft)] px-6 py-8 text-center shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-xl"
        aria-label="Introduction section"
      >
        <p class="font-mono md:text-sm font-semibold text-white">
          De Corporate Sustainability Reporting Directive (CSRD) is een Europese richtlijn die
          bedrijven verplicht om uitgebreid te rapporteren over hun impact op milieu, mens en
          bestuur (ESG). Het doel is om meer transparantie te creëren over duurzaamheidsprestaties
          en greenwashing tegen te gaan. Grote bedrijven en later ook beursgenoteerde mkb-bedrijven
          moeten volgens vaste Europese standaarden (ESRS) rapporteren, waarbij de informatie
          bovendien gecontroleerd wordt door een accountant. Zo wil de Europese Unie duurzame
          bedrijfsvoering stimuleren en investeerders beter informeren.
        </p>
      </section>

      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 pb-2">
        <section
          v-for="(config, index) in chartConfigs"
          :key="index"
          class="rounded-2xl bg-[var(--vt-c-grey-soft)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl hover:scale-[1.02] cursor-pointer group relative overflow-hidden"
          :aria-label="`Pie chart ${index + 1}`"
        >
          <ChartWrapper type="pie" :data="config.data" :options="config.options" />
        </section>
      </div>
    </section>
  </div>
</template>
