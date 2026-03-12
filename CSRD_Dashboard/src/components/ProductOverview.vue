<script setup lang="ts">
import BackButton from './layout/BackButton.vue'
import PageLayout from './layout/PageLayout.vue'
import WidgetCard from './layout/WidgetCard.vue'
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import { useMaterials } from '@/composables/useMaterials'

const { materials, loading, error, fetchMaterials } = useMaterials()

onMounted(() => {
  fetchMaterials()
})

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
    labels: ['Verbranding', 'Gekochte goederen', 'Onderhoud', 'Transport'],
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
  <PageLayout title="Product Overview">
    <template #actions>
      <BackButton />
    </template>

    <p v-if="loading" class="text-center text-[var(--vt-c-primary-text)] font-mono">
      Loading materials...
    </p>
    <p v-else-if="error" class="text-center text-red-400 font-mono">{{ error }}</p>
    <p
      v-else-if="!materials || materials.length === 0"
      class="text-center text-[#8ABFB8] font-mono"
    >
      No materials found!
    </p>

    <WidgetCard class="px-6 py-8 text-center hover:shadow-xl" aria-label="Introduction section">
      <p class="font-mono text-xs md:text-sm font-semibold text-[var(--vt-c-primary-text)]">
        De Corporate Sustainability Reporting Directive (CSRD) is een Europese richtlijn die
        bedrijven verplicht om uitgebreid te rapporteren over hun impact op milieu, mens en bestuur
        (ESG). Het doel is om meer transparantie te creëren over duurzaamheidsprestaties en
        greenwashing tegen te gaan. Grote bedrijven en later ook beursgenoteerde mkb-bedrijven
        moeten volgens vaste Europese standaarden (ESRS) rapporteren, waarbij de informatie
        bovendien gecontroleerd wordt door een accountant. Zo wil de Europese Unie duurzame
        bedrijfsvoering stimuleren en investeerders beter informeren.
      </p>
    </WidgetCard>

    <WidgetCard>
      <article
        v-for="material in materials"
        :key="material.id"
        class="p-4 border rounded-lg hover:bg-[var(--vt-c-widget-background)] transition-colors"
      >
        <h3 class="text-xl font-mono font-bold text-[var(--vt-c-primary-text)]">
          {{ material.name }}
        </h3>
      </article>
    </WidgetCard>

    <WidgetCard>
      <ChartWrapper :option="pieConfig" @click="handlePieClick" />
    </WidgetCard>
  </PageLayout>
</template>

<style scoped lang="scss"></style>
