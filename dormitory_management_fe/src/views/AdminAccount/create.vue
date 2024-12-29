<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Thêm mới tài khoản admin</li>
    </ol>
  </nav>
  <div class="container mt-5">
    <form @submit.prevent="submitAccount">
      <div class="form-group">
        <label for="username">Tên Đăng Nhập</label>
        <input
          type="text"
          class="form-control"
          id="username"
          v-model="account.username"
          required
        />
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
      <a href="/adminaccount" class="btn btn-secondary">Quay lại</a>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useAdminAccountStore } from '@/stores/adminAccount';
import { useRouter } from 'vue-router';

const adminAccountStore = useAdminAccountStore();
const router = useRouter();

const account = ref({
  username: '',
  password: '',
  role: '',
});

const submitAccount = async () => {
  try {
    await adminAccountStore.addAdminAccount(account.value);
    router.push({ path: '/adminaccount' });
    localStorage.setItem('status', 'Thêm mới tài khoản thành công');
  } catch (error) {
    console.error('Error creating account:', error);
  }
};
</script>

<style scoped>
</style>
