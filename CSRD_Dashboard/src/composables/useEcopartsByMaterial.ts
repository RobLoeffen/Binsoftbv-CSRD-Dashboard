import type { Ecopart } from '@/types/ecopart'
import { useFetch } from './useFetch'

export function useEcopartsByMaterial() {
  const { data: ecoparts, loading, error, execute } = useFetch<Ecopart[]>()

  async function fetchByMaterial(materialName: string, shapeType?: string) {
    loading.value = true
    error.value = null
    let url = `/api/Ecopart/by-material?materialName=${(materialName)}`
    if (shapeType) {
      url += `&shapeType=${(shapeType)}`
    }
    await execute(url)
  }

  return { ecoparts, loading, error, fetchByMaterial }
}
