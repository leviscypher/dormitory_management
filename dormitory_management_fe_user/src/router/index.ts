import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/Auth/index.vue'
import DefaultLayout from '@/layout/index.vue' 

const routes = [
  {
    path: '/',
    component: LoginView, 
    children: [
      {
        path: '',
        name: 'login',
        component: LoginView
      }
    ]
  },
  {
    path: '/',
    component: DefaultLayout, 
    children: [
      {
        path: 'home',
        name: 'home',
        component: () => import('@/views/Home/index.vue')
      },
      {
        path: 'detail/:id', 
        name: 'detail',
        component: () => import('@/views/Home/detail.vue')
      },
      {
        path: 'bookroom/:id', 
        name: 'bookroom',
        component: () => import('@/views/BookRoom/index.vue')
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router
