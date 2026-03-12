<script setup lang="ts">
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import { createBarChartConfig } from './Dashboard/barChartConfig'
import { createLineChartConfig } from './Dashboard/lineChartConfig'
import { createRadarChartConfig } from './Dashboard/radarChartConfig'
import PageLayout from './layout/PageLayout.vue'
import WidgetCard from './layout/WidgetCard.vue'
import { useRouter } from 'vue-router'
const route = useRouter()

const pieConfig = createPieChartConfig(
  ['Scope 1', 'Scope 2', 'Scope 3 (Downstream)', 'Scope 3 (Upstream)'],
  [40, 20, 80, 20],
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

const radarConfig = createRadarChartConfig(
  ['Fase 1', 'Fase 2', 'Fase 3', 'Fase 4', 'Fase 5'],
  [40, 20, 80, 10, 50],
  'CSRD fases',
)

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
  route.push(`/fase/${params.dataIndex + 1}`)
}
</script>

<template>
  <PageLayout title="Dashboard - CRSD">
    <WidgetCard class="px-6 py-8 text-center hover:shadow-xl" aria-label="Introduction section">
      <p class="font-mono md:text-sm font-semibold text-[var(--vt-c-primary-text)]">
        De Corporate Sustainability Reporting Directive (CSRD) is een Europese richtlijn die
        bedrijven verplicht om uitgebreid te rapporteren over hun impact op milieu, mens en bestuur
        (ESG). Het doel is om meer transparantie te creëren over duurzaamheidsprestaties en
        greenwashing tegen te gaan. Grote bedrijven en later ook beursgenoteerde mkb-bedrijven
        moeten volgens vaste Europese standaarden (ESRS) rapporteren, waarbij de informatie
        bovendien gecontroleerd wordt door een accountant. Zo wil de Europese Unie duurzame
        bedrijfsvoering stimuleren en investeerders beter informeren.
      </p>
    </WidgetCard>

    <div class="grid grid-cols-1 sm:grid-cols-3 gap-4 sm:gap-6">
      <WidgetCard
        v-for="stat in stats"
        :key="stat.label"
        class="px-6 py-5 flex items-center gap-4"
      >
        <div class="flex flex-col min-w-0">
          <span class="font-mono text-xs font-semibold uppercase tracking-widest text-[#8ABFB8]">
            {{ stat.label }}
          </span>
          <span class="font-mono text-2xl font-bold leading-tight" :style="{ color: stat.accent }">
            {{ stat.value }}
          </span>
          <span class="font-mono text-xs text-[#8ABFB8] mt-0.5">{{ stat.unit }}</span>
        </div>
      </WidgetCard>
    </div>

    <div class="flex-1 grid grid-cols-1 lg:grid-cols-2 gap-4 sm:gap-6 min-h-[400px]">
      <WidgetCard
        class="min-h-[400px] p-4"
        aria-label="Line chart section"
      >
        <ChartWrapper :option="lineConfig" />
      </WidgetCard>
       <WidgetCard
        class="min-h-[400px] p-4"
        aria-label="Radar chart section"
      >
        <ChartWrapper :option="radarConfig" />
      </WidgetCard>
      <WidgetCard
        class="min-h-[400px] p-4"
        aria-label="Bar chart section"
      >
        <ChartWrapper :option="barConfig" />
      </WidgetCard>
      <WidgetCard
        class="min-h-[500px] p-4"
        aria-label="Pie chart section"
      >
        <ChartWrapper :option="pieConfig" @click="handlePieClick" />
      </WidgetCard>
    </div>
  </PageLayout>
</template>
