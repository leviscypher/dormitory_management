<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <li class="breadcrumb-item" aria-current="page">Chi tiết cấu hình ngày</li>
    </ol>
  </nav>
  <div class="container mt-5">
    <div class="form-group" v-for="item in fakeData" :key="item.id">
      <label for="username">Ở đến ngày</label>
      <p>{{ item.date }}</p>

      <label for="username">Mã phòng</label>
      <p>{{ item.roomId }}</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useStudentStore } from '@/stores/Students' // Import store
import { useRouter } from 'vue-router' // Import useRouter

// Initialize store và router
const studentStore = useStudentStore()
const router = useRouter()

// Khởi tạo student và fake data
const student = ref({
  studentCode: '',
  fullName: '',
  dateOfBirth: '',
  gender: '',
  address: '',
  phoneNumber: '',
  email: ''
})

const fakeData = ref([{ id: 1, date: '2024-09-01', roomId: 101 }])

// Hàm để thêm sinh viên
const submitStudent = async () => {
  try {
    await studentStore.createStudent(student.value)
    router.push({ path: '/studentmanagement' })
    localStorage.setItem('status', 'Thêm mới thành công')
  } catch (error) {
    console.error('Error creating student:', error)
    // Handle error feedback
  }
}
</script>
<style scoped>
/* Add any styles if necessary */
</style>
