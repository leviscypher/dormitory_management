<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Cập nhật tài khoản admin</li>
    </ol>
  </nav>
  <div class="container mt-5">
    <form @submit.prevent="submitAccount">
      <div class="form-group">
        <label for="username">Tên Đăng Nhập</label>
        <p  id="username">{{ account.username }}</p>
      </div>
      <div class="form-group">
        <label for="password">Mật Khẩu</label>
        <p  id="password">{{ account.password }}</p>
      </div>
      <div class="form-group">
        <label for="role">Quyền</label>
        <p  id="role">{{ getRoleName(account.role) }}</p>
      </div>

      <button type="submit" class="btn btn-primary me-3">Lưu</button>
      <router-link to="/adminaccount" class="btn btn-secondary">Quay lại</router-link>
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

const getRoleName = (role) => {
  switch (role) {
    case '1': return 'Quản lý';
    case '2': return 'Kế toán';
    case '3': return 'Admin';
    default: return 'Không xác định';
  }
};

const submitAccount = async () => {
  try {
    await adminAccountStore.updateAdminAccount(account.value);
    router.push({ path: '/adminaccount' });
    localStorage.setItem('status', 'Cập nhật tài khoản thành công');
  } catch (error) {
    console.error('Error updating account:', error);
  }
};
</script>

<style scoped>
</style>
