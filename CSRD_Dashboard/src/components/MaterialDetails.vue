<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import PageLayout from './layout/PageLayout.vue'
import WidgetCard from './layout/WidgetCard.vue'
import BackButton from './layout/BackButton.vue'

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
  pieChartData.map((chartData) =>
    createPieChartConfig(chartData.labels, chartData.values, chartData.title),
  ),
)

const navigateToShapes = (materialTitle: string) => {
  router.push(`/shapes/${materialTitle}`)
}
</script>

<template>
  <PageLayout :title="`Material ${materialId}`">
    <template #actions>
      <BackButton />
    </template>

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

    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 pb-2">
      <WidgetCard
        v-for="(config, index) in chartConfigs"
        :key="index"
        class="h-[300px] p-4 hover:shadow-2xl hover:scale-[1.02] cursor-pointer"
        :aria-label="`Pie chart ${index + 1}`"
        @click="navigateToShapes(pieChartData[index]?.title ?? '')"
      >
        <ChartWrapper :option="config" />
      </WidgetCard>
    </div>
  </PageLayout>
</template>
