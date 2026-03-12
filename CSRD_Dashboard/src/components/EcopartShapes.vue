<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import ChartWrapper from './Dashboard/ChartWrapper.vue'
import { createPieChartConfig } from './Dashboard/pieChartConfig'
import { useShapeDistribution } from '@/composables/useShapeDistribution'
import PageLayout from './layout/PageLayout.vue'
import WidgetCard from './layout/WidgetCard.vue'
import BackButton from './layout/BackButton.vue'

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
</script>

<template>
  <PageLayout :title="`Shapes – ${materialName}`">
    <template #actions>
      <BackButton />
    </template>

    <p v-if="loading" class="text-center text-[var(--vt-c-primary-text)] font-mono">
      Loading shape distribution...
    </p>
    <p v-else-if="error" class="text-center text-red-400 font-mono">{{ error }}</p>
    <p
      v-else-if="!distribution || distribution.shapeCounts.length === 0"
      class="text-center text-[#8ABFB8] font-mono"
    >
      No shape data found for {{ materialName }}.
    </p>
    <WidgetCard
      v-else-if="chartConfig"
      class="h-[50vh] p-4 hover:shadow-2xl"
      aria-label="Shape distribution pie chart"
    >
      <ChartWrapper :option="chartConfig" @click="handlePieClick" />
    </WidgetCard>
  </PageLayout>
</template>
