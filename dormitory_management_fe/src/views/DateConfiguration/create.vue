<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Thêm mới cấu hình ngày</li>
    </ol>
  </nav>
  <div class="container mt-5">
    <form @submit.prevent="submitDateConfig">
      <div class="form-group">
        <label for="date">Chọn ngày</label>
        <input v-model="newDateConfig.date" type="date" class="form-control" id="date" required />
      </div>
      <div class="form-group">
        <label for="roomId">Số phòng</label>
        <input
          v-model="newDateConfig.roomId"
          type="number"
          class="form-control"
          id="roomId"
          required
        />
      </div>
      <button type="submit" class="btn btn-primary me-3">Lưu</button>
      <router-link to="/dateconfiguration" class="btn btn-secondary">Quay lại</router-link>
    </form>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue'
import { useDateConfigStore } from '@/stores/DateConfig'
import { useRouter } from 'vue-router'

const dateConfigStore = useDateConfigStore()
const router = useRouter()

const newDateConfig = ref({
  date: '',
  roomId: null
})

const submitDateConfig = async () => {
  try {
    await dateConfigStore.createDateConfig(newDateConfig.value)
    router.push('/dateconfiguration')
    alert('Thêm mới thành công')

  } catch (error) {
    console.error('Error creating date configuration:', error)
    alert('Thêm mới không thành công')

  }
}
</script>
