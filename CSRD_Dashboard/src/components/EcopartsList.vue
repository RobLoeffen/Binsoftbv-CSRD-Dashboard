<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useEcopartsByMaterial } from '@/composables/useEcopartsByMaterial'
import type { Ecopart } from '@/types/ecopart'
import PageLayout from './layout/PageLayout.vue'
import WidgetCard from './layout/WidgetCard.vue'
import BackButton from './layout/BackButton.vue'

const route = useRoute()
const router = useRouter()

const material = computed(() => route.query.material as string | undefined)
const shape = computed(() => route.query.shape as string | undefined)

const { ecoparts, loading, error, fetchByMaterial } = useEcopartsByMaterial()

onMounted(() => {
  if (material.value) {
    fetchByMaterial(material.value, shape.value)
  }
})

const handleEcopartClick = (ecopart: Ecopart) => {
  router.push(`/ecoparts/detail/${ecopart.id}`)
}
</script>

<template>
  <PageLayout>
    <template #title>
      Ecoparts
      <span v-if="material" class="text-purple-300">{{ material }}</span>
      <span v-if="shape" class="text-blue-300"> · {{ shape }}</span>
    </template>
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
      <p v-if="loading" class="col-span-full text-center text-[var(--vt-c-primary-text)] font-mono">
        Loading ecoparts...
      </p>
      <p v-else-if="error" class="col-span-full text-center text-red-400 font-mono">
        {{ error }}
      </p>
      <p
        v-else-if="ecoparts.length === 0"
        class="col-span-full text-center text-[#8ABFB8] font-mono"
      >
        No ecoparts found.
      </p>
      <template v-else>
        <article
          v-for="ecopart in ecoparts"
          :key="ecopart.id"
          @click="handleEcopartClick(ecopart)"
          class="rounded-2xl bg-[var(--vt-c-widget-background)] p-6 shadow-lg border border-white/5 hover:border-white/20 transition-all duration-300 hover:shadow-2xl hover:scale-[1.02] cursor-pointer group relative overflow-hidden"
          :aria-label="`Ecopart: ${ecopart.name}`"
        >
          <div class="relative z-10 flex flex-col gap-3">
            <h3 class="text-xl font-mono font-bold text-[var(--vt-c-primary-text)]">
              {{ ecopart.name }}
            </h3>
            <div class="flex gap-2 flex-wrap">
              <span class="text-xs font-mono text-purple-300 bg-purple-500/20 px-2 py-1 rounded">
                {{ ecopart.material.name }}
              </span>
              <span class="text-xs font-mono text-blue-300 bg-blue-500/20 px-2 py-1 rounded">
                {{ ecopart.shape.shapeType }}
              </span>
            </div>
            <p class="text-sm text-[#8ABFB8] font-mono">
              Dichtheid: {{ ecopart.material.density }} g/cm³
            </p>
            <div class="mt-2 pt-3 border-t border-white/10">
              <span class="text-xs text-[#8ABFB8] font-mono">Klik voor meer details →</span>
            </div>
          </div>
        </article>
      </template>
    </div>
  </PageLayout>
</template>
