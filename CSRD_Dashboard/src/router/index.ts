import DashboardCRSD from '@/components/DashboardCRSD.vue'
import FaseDetails from '@/components/FaseDetails.vue'
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
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
