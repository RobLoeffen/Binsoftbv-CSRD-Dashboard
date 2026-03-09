import type { Ecopart } from '@/types/ecopart'
import { useFetch } from './useFetch'

export function useEcoparts() {
  const { data: ecoparts, loading, error, execute } = useFetch<Ecopart[]>()

  async function fetchEcoparts() {
    await execute('/api/Ecopart')
  }

  return { ecoparts, loading, error, fetchEcoparts }
}
