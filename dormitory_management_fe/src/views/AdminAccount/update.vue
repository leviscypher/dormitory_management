<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Cập nhập tài khoản admin</li>
    </ol>
  </nav>
  <div class="container mt-5">
    <form @submit.prevent="submitAccount">
      <div class="form-group">
        <label for="username">Tên Đăng Nhập</label>
        <input type="text" class="form-control" id="username" v-model="account.username" required />
      </div>
      <div class="form-group">
        <label for="password">Mật Khẩu</label>
        <input
          type="password"
          class="form-control"
          id="password"
          v-model="account.password"
          required
        />
      </div>
      <div class="form-group">
        <label for="role">Chọn quyền</label>
        <select class="form-select" id="role" v-model="account.role" required>
          <option value="" disabled selected>Chọn phân quyền</option>
          <option value="1">Quản lý</option>
          <option value="2">Kế toán</option>
          <option value="3">Admin</option>
        </select>
      </div>

      <button type="submit" class="btn btn-primary me-3">Lưu</button>
      <a href="/accountmanagement" class="btn btn-secondary">Quay lại</a>
    </form>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useAdminAccountStore } from '@/stores/adminAccount';
import { useRouter, useRoute } from 'vue-router';

const adminAccountStore = useAdminAccountStore();
const router = useRouter();
const route = useRoute();

const account = ref({
  id: 0,
  username: '',
  password: '',
  role: '',
});
onMounted(async () => {
  const accountId = route.params.id; 
  await adminAccountStore.fetchAccountById(accountId); 
  Object.assign(account.value, adminAccountStore.adminAccount); 
});

const submitAccount = async () => {
  try {
    await adminAccountStore.updateAdminAccount(account.value);
    router.push({ path: '/adminaccount' });
    localStorage.setItem('status', 'Cập nhật tài khoản thành công');
  } catch (error) {
    console.error('Error updating account:', error);
    // Handle error feedback (you might want to show a notification to the user)
  }
};
</script>

<style scoped>
/* Add any styles if necessary */
</style>
