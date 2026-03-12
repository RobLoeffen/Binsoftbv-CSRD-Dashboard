import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/components/DashboardCRSD.vue'),
  },
  {
    path: '/fase/:faseId',
    name: 'FaseDetails',
    component: () => import('@/components/FaseDetails.vue'),
    props: true,
  },
  {
    path: '/product',
    name: 'ProductOverview',
    component: () => import('@/components/ProductOverview.vue'),
    props: true,
  },
  {
    path: '/materials/:materialId',
    name: 'MaterialDetails',
    component: () => import('@/components/MaterialDetails.vue'),
    props: true,
  },
  {
    path: '/ecoparts/detail/:ecopartId',
    name: 'EcopartDetail',
    component: () => import('@/components/EcopartDetail.vue'),
    props: true,
  },
  {
    path: '/ecoparts',
    name: 'EcopartsList',
    component: () => import('@/components/EcopartsList.vue'),
  },
  {
    path: '/shapes/:materialName',
    name: 'EcopartShapes',
    component: () => import('@/components/EcopartShapes.vue'),
    props: true,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
