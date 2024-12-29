<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Cập nhật tài khoản</li>
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
        <input type="password" class="form-control" id="passwordHash" v-model="account.passwordHash" required />
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
      <button type="submit" class="btn btn-primary me-3">Cập nhật</button>
      <a href="/accountmanagement" class="btn btn-secondary">Quay lại</a>
    </form>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useAccountStore } from '@/stores/account'; 
import { useRouter, useRoute } from 'vue-router'; 

const accountStore = useAccountStore(); 
const router = useRouter(); 
const route = useRoute();

const account = ref({
  username: '',
  passwordHash: '',
  firstName: '',
  lastName: '',
  email: '',
  phone: '',
});

onMounted(async () => {
  const accountId = route.params.id; 
  await accountStore.fetchAccountById(accountId); 
  Object.assign(account.value, accountStore.account); 
});

const submitAccount = async () => {
  const accountId = route.params.id; 
  try {
    await accountStore.updateAccount(accountId, account.value); 
    alert("Cập nhật thành công");
    router.push({ path: '/accountmanagement' }); 
  } catch (error) {
    console.error('Error updating account:', error);
    alert("Cập nhật không thành công");
  }
};
</script>
