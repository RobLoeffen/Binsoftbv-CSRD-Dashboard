import { ref } from 'vue'
import type { EcopartDetail } from '@/types/ecopart'

export function useEcopartDetail() {
  const ecopart = ref<EcopartDetail | null>(null)
  const loading = ref(false)
  const error = ref<string | null>(null)

  async function fetchEcopartDetail(id: string) {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`/api/Ecopart/${id}`)
      if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`)
      ecopart.value = await response.json()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Failed to fetch ecopart detail'
    } finally {
      loading.value = false
    }
  }

  return { ecopart, loading, error, fetchEcopartDetail }
}
