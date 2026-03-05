<script setup lang="ts">
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import { createBarChartConfig } from './Dashboard/barChartConfig'
import { createLineChartConfig } from './Dashboard/lineChartConfig'
import router from '@/router'

const pieConfig = createPieChartConfig(
  ['Fase 1', 'Fase 2', 'Fase 3', 'Fase 4', 'Fase 5'],
  [40, 20, 80, 10, 50],
  'CSRD fases',
)

const barConfig = createBarChartConfig(
  ['Lasersnijden', 'Messnijden', 'Ponsen', 'CNC frezen', 'Watersnijden', 'Zagen'],
  [40, 20, 80, 10, 50],
  'Processen en hun CO₂-uitstoot',
  'CO₂/KG',
)

const lineConfig = createLineChartConfig(
  ['2022', '2023', '2024', '2025', '2026'],
  [3000, 2500, 2200, 1500, 900],
  'CO₂-uitstoot per jaar (CO₂e/KG)',
)

// TODO: replace with backend data
const stats = [
  {
    label: 'Totale uitstoot',
    value: '12.480',
    unit: 'CO₂e/KG',
    accent: '#FF967E',
  },
  {
    label: 'Bespaard',
    value: '2.100',
    unit: 'CO₂e/KG',
    accent: '#ABD006',
  },
  {
    label: 'Geplaatste orders',
    value: '348',
    unit: 'orders',
    accent: '#6FD6C8',
  },
]

function handlePieClick(params: { dataIndex: number }) {
  router.push(`/fase/${params.dataIndex + 1}`)
}
</script>

<template>
  <div class="min-h-screen w-full p-4 sm:p-6 md:p-8 flex items-center justify-center">
    <section
      class="w-full max-w-[1250px] rounded-3xl p-4 sm:p-6 flex flex-col gap-4 sm:gap-6"
      aria-label="Dashboard content area"
    >
      <header class="relative">
        <div class="absolute inset-0 blur-xl rounded-lg"></div>
        <h1 class="text-3xl font-mono font-bold text-[var(--vt-c-primary-text)] relative z-10">
          Dashboard - CRSD
        </h1>
      </header>

      <section
        class="w-full rounded-2xl bg-[var(--vt-c-widget-background)] px-6 py-8 text-center shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-xl"
        aria-label="Introduction section"
      >
        <p class="font-mono md:text-sm font-semibold text-[var(--vt-c-primary-text)]">
          De Corporate Sustainability Reporting Directive (CSRD) is een Europese richtlijn die
          bedrijven verplicht om uitgebreid te rapporteren over hun impact op milieu, mens en
          bestuur (ESG). Het doel is om meer transparantie te creëren over duurzaamheidsprestaties
          en greenwashing tegen te gaan. Grote bedrijven en later ook beursgenoteerde mkb-bedrijven
          moeten volgens vaste Europese standaarden (ESRS) rapporteren, waarbij de informatie
          bovendien gecontroleerd wordt door een accountant. Zo wil de Europese Unie duurzame
          bedrijfsvoering stimuleren en investeerders beter informeren.
        </p>
      </section>

      <div class="grid grid-cols-1 sm:grid-cols-3 gap-4 sm:gap-6">
        <div
          v-for="stat in stats"
          :key="stat.label"
          class="rounded-2xl bg-[var(--vt-c-widget-background)] px-6 py-5 flex items-center gap-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:scale-[1.02]"
        >
          <div class="flex flex-col min-w-0">
            <span class="font-mono text-xs font-semibold uppercase tracking-widest text-[#8ABFB8]">
              {{ stat.label }}
            </span>
            <span
              class="font-mono text-2xl font-bold leading-tight"
              :style="{ color: stat.accent }"
            >
              {{ stat.value }}
            </span>
            <span class="font-mono text-xs text-[#8ABFB8] mt-0.5">{{ stat.unit }}</span>
          </div>
        </div>
      </div>

      <div class="flex-1 grid grid-cols-1 lg:grid-cols-2 gap-4 sm:gap-6 min-h-[400px]">
        <section
          class="min-h-[400px] lg:h-full rounded-2xl bg-[var(--vt-c-widget-background)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl hover:scale-[1.01] cursor-pointer group relative overflow-hidden"
          aria-label="Bar chart section"
        >
          <ChartWrapper :option="barConfig" />
        </section>
        <section
          class="min-h-[400px] lg:h-full rounded-2xl bg-[var(--vt-c-widget-background)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl hover:scale-[1.01] cursor-pointer group relative overflow-hidden"
          aria-label="Pie chart section"
        >
          <ChartWrapper :option="pieConfig" @click="handlePieClick" />
        </section>
        <section
          class="min-h-[400px] lg:h-full rounded-2xl bg-[var(--vt-c-widget-background)] p-4 shadow-lg border border-white/5 hover:border-white/10 transition-all duration-300 hover:shadow-2xl hover:scale-[1.01] cursor-pointer group relative overflow-hidden"
          aria-label="Line chart section"
        >
          <ChartWrapper :option="lineConfig" />
        </section>
      </div>
    </section>
  </div>
</template>
