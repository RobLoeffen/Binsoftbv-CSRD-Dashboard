import { ref } from 'vue'
import type { Ecopart } from '@/types/ecopart'

export function useEcoparts() {
  const ecoparts = ref<Ecopart[]>([])
  const loading = ref(false)
  const error = ref<string | null>(null)

  async function fetchEcoparts() {
    loading.value = true
    error.value = null
    try {
      const response = await fetch('/api/Ecopart')
      if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`)
      ecoparts.value = await response.json()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Failed to fetch ecoparts'
    } finally {
      loading.value = false
    }
  }

  return { ecoparts, loading, error, fetchEcoparts }
}
