import type { EcopartDetail } from '@/types/ecopart'
import { useFetch } from './useFetch'

export function useEcopartDetail() {
  const { data: ecopart, loading, error, execute } = useFetch<EcopartDetail>()

  async function fetchEcopartDetail(id: string) {
    await execute(`/api/Ecopart/${id}`)
  }

  return { ecopart, loading, error, fetchEcopartDetail }
}