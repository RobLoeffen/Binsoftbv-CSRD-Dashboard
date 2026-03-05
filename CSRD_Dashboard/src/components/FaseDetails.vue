<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'

const route = useRoute()
const router = useRouter()

const faseId = computed(() => route.params.faseId as string)

const faseData: Record<string, { labels: string[]; values: number[]; title: string }> = {
  '1': {
    labels: ['Sub A', 'Sub B', 'Sub C', 'Sub D'],
    values: [30, 45, 80, 25],
    title: 'Fase 1 - Details',
  },
  '2': {
    labels: ['Item X', 'Item Y', 'Item Z'],
    values: [50, 70, 40],
    title: 'Fase 2 - Details',
  },
  '3': {
    labels: ['Grondstofwinning', 'Productie', 'Transport', 'Gebruik', 'Verbranding', 'Recycling'],
    values: [85, 90, 75, 80, 70, 100],
    title: 'Fase 3 - Co2 uitstoot',
  },
  '4': {
    labels: ['Section A', 'Section B'],
    values: [20, 80],
    title: 'Fase 4 - Details',
  },
  '5': {
    labels: ['Component 1', 'Component 2', 'Component 3'],
    values: [55, 80, 45],
    title: 'Fase 5 - Details',
  },
}

const currentFaseData = computed(() => {
  const data = faseData[faseId.value] ?? faseData['1']
  return data as { labels: string[]; values: number[]; title: string }
})

const pieConfig = computed(() =>
  createPieChartConfig(
    currentFaseData.value.labels,
    currentFaseData.value.values,
    currentFaseData.value.title,
  ),
)

function handlePieClick(params: { dataIndex: number }) {
  router.push(`/materials/${params.dataIndex + 1}`)
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
        <h1 class="text-3xl font-mono font-bold text-white relative z-10">Fase {{ faseId }}</h1>
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

      <div class="flex-1 pb-2">
        <section
          class="h-[450px] rounded-2xl bg-[var(--vt-c-grey-soft)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl hover:scale-[1.01] cursor-pointer group relative overflow-hidden"
          aria-label="Pie chart section"
        >
          <ChartWrapper :option="pieConfig" @click="handlePieClick" />
        </section>
      </div>
    </section>
  </div>
</template>
