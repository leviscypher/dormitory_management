<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Cập nhật tài khoản</li>
    </ol>
  </nav>
  <div class="container mt-3 mb-3">
    <form @submit.prevent="submitAccount">
      <div class="form-group">
        <label for="fullName">Họ và Tên</label>
        <input type="text" class="form-control" id="fullName" v-model="account.fullName" required />
      </div>
      <div class="form-group">
        <label for="studentCode">Mã Sinh Viên</label>
        <input
          type="text"
          class="form-control"
          id="studentCode"
          v-model="account.studentCode"
          required
        />
      </div>
      <div class="form-group">
        <label for="roomId">Mã Phòng</label>
        <input type="text" class="form-control" id="roomId" v-model="account.roomId" required />
      </div>
      <div class="form-group">
        <label for="paymentMonth">Khu/Toà nhà</label>
        <input
          type="text"
          class="form-control"
          id="paymentMonth"
          v-model="account.paymentMonth"
          required
        />
      </div>
      <div class="form-group">
        <label for="paymentYear">Nội dung</label>
        <input
          type="text"
          class="form-control"
          id="paymentYear"
          v-model="account.paymentYear"
          required
        />
      </div>
      <div class="form-group">
        <label for="amount">Số Tiền</label>
        <input type="number" class="form-control" id="amount" v-model="account.amount" required />
      </div>
      <div class="form-group">
        <label for="paymentStatus">Trạng Thái Thanh Toán</label>
        <div>
          <input type="radio" id="roomStatus" value="2" v-model="account.paymentStatus" />
          <label for="roomStatus">Đã thanh toán</label>
        </div>
        <div>
          <input type="radio" id="roomStatus1" value="1" v-model="account.paymentStatus" />
          <label for="roomStatus1">Chưa thanh toán</label>
        </div>
      </div>
      <div class="form-group">
        <label for="paymentDate">Ngày Thanh Toán</label>
        <input
          type="date"
          class="form-control"
          id="paymentDate"
          v-model="account.paymentDate"
          required
        />
      </div>

      <button type="submit" class="btn btn-primary me-3">Lưu</button>
      <router-link to="/invoice" class="btn btn-secondary">Quay lại</router-link>
    </form>
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

const submitAccount = async () => {
  const accountId = route.params.id
  try {
    await invoiceStore.updateInvoice(accountId, account.value)
    alert('Cập nhật thành công')
    router.push({ path: '/invoice' })
  } catch (error) {
    console.error('Error updating account:', error)
    alert('Cập nhật không thành công')
  }
}
</script>

<style scoped></style>
