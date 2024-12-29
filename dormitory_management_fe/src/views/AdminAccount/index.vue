<script setup lang="ts">
import { onMounted, computed } from 'vue';
import { useAdminAccountStore } from '@/stores/adminAccount'; // Import your account store

const adminAccountStore = useAdminAccountStore(); // Use the account store

// Fetch accounts when the component mounts
const fetchAccounts = async () => {
  await adminAccountStore.fetchAdminAccounts();
};

onMounted(async () => {
  await fetchAccounts();
});

// Use accounts from the account store
const accounts = computed(() => adminAccountStore.adminAccounts);

// Function to delete an account
const deleteAccount = async (accountId: number) => {
  if (confirm('Bạn có chắc muốn xóa tài khoản này?')) {
    await adminAccountStore.deleteAdminAccount(accountId);
  }
};
const getRoomStatusText = (statusCode: string) => {
  switch (statusCode) {
    case '1':
      return 'Quản lý'
    case '2':
      return 'Kế toán'
    case '3':
      return 'Admin'
    default:
      return 'Không xác định'
  }
}
</script>

<template>
  <div>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Quản lý tài khoản admin</li>
      </ol>
    </nav>

    <h5 class="mb-3">Danh sách tài khoản admin</h5>
    <div class="mb-3">
      <router-link to="/adminaccount/create" class="btn btn-primary">
        Thêm tài khoản
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
            <th>Tên Đăng Nhập</th>
            <th>Quyền</th>
            <th scope="col">Tính năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(account, index) in accounts" :key="account.id">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ account.username }}</td>
            <td>{{ getRoomStatusText(account.role) }}</td>
            <td class="d-flex gap-3">
              <router-link
                :to="{ name: 'updateAdminAccount', params: { id: account.id } }"
                class="btn btn-primary btn-sm"
              >
                Cập nhật
              </router-link>
              <router-link
                :to="{ name: 'detailAdminAccount', params: { id: account.id } }"
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
