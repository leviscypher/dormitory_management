<script setup lang="ts">
import { onMounted, computed } from 'vue';
import { useInvoiceStore } from '@/stores/invoice';

const invoiceStore = useInvoiceStore();


const fetchInvoices = async () => {
  await invoiceStore.fetchInvoices();
};

onMounted(async () => {
  await fetchInvoices();
});

const invoices = computed(() => invoiceStore.invoices);

const deleteAccount = async (accountId: number) => {
  if (confirm('Bạn có chắc muốn xóa tài khoản này?')) {
    await invoiceStore.deleteInvoice(accountId);
  }
};
const getRoomStatusText = (statusCode: string) => {
  switch (statusCode) {
    case '1':
      return 'Đã đóng'
    case '2':
      return 'Chưa đóng'
    default:
      return 'Không xác định'
  }
}
const formatCurrency = (amount: number): string => {
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND'
  }).format(amount)
}
</script>


<template lang="">
  <div>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Quản lý hoá đơn</li>
      </ol>
    </nav>

    <h5 class="mb-3">Quản lý hoá đơn</h5>
    <div class="mb-3">
      <router-link to="/invoice/create" class="btn btn-primary">
        Thêm hoá đơn
      </router-link>
    </div>
    <div class="input-group search-input">
      <span class="input-group-text" id="search-input">
        <svg
          class="icon-18"
          width="18"
          viewBox="0 0 24 24"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <circle
            cx="11.7669"
            cy="11.7666"
            r="8.98856"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          ></circle>
          <path
            d="M18.0186 18.4851L21.5426 22"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          ></path>
        </svg>
      </span>
      <input type="search" class="form-control" placeholder="Search..." />
    </div>
    <div class="table-responsive">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>STT</th>
            <th>Họ và tên</th>
            <th>Cccd</th>
            <th>Nội dung</th>
            <th>Số tiền</th>
            <th>Trạng thái thanh toán</th>
            <th scope="col">Tính năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(account, index) in invoices" :key="account.id">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ account.fullName }}</td>
          
            <td>{{ account.studentCode }}</td>
            <td>{{ account.paymentMonth }}</td>
            <td>{{ formatCurrency(account.amount) }}</td>
            <td>{{getRoomStatusText(account.paymentStatus)}}</td>
            <td class="d-flex gap-3">
              <router-link
                :to="{ name: 'updateInvoice', params: { id: account.id } }"
                class="btn btn-primary btn-sm"
              >
                Cập nhật
              </router-link>
              <router-link
                :to="{ name: 'detailInvoice', params: { id: account.id } }"
                class="btn btn-primary btn-sm"
              >
                Chi tiết
              </router-link>
              <button @click="deleteAccount(account.id)" class="btn btn-danger btn-sm">Xóa</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
.table {
  margin-top: 20px;
}
</style>
