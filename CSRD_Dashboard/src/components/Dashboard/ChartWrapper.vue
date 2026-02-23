<script setup lang="ts">
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
  ArcElement,
} from 'chart.js'
import { Bar, Pie } from 'vue-chartjs'

ChartJS.register(CategoryScale, LinearScale, BarElement, ArcElement, Title, Tooltip, Legend)

interface Props {
  type: 'pie' | 'bar'
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  data: any
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  options: any
}

defineProps<Props>()
</script>

<template>
  <Pie v-if="type === 'pie'" :data="data" :options="options" />
  <Bar v-if="type === 'bar'" :data="data" :options="options" />
</template>
