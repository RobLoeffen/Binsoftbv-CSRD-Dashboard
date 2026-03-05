import DashboardCRSD from '@/components/DashboardCRSD.vue'
import EcopartDetail from '@/components/EcopartDetail.vue'
import EcopartsList from '@/components/EcopartsList.vue'
import EcopartShapes from '@/components/EcopartShapes.vue'
import FaseDetails from '@/components/FaseDetails.vue'
import MaterialDetails from '@/components/MaterialDetails.vue'
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: DashboardCRSD,
  },
  {
    path: '/fase/:faseId',
    name: 'FaseDetails',
    component: FaseDetails,
    props: true,
  },
  {
    path: '/materials/:materialId',
    name: 'MaterialDetails',
    component: MaterialDetails,
    props: true,
  },
  {
    path: '/ecoparts/detail/:ecopartId',
    name: 'EcopartDetail',
    component: EcopartDetail,
    props: true,
  },
  {
    path: '/ecoparts',
    name: 'EcopartsList',
    component: EcopartsList,
  },
  {
    path: '/shapes/:materialName',
    name: 'EcopartShapes',
    component: EcopartShapes,
    props: true,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
