<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Thêm mới tài khoản</li>
    </ol>
  </nav>
  <div class="container mt-3">
    <form @submit.prevent="submitAccount"> <!-- Prevent default form submission -->
      <div class="form-group">
        <label for="username">Tên Đăng Nhập</label>
        <input type="text" class="form-control" id="username" v-model="account.username" required />
      </div>
      <div class="form-group">
        <label for="password">Mật Khẩu</label>
        <input type="password" class="form-control" id="password" v-model="account.passwordHash" required />
      </div>
      <div class="form-group">
        <label for="firstName">Họ</label>
        <input type="text" class="form-control" id="firstName" v-model="account.firstName" required />
      </div>
      <div class="form-group">
        <label for="lastName">Tên</label>
        <input type="text" class="form-control" id="lastName" v-model="account.lastName" required />
      </div>
      <div class="form-group">
        <label for="email">Email</label>
        <input type="email" class="form-control" id="email" v-model="account.email" required />
      </div>
      <div class="form-group">
        <label for="phone">Số Điện Thoại</label>
        <input type="text" class="form-control" id="phone" v-model="account.phone" required />
      </div>
      <button type="submit" class="btn btn-primary me-3">Lưu</button>
      <router-link to="/accountmanagement" class="btn btn-secondary">Quay lại</router-link>
    </form>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import { useAccountStore } from '@/stores/account'; 
import { useRouter } from 'vue-router'; 

const accountStore = useAccountStore(); 
const router = useRouter(); 
const account = ref({
  username: '',
  passwordHash: '',
  firstName: '',
  lastName: '',
  email: '',
  phone: '',
});

const submitAccount = async () => {
  try {
    await accountStore.createAccount(account.value);
    alert("Thêm mới thành công")
    router.push({ path: '/accountmanagement' });
  } catch (error) {
    console.error('Error creating account:', error);
    alert("Thêm mới không thành công")
  }
};
</script>

<style scoped>
/* Add any styles if necessary */
</style>
