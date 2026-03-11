<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import { getBorderColorByValue, getTextColorByValue } from '../utils/colorScale'
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
    title: 'Scope 1 - Details',
  },
  '2': {
    labels: ['Item X', 'Item Y', 'Item Z'],
    values: [50, 70, 40],
    title: 'Scope 2 - Details',
  },
  '3': {
    labels: ['Verbranding', 'Hergebruik', 'Onderhoud', 'Transport'],
    values: [85, 90, 75, 20],
    title: 'Scope 3 - Downstream Details',
  },
  '4': {
    labels: ['Section A', 'Section B'],
    values: [20, 80],
    title: 'Scope 3 - Upstream Details',
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
  <PageLayout :title="`Scope ${faseId}`">
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

    <div class="grid grid-cols-1 lg:grid-cols-2 gap-4 sm:gap-6 min-h-[450px]">
      <WidgetCard
        class="h-[450px] p-4 hover:shadow-2xl hover:scale-[1.01] cursor-pointer"
        aria-label="Pie chart section"
      >
        <ChartWrapper :option="pieConfig" @click="handlePieClick" />
      </WidgetCard>
      <WidgetCard
        class="h-[450px] p-4 hover:shadow-2xl hover:scale-[1.01] cursor-pointer"
        aria-label="Pie chart section"
      >
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-4 sm:gap-6">
          <section
            v-for="(label, index) in currentFaseData.labels"
            :key="index"
            class="flex flex-col justify-between p-4 border-2 rounded-xl bg-white/5 hover:bg-white/10 cursor-pointer transition-all duration-200"
            :class="getBorderColorByValue(currentFaseData.values[index] ?? 0)"
          >
            <h3
              class="font-mono text-xs font-medium uppercase tracking-widest text-[var(--vt-c-secondary-text)] mb-3"
            >
              {{ label }}
            </h3>
            <p class="font-mono text-3xl font-bold"
              :class="getTextColorByValue(currentFaseData.values[index] ?? 0)"
            >
              {{ currentFaseData.values[index] ?? 0 }}
              <span class="text-xs font-normal text-[var(--vt-c-secondary-text)]"
                >CO₂e/KG</span
              >
            </p>
          </section>
        </div>
      </WidgetCard>
    </div>
  </PageLayout>
</template>
