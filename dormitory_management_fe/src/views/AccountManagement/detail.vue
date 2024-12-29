<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Cập nhật tài khoản</li>
    </ol>
  </nav>
  <div class="container mt-5">
    <form @submit.prevent="submitAccount"> <!-- Prevent default form submission -->
      <div class="form-group">
        <label for="username">Tên Đăng Nhập</label>
        <p>{{ account.username }}</p>
      </div>
      <div class="form-group">
        <label for="password">Mật Khẩu</label>
        <p>{{ account.password }}</p>
      </div>
      <div class="form-group">
        <label for="firstName">Họ</label>
        <p>{{ account.firstName }}</p>
      </div>
      <div class="form-group">
        <label for="lastName">Tên</label>
        <p>{{ account.lastName }}</p>
      </div>
      <div class="form-group">
        <label for="email">Email</label>
        <p>{{ account.email }}</p>
      </div>
      <div class="form-group">
        <label for="phone">Số Điện Thoại</label>
        <p>{{ account.phone }}</p>
      </div>
      <a href="/accountmanagement" class="btn btn-secondary">Quay lại</a>
    </form>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import { useAccountStore } from '@/stores/account'; // Import account store
import { useRouter } from 'vue-router'; // Import useRouter
import { useRoute } from 'vue-router'; // Import useRoute for getting route params

export default {
  setup() {
    const accountStore = useAccountStore(); // Initialize account store
    const router = useRouter(); // Initialize router
    const route = useRoute(); // Initialize route to get account ID from URL
    const accountId = route.params.id; // Get the account ID from route parameters

    const account = ref({
      username: '',
      password: '',
      firstName: '',
      lastName: '',
      email: '',
      phone: '',
    });

    // Fetch account details when the component is mounted
    onMounted(async () => {
      await accountStore.fetchAccountById(accountId); // Fetch the account details
      Object.assign(account.value, accountStore.account); // Assign fetched account data to account
    });

    const submitAccount = async () => {
      try {
        await accountStore.updateAccount(accountId, account.value); // Call updateAccount action from accountStore
        router.push({ path: '/accountmanagement' });
        localStorage.setItem('status', 'Cập nhật tài khoản thành công');
      } catch (error) {
        console.error('Error updating account:', error);
        // Handle error feedback (you might want to show a notification to the user)
      }
    };

    return { account, submitAccount };
  },
};
</script>

<style scoped>
/* Add any styles if necessary */
</style>
