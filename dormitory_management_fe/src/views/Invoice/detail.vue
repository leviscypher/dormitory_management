<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Chi tiết tài khoản</li>
    </ol>
  </nav>
  <div class="container mt-3 mb-3">
    <div class="form-group">
      <label for="fullName">Họ và Tên</label>
      <p id="fullName">{{ account.fullName }}</p>
    </div>
    <div class="form-group">
      <label for="studentCode">Mã Sinh Viên</label>
      <p id="studentCode">{{ account.studentCode }}</p>
    </div>
    <div class="form-group">
      <label for="roomId">Mã Phòng</label>
      <p id="roomId">{{ account.roomId }}</p>
    </div>
    <div class="form-group">
      <label for="paymentMonth">Khu/Toà nhà</label>
      <p id="paymentMonth">{{ account.paymentMonth }}</p>
    </div>
    <div class="form-group">
      <label for="paymentYear">Nội dung</label>
      <p id="paymentYear">{{ account.paymentYear }}</p>
    </div>
    <div class="form-group">
      <label for="amount">Số Tiền</label>
      <p id="amount">{{ account.amount }}</p>
    </div>
    <div class="form-group">
      <label for="paymentStatus">Trạng Thái Thanh Toán</label>
      <p id="paymentStatus">{{ account.paymentStatus }}</p>
    </div>
    <div class="form-group">
      <label for="paymentDate">Ngày Thanh Toán</label>
      <p id="paymentDate">{{ account.paymentDate }}</p>
    </div>

    <router-link to="/invoice" class="btn btn-secondary">Quay lại</router-link>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useInvoiceStore } from '@/stores/invoice'
import { useRouter, useRoute } from 'vue-router'; 

const invoiceStore = useInvoiceStore()
const router = useRouter(); 
const route = useRoute();
const account = ref({
  fullName: '',
  studentCode: '',
  roomId: '',
  paymentMonth: null,
  paymentYear: null,
  amount: null,
  paymentStatus: '',
  paymentDate: ''
})

onMounted(async () => {
  const accountId = route.params.id
  await invoiceStore.fetchInvoice(accountId)
  Object.assign(account.value, invoiceStore.invoice)
})
</script>

<style scoped></style>
