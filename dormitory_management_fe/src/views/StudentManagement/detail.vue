<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useStudentStore } from '@/stores/Students'; 
import { useRouter } from 'vue-router'; 

const studentStore = useStudentStore(); 
const router = useRouter(); 
const student = ref({
  studentCode: '',
  fullName: '',
  dateOfBirth: '',
  gender: '',
  address: '',
  phoneNumber: '',
  email: '',
});

const studentId = router.currentRoute.value.params.id; 

const fetchStudent = async () => {
  await studentStore.fetchStudent(studentId); 
  if (studentStore.student) {
    const date = new Date(studentStore.student.dateOfBirth);
    if (!isNaN(date.getTime())) {
      student.value.dateOfBirth = date.toISOString().split('T')[0];
    }
    Object.assign(student.value, studentStore.student); 
  }
};

onMounted(fetchStudent); 


const formatDate = (dateString: string) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, '0'); 
  const day = date.getDate().toString().padStart(2, '0');
  return `${year}-${month}-${day}`;
};


</script>

<template>
  <div>
    <h5 class="mb-3">Thông tin sinh viên</h5>
    <div class="row">
      <div class="col-6">
        <div class="mb-3">
          <label class="form-label">Mã sinh viên</label>
          <div>{{ student.studentCode }}</div>
        </div>
        <div class="mb-3">
          <label class="form-label">Họ tên</label>
          <div>{{ student.fullName }}</div>
        </div>
        <div class="mb-3">
          <label class="form-label">Ngày sinh</label>
          <div>{{ formatDate(student.dateOfBirth) }} </div>
        </div>
      </div>
      <div class="col-6">
        <div class="mb-3">
          <label class="form-label">Giới tính</label>
          <div>{{ student.gender }}</div>
        </div>
        <div class="mb-3">
          <label class="form-label">Địa chỉ</label>
          <div>{{ student.address }}</div>
        </div>
        <div class="mb-3">
          <label class="form-label">Số điện thoại</label>
          <div>{{ student.phoneNumber }}</div>
        </div>
        <div class="mb-3">
          <label class="form-label">Email</label>
          <div>{{ student.email }}</div>
        </div>
      </div>
      <div class="col-12">
        <button class="btn btn-primary" @click="router.push('/studentmanagement')">Quay lại</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Add any styles if necessary */
</style>
