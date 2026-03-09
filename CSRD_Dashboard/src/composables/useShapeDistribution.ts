import { computed } from 'vue'
import type { Ecopart, ShapeDistribution } from '@/types/ecopart'
import { useFetch } from './useFetch'

export function useShapeDistribution() {
  const { data: ecoparts, loading, error, execute } = useFetch<Ecopart[]>()

  const distribution = computed<ShapeDistribution | null>(() => {
    if (!ecoparts.value || ecoparts.value.length === 0) return null

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
    const url = `/api/Ecopart/by-material?materialName=${encodeURIComponent(materialName)}`
    await execute(url)
  }
  return { distribution, loading, error, fetchDistribution }
}
