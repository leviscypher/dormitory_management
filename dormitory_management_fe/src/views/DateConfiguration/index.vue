<script setup lang="ts">
import { onMounted, computed, ref } from 'vue'
import { useDateConfigStore } from '@/stores/DateConfig' // Use the correct store

const dateConfigStore = useDateConfigStore() // Use the date config store

// Fetch date configurations
const fetchDateConfigs = async () => {
  await dateConfigStore.fetchDateConfigs()
}

onMounted(async () => {
  await fetchDateConfigs()
})

const dateConfigs = computed(() => dateConfigStore.dateConfigs)

const deleteDateConfig = async (id: number) => {
  if (confirm('Bạn có chắc muốn xóa cấu hình ngày này?')) {
    await dateConfigStore.deleteDateConfig(id)
  }
}
const formatDate = (dateString: string): string => {
  const date = new Date(dateString)
  return date.toLocaleDateString('vi-VN')
}
</script>

<template>
  <div>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Cấu hình ngày</li>
      </ol>
    </nav>

    <h5 class="mb-3">Danh sách cấu hình ngày</h5>
    <div class="mb-3">
      <router-link to="/dateconfiguration/create" class="btn btn-primary">
        Thêm cấu hình
      </router-link>
    </div>

    <div class="table-responsive">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>STT</th>
            <th>Ngày</th>
            <th>Phòng</th>
            <th>Tính năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(config, index) in dateConfigs" :key="config.id">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ formatDate(config.date) }}</td>
            <td>{{ config.roomId }}</td>
            <td class="d-flex gap-3">
              <router-link
                :to="{ name: 'updateDateConfiguration', params: { id: config.id } }"
                class="btn btn-primary btn-sm"
              >
                Cập nhật
              </router-link>
              <router-link
                :to="{ name: 'detailDateConfiguration', params: { id: config.id } }"
                class="btn btn-primary btn-sm"
              >
                Chi tiết
              </router-link>
              <button @click="deleteDateConfig(config.id)" class="btn btn-danger btn-sm">
                Xóa
              </button>
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
