import type { Material } from '@/types/material'
import { useFetch } from './useFetch'

export function useMaterials() {
  const { data: materials, loading, error, execute } = useFetch<Material[]>()

  async function fetchMaterials() {
    await execute('/api/Material')
  }

  return { materials, loading, error, fetchMaterials }
}
