<template>
  <div>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Thêm mới sinh viên</li>
      </ol>
    </nav>
    <form @submit.prevent="submitStudent">
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
            <input
              type="text"
              class="form-control"
              id="fullName"
              v-model="student.fullName"
            />
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
              <input
                type="radio"
                id="genderMale"
                value="Nam"
                v-model="student.gender"
              />
              <label for="genderMale">Nam</label>
            </div>
            <div>
              <input
                type="radio"
                id="genderFemale"
                value="Nữ"
                v-model="student.gender"
              />
              <label for="genderFemale">Nữ</label>
            </div>
            <div>
              <input
                type="radio"
                id="genderOther"
                value="Khác"
                v-model="student.gender"
              />
              <label for="genderOther">Khác</label>
            </div>
          </div>
          <div class="mb-3">
            <label for="address" class="form-label">Địa chỉ</label>
            <input
              type="text"
              class="form-control"
              id="address"
              v-model="student.address"
            />
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
            <input
              type="text"
              class="form-control"
              id="email"
              v-model="student.email"
            />
          </div>
        </div>
        <div class="col-12">
          <button type="submit" class="btn btn-primary">Tạo Mới</button>
        </div>
      </div>
    </form>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';
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

const submitStudent = async () => {
  try {
    await studentStore.createStudent(student.value);
    router.push({ path: '/studentmanagement' });
   alert('Thêm mới thành công');
  } catch (error) {
    console.error('Error creating student:', error);
  }
};
</script>


<style scoped>
/* Add any styles if necessary */
</style>
