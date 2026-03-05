import { ref, computed } from 'vue'
import type { Ecopart, ShapeDistribution } from '@/types/ecopart'

export function useShapeDistribution() {
  const ecoparts = ref<Ecopart[]>([])
  const loading = ref(false)
  const error = ref<string | null>(null)

  const distribution = computed<ShapeDistribution | null>(() => {
    if (ecoparts.value.length === 0) return null

    const materialName = ecoparts.value[0]?.material.name ?? ''
    const shapeCounts = new Map<string, number>()

    for (const ecopart of ecoparts.value) {
      const shape = ecopart.shape.shapeType
      shapeCounts.set(shape, (shapeCounts.get(shape) ?? 0) + 1)
    }

    return {
      materialName,
      shapeCounts: Array.from(shapeCounts.entries()).map(([shapeType, count]) => ({
        shapeType,
        count,
      })),
    }
  })

  async function fetchDistribution(materialName: string) {
    loading.value = true
    error.value = null
    try {
      const url = `/api/Ecopart/by-material?materialName=${encodeURIComponent(materialName)}`
      const response = await fetch(url)
      if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`)
      ecoparts.value = await response.json()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Failed to fetch shape distribution'
    } finally {
      loading.value = false
    }
  }

  return { distribution, loading, error, fetchDistribution }
}
