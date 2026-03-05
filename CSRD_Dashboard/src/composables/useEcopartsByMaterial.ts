import { ref } from 'vue'
import type { Ecopart } from '@/types/ecopart'

export function useEcopartsByMaterial() {
  const ecoparts = ref<Ecopart[]>([])
  const loading = ref(false)
  const error = ref<string | null>(null)

  async function fetchByMaterial(materialName: string, shapeType?: string) {
    loading.value = true
    error.value = null
    try {
      let url = `/api/Ecopart/by-material?materialName=${encodeURIComponent(materialName)}`
      if (shapeType) {
        url += `&shapeType=${encodeURIComponent(shapeType)}`
      }
      const response = await fetch(url)
      if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`)
      ecoparts.value = await response.json()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Failed to fetch ecoparts'
    } finally {
      loading.value = false
    }
  }

  return { ecoparts, loading, error, fetchByMaterial }
}
