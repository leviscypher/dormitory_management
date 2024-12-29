<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useStudentStore } from '@/stores/Students'
import { useRouter } from 'vue-router'

const studentStore = useStudentStore()
const router = useRouter()

const student = ref({
  studentCode: '',
  fullName: '',
  dateOfBirth: '',
  gender: '',
  address: '',
  phoneNumber: '',
  email: ''
})

const studentId = router.currentRoute.value.params.id

const fetchStudent = async () => {
  await studentStore.fetchStudent(studentId)
  if (studentStore.student) {
    const date = new Date(studentStore.student.dateOfBirth)
    if (!isNaN(date.getTime())) {
      student.value.dateOfBirth = formatDate(student.value.dateOfBirth)
      // student.value.dateOfBirth = date.toISOString().split('T')[0]
      
    }
    Object.assign(student.value, studentStore.student)
  }
}

const updateStudent = async () => {
  try {
    await studentStore.editStudent(studentId, student.value)
    router.push({ path: '/studentmanagement' })
    localStorage.setItem('status', 'Cập nhật thành công')
  } catch (error) {
    console.error('Error updating student:', error)
  }
}

onMounted(fetchStudent)

const formatDate = (dateString: string) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = ('0' + (date.getMonth() + 1)).slice(-2);
  const day = ('0' + date.getDate()).slice(-2);
  return `${year}-${month}-${day}`;
};


// return { student, updateStudent, formatDate };
</script>

<template>
  <div>
    <h5 class="mb-3">Cập nhật thông tin sinh viên</h5>
    <form @submit.prevent="updateStudent">
      <div class="row">
        <div class="col-6">
          <div class="mb-3">
            <label for="studentCode" class="form-label">Mã sinh viên</label>
            <input
              type="text"
              class="form-control"
              id="studentCode"
              v-model="student.studentCode"
            />
          </div>
          <div class="mb-3">
            <label for="fullName" class="form-label">Họ tên</label>
            <input type="text" class="form-control" id="fullName" v-model="student.fullName" />
          </div>
          <div class="mb-3">
            <label for="dateOfBirth" class="form-label">Ngày sinh</label>
            <input
              type="date"
              class="form-control"
              id="dateOfBirth"
              v-model="student.dateOfBirth"
            />
          </div>
        </div>
        <div class="col-6">
          <div class="mb-3">
            <label class="form-label">Giới tính</label>
            <div>
              <input type="radio" id="genderMale" value="Nam" v-model="student.gender" />
              <label for="genderMale">Nam</label>
            </div>
            <div>
              <input type="radio" id="genderFemale" value="Nữ" v-model="student.gender" />
              <label for="genderFemale">Nữ</label>
            </div>
            <div>
              <input type="radio" id="genderOther" value="Khác" v-model="student.gender" />
              <label for="genderOther">Khác</label>
            </div>
          </div>
          <div class="mb-3">
            <label for="address" class="form-label">Địa chỉ</label>
            <input type="text" class="form-control" id="address" v-model="student.address" />
          </div>
          <div class="mb-3">
            <label for="phoneNumber" class="form-label">Số điện thoại</label>
            <input
              type="text"
              class="form-control"
              id="phoneNumber"
              v-model="student.phoneNumber"
            />
          </div>
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <input type="email" class="form-control" id="email" v-model="student.email" />
          </div>
        </div>
        <div class="w-25">
          <div class="d-flex gap-3">
            <div class="col-6 col-lg-6">
              <button class="btn btn-danger" @click="router.push('/studentmanagement')">
                Quay lại
              </button>
            </div>
            <div class="col-6  col-lg-6">
              <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<style scoped>
/* Add any styles if necessary */
</style>
