<script setup lang="ts">
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import { createBarChartConfig } from './Dashboard/barChartConfig'
import router from '@/router'

const pieConfig = createPieChartConfig(
  ['Fase 1', 'Fase 2', 'Fase 3', 'Fase 4', 'Fase 5'],
  [40, 20, 80, 10, 50],
  'CSRD fases',
  (index) => {
    router.push(`/fase/${index + 1}`)
  },
)

const barConfig = createBarChartConfig(
  ['Fase 1', 'Fase 2', 'Fase 3', 'Fase 4', 'Fase 5'],
  [40, 20, 80, 10, 50],
  'Bar Chart Example',
  'Data One',
)
</script>

<template>
  <div class="min-h-screen w-full p-4 sm:p-6 md:p-8 flex items-center justify-center">
    <section
      class="w-full max-w-[1250px] rounded-3xl bg-[var(--vt-c-grey)] p-4 sm:p-6 flex flex-col gap-4 sm:gap-6"
      aria-label="Dashboard content area"
    >
      <header class="relative">
        <div
          class="absolute inset-0 bg-gradient-to-r from-purple-500/20 to-blue-500/20 blur-xl rounded-lg"
        ></div>
        <h1 class="text-3xl font-mono font-bold text-white relative z-10">Dashboard - CRSD</h1>
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

      <div class="flex-1 grid grid-cols-1 lg:grid-cols-2 gap-4 sm:gap-6 min-h-[400px]">
        <section
          class="min-h-[300px] lg:h-full rounded-2xl bg-[var(--vt-c-grey-soft)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl hover:scale-[1.01] cursor-pointer group relative overflow-hidden"
          aria-label="Bar chart section"
        >
          <ChartWrapper type="bar" :data="barConfig.data" :options="barConfig.options" />
        </section>
        <section
          class="min-h-[300px] lg:h-full rounded-2xl bg-[var(--vt-c-grey-soft)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl hover:scale-[1.01] cursor-pointer group relative overflow-hidden"
          aria-label="Pie chart section"
        >
          <ChartWrapper type="pie" :data="pieConfig.data" :options="pieConfig.options" />
        </section>
      </div>
    </section>
  </div>
</template>
