<script setup lang="ts">
import { onMounted, computed } from 'vue'
import { useStudentStore } from '@/stores/Students'

const successMessage = localStorage.getItem('status') || ''
const studentStore = useStudentStore()

const fetchStudents = async () => {
  await studentStore.fetchStudents()


}
onMounted(async () => {
  await fetchStudents()
})
const students = computed(() => studentStore.students)

const deleteStudent = async (studentId: number) => {
  if (confirm('Bạn có chắc muốn xóa sinh viên này?')) {
    await studentStore.deleteStudent(studentId)
  }
}
const formatDate = (dateString: string): string => {
  const date = new Date(dateString)
  return date.toLocaleDateString('vi-VN')
}
</script>
<template lang="">
  <div>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Quản lý sinh viên</li>
      </ol>
    </nav>

    <h5 class="mb-3">Danh sách sinh viên</h5>
    <div class="mb-3">
      <router-link to="/studentmanagement/create" class="btn btn-primary"
        >Thêm sinh viên</router-link
      >
    </div>
    <div class="input-group search-input">
      <span class="input-group-text" id="search-input">
        <svg
          class="icon-18"
          width="18"
          viewBox="0 0 24 24"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <circle
            cx="11.7669"
            cy="11.7666"
            r="8.98856"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          ></circle>
          <path
            d="M18.0186 18.4851L21.5426 22"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          ></path>
        </svg>
      </span>
      <input type="search" class="form-control" placeholder="Search..." />
    </div>
    <div class="table-responsive">
      <table class="table table-striped">
        <thead>
          <tr>
            <th scope="col">STT</th>
            <th scope="col">Mã cccd</th>
            <th scope="col">Họ Tên</th>
            <th scope="col">Giới tính</th>
            <th scope="col">Số điện thoại</th>
            <th scope="col">Email</th>
            <th scope="col">Tính năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(student, index) in students" :key="student.id">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ student.studentCode }}</td>
            <td>{{ student.fullName }}</td>
            <td>{{ student.gender }}</td>
            <td>{{ student.phoneNumber }}</td>
            <td>{{ student.email }}</td>
            <td class="d-flex flex-column gap-2">
              <router-link
                :to="{ name: 'update', params: { id: student.id } }"
                class="btn btn-primary btn-sm"
              >
                Cập nhật
              </router-link>
              <router-link
                :to="{ name: 'detail', params: { id: student.id } }"
                class="btn btn-primary btn-sm"
              >
                chi tiết
              </router-link>
              <button @click="deleteStudent(student.id)" class="btn btn-danger btn-sm">Xóa</button>
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
