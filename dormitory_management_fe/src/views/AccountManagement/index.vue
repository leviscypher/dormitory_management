<script setup lang="ts">
import { onMounted, computed } from 'vue'
import { useAccountStore } from '@/stores/account'

const accountStore = useAccountStore()

const fetchAccounts = async () => {
  await accountStore.fetchAccounts()
}

onMounted(async () => {
  await fetchAccounts()
})

const accounts = computed(() => accountStore.accounts)

const deleteAccount = async (accountId: number) => {
  if (confirm('Bạn có chắc muốn xóa tài khoản này?')) {
    await accountStore.deleteAccount(accountId)
  }
}
</script>

<template lang="">
  <div>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Quản lý tài khoản</li>
      </ol>
    </nav>

    <h5 class="mb-3">Quản lý tài khoản</h5>
    <div class="mb-3">
      <router-link to="/accountmanagement/create" class="btn btn-primary">
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
            <th>Họ Tên</th>
            <th>Email</th>
            <th>Số Điện Thoại</th>
            <th scope="col">Tính năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(account, index) in accounts" :key="account.id">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ account.username }}</td>
            <td>{{ account.firstName }} {{ account.lastName }}</td>
            <td>{{ account.email }}</td>
            <td>{{ account.phone }}</td>
            <td class="d-flex gap-3">
              <router-link
                :to="{ name: 'updateAccount', params: { id: account.id } }"
                class="btn btn-primary btn-sm"
              >
                Cập nhật
              </router-link>
              <router-link
                :to="{ name: 'detailAccount', params: { id: account.id } }"
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
