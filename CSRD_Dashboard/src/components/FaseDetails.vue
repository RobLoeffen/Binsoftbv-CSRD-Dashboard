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
</script>

<template>
  <PageLayout :title="`Fase ${faseId}`">
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

    <WidgetCard
      class="h-[450px] p-4 hover:shadow-2xl hover:scale-[1.01] cursor-pointer"
      aria-label="Pie chart section"
    >
      <ChartWrapper :option="pieConfig" @click="handlePieClick" />
    </WidgetCard>
  </PageLayout>
</template>
