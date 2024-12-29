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
        path: 'studentmanagement', 
        name: 'studentmanagement',
        component: () => import('@/views/StudentManagement/index.vue')
      },
      {
        path: 'studentmanagement/create', 
        name: 'create',
        component: () => import('@/views//StudentManagement/create.vue')
      },
      {
        path: 'studentmanagement/update/:id', 
        name: 'update',
        component: () => import('@/views/StudentManagement/update.vue')
      },
      {
        path: 'studentmanagement/detail/:id', 
        name: 'detail',
        component: () => import('@/views/StudentManagement/detail.vue')
      },  
      {
        path: 'roommanager', 
        name: 'roommanager',
        component: () => import('@/views/RoomManager/index.vue')
      },
      {
        path: 'roommanager/create', 
        name: 'createRoom',
        component: () => import('@/views/RoomManager/create.vue')
      },
      {
        path: 'roommanager/update/:id', 
        name: 'updateRoom',
        component: () => import('@/views/RoomManager/update.vue')
      },
      {
        path: 'roommanager/detail/:id', 
        name: 'detailRoom',
        component: () => import('@/views/RoomManager/detail.vue')
      },
      {
        path: 'roomreservationmanagement', 
        name: 'roomReservationManagement',
        component: () => import('@/views/RoomReservationManagement/index.vue')
      },
      {
        path: 'roomreservationmanagement/create', 
        name: 'createReservations',
        component: () => import('@/views/RoomReservationManagement/create.vue')
      },
      {
        path: 'roomreservationmanagement/update/:id', 
        name: 'updateReservation',
        component: () => import('@/views/RoomReservationManagement/update.vue')
      },
      {
        path: 'roomreservationmanagement/detail/:id', 
        name: 'detailReservation',
        component: () => import('@/views/RoomReservationManagement/detail.vue')
      },
      {
        path: 'accountmanagement', 
        name: 'accountManagement',
        component: () => import('@/views/AccountManagement/index.vue')
      },
      {
        path: 'accountmanagement/create', 
        name: 'createAccount',
        component: () => import('@/views/AccountManagement/create.vue')
      },
      {
        path: 'accountmanagement/update/:id', 
        name: 'updateAccount',
        component: () => import('@/views/AccountManagement/update.vue')
      },
      {
        path: 'accountmanagement/detail/:id', 
        name: 'detailAccount',
        component: () => import('@/views/AccountManagement/detail.vue')
      },
      {
        path: 'invoice', 
        name: 'invoiceManagement',
        component: () => import('@/views/Invoice/index.vue')
      },
      {
        path: 'invoice/create', 
        name: 'createInvoice',
        component: () => import('@/views/Invoice/create.vue')
      },
      {
        path: 'invoice/update/:id', 
        name: 'updateInvoice',
        component: () => import('@/views/Invoice/update.vue')
      },
      {
        path: 'invoice/detail/:id', 
        name: 'detailInvoice',
        component: () => import('@/views/Invoice/detail.vue')
      },
      {
        path: 'dateconfiguration', 
        name: 'dateConfiguration',
        component: () => import('@/views/DateConfiguration/index.vue')
      },
      {
        path: 'dateconfiguration/create', 
        name: 'createDateConfiguration',
        component: () => import('@/views/DateConfiguration/create.vue')
      },
      {
        path: 'dateconfiguration/update/:id', 
        name: 'updateDateConfiguration',
        component: () => import('@/views/DateConfiguration/update.vue')
      },
      {
        path: 'dateconfiguration/detail/:id', 
        name: 'detailDateConfiguration',
        component: () => import('@/views/DateConfiguration/detail.vue')
      },
      {
        path: 'adminaccount', 
        name: 'adminAccount',
        component: () => import('@/views/AdminAccount/index.vue')
      },
      {
        path: 'adminaccount/create', 
        name: 'adminaccount',
        component: () => import('@/views/AdminAccount/create.vue')
      },
      {
        path: 'adminaccount/update/:id', 
        name: 'updateAdminAccount',
        component: () => import('@/views/AdminAccount/update.vue')
      },
      {
        path: 'adminaccount/detail/:id', 
        name: 'detailAdminAccount',
        component: () => import('@/views/AdminAccount/detail.vue')
      }
      
        
    ]
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router
