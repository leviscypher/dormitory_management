<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Cập nhập cấu hình ngày</li>
    </ol>
  </nav>
  <div class="container mt-5">
    <form @submit.prevent="submitDateConfig">
      <div class="form-group">
        <label for="date">Chọn ngày</label>
        <input
          type="date"
          class="form-control"
          v-model="formattedDate"
          id="date"
          required
        />
      </div>
      <div class="form-group">
        <label for="roomId">Số phòng</label>
        <input
          type="text"
          class="form-control"
          v-model="dateConfig.roomId"
          id="roomId"
          required
        />
      </div>
      <button type="submit" class="btn btn-primary me-3">Lưu</button>
      <router-link to="/dateconfiguration" class="btn btn-secondary">
        Quay lại
      </router-link>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useDateConfigStore } from '@/stores/DateConfig';
import { useRouter, useRoute } from 'vue-router';

const dateConfigStore = useDateConfigStore();
const router = useRouter();
const route = useRoute();

const dateConfig = ref({
  date: '',
  roomId: null
});

const formatDate = (dateString: string) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = ('0' + (date.getMonth() + 1)).slice(-2);
  const day = ('0' + date.getDate()).slice(-2);
  return `${year}-${month}-${day}`;
};

const formattedDate = computed({
  get() {
    return dateConfig.value.date ? formatDate(dateConfig.value.date) : '';
  },
  set(value: string) {
    dateConfig.value.date = value;
  }
});

const fetchDateConfig = async () => {
  const id = route.params.id;
  try {
    await dateConfigStore.fetchDateConfigById(id);
    dateConfig.value = dateConfigStore.dateConfig;
  } catch (error) {
    console.error('Error fetching date configuration:', error);
  }
};

onMounted(fetchDateConfig);

const submitDateConfig = async () => {
  const id = route.params.id;
  try {
    await dateConfigStore.updateDateConfig(id, dateConfig.value);
    router.push('/dateconfiguration');
    alert('Cập nhập thành công');
  } catch (error) {
    console.error('Error updating date configuration:', error);
    alert('Cập nhập không thành công');
  }
};
</script>
