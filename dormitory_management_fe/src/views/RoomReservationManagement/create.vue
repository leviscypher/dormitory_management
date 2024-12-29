<script setup lang="ts">
import { useRouter } from 'vue-router'
import { ref } from 'vue'
import { useRoomRegistrationStore } from '@/stores/roomRegistration'

const router = useRouter()

const imageSrc = ref<string | null>(null)
const imageBefore = ref<string | null>(null)
const imageAfter = ref<string | null>(null)

const roomRegistration = ref({
  studentCode: '',
  fullName: '',
  dateOfBirth: '',
  gender: '',
  address: '',
  phoneNumber: '',
  email: '',
  idPhoto: null as File | null,
  beforeImage: null as File | null,
  afterImage: null as File | null
})

const onFileSelected = (event: Event) => {
  const input = event.target as HTMLInputElement
  const file = input.files ? input.files[0] : null
  if (file) {
    const reader = new FileReader()
    reader.onload = (e) => {
      imageSrc.value = e.target?.result as string
    }
    reader.readAsDataURL(file)
    roomRegistration.value.idPhoto = file 
  }
}

const onFileBefore = (event: Event) => {
  const input = event.target as HTMLInputElement
  const file = input.files ? input.files[0] : null
  if (file) {
    const reader = new FileReader()
    reader.onload = (e) => {
      imageBefore.value = e.target?.result as string
    }
    reader.readAsDataURL(file)
    roomRegistration.value.beforeImage = file 
  }
}

const onFileAfter = (event: Event) => {
  const input = event.target as HTMLInputElement
  const file = input.files ? input.files[0] : null
  if (file) {
    const reader = new FileReader()
    reader.onload = (e) => {
      imageAfter.value = e.target?.result as string
    }
    reader.readAsDataURL(file)
    roomRegistration.value.afterImage = file 
  }
}

const submitStudent = async () => {
  const formData = new FormData()
  for (const key in roomRegistration.value) {
    formData.append(key, roomRegistration.value[key])
  }

  try {
    const response = await roomRegistrationStore.createStudent(formData)
    if (response.status === 201) {
      router.push({ path: '/studentmanagement' })
      localStorage.setItem('status', 'Thêm mới thành công')
    }
  } catch (error) {
    console.error('Error creating student:', error)
  }
}

const goBack = () => {
  router.back()
}
</script>
<template>
  <div class="container main">
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Thêm mới đăng ký</li>
      </ol>
    </nav>
    <div class="row">
      <div class="col-12 col-lg-6 col-xl-6">
        <div class="card">
          <h5 class="card-header border-bottom bg-light">Thông tin sinh viên</h5>
          <div class="card-body">
            <form>
              <div class="mb-3">
                <label for="cccd" class="form-label">CMNND/CCCD(*)</label>
                <input type="text" class="form-control" id="cccd" />
              </div>
              <div class="mb-3">
                <label for="date" class="form-label">Ngày CMNND/CCCD(*)</label>
                <input type="date" class="form-control" id="date" />
              </div>
              <div class="mb-3">
                <label for="place__issue" class="form-label">Nơi cấp CMNND/CCCD(*)</label>
                <input type="text" class="form-control" id="place__issue" />
              </div>
              <div class="mb-3">
                <label for="fullname" class="form-label">Email</label>
                <input type="text" class="form-control" id="fullname" />
              </div>
              <div class="mb-3">
                <label for="fullname" class="form-label">Họ và Tên</label>
                <input type="text" class="form-control" id="fullname" />
              </div>
              <div class="mb-3">
                <label for="gender" class="form-label">Giới tính</label>
                <select class="form-select form-select-sm" aria-label=".form-select-sm example">
                  <option selected>Chọn giới tính</option>
                  <option value="1">Nam</option>
                </select>
              </div>
              <div class="mb-3">
                <label for="cccd" class="form-label">Thời gian ở</label>
                <div class="form-check">
                  <input
                    class="form-check-input"
                    type="radio"
                    name="flexRadioDefault"
                    id="flexRadioDefault1"
                  />
                  <label class="form-check-label" for="flexRadioDefault1">
                    Đến ngày 28/10/2024
                  </label>
                </div>
                <div class="form-check">
                  <input
                    class="form-check-input"
                    type="radio"
                    name="flexRadioDefault"
                    id="flexRadioDefault2"
                  />
                  <label class="form-check-label" for="flexRadioDefault2">
                    Đến ngày 28/10/2024
                  </label>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div class="col-12 col-lg-6 col-xl-6">
        <div class="card">
          <h5 class="card-header border-bottom bg-light">Ảnh thẻ 4x6</h5>
          <div class="card-body">
            <div class="bd-example">
              <div class="list-group">
                <div>
                  <input
                    class="form-control"
                    type="file"
                    @change="onFileSelected"
                    accept="image/*"
                    id="formFile"
                  />
                  <div v-if="imageSrc" class="mt-2">
                    <img :src="imageSrc" alt="Selected Image" width="150" height="200" />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="card">
          <h5 class="card-header border-bottom bg-light">Ảnh căn cước công dân</h5>
          <div class="card-body">
            <div class="bd-example">
              <div class="list-group">
                <div class="">
                  <div>
                    <label for="formFile" class="form-label">Mặt trước</label>
                    <input
                      class="form-control"
                      type="file"
                      @change="onFileBefore"
                      accept="image/*"
                      id="formFile"
                    />
                    <div v-if="imageBefore" class="mt-2">
                      <img :src="imageBefore" alt="Selected Image" class="w-100 cccd" />
                    </div>
                  </div>
                  <div>
                    <label for="formFile" class="form-label">Mặt trước</label>
                    <input
                      class="form-control"
                      type="file"
                      @change="onFileAfter"
                      accept="image/*"
                      id="formFile"
                    />
                    <div v-if="imageAfter" class="mt-2">
                      <img :src="imageAfter" alt="Selected Image" class="w-100 cccd" />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12 col-lg-6 col-xl-6">
        <button type="submit" class="btn btn-primary" @click="goBack">Quay lại</button>
      </div>
      <div class="col-12 col-lg-6 col-xl-6">
        <router-link to="/bookroom/1" type="submit" class="btn btn-primary">Tạo mới</router-link>
      </div>
    </div>
  </div>
</template>
<style lang="css" scoped>
.main {
  padding: 15px 20px;
}
.custom-multiselect {
  background-color: white !important;
  height: 32px;
  border: 1px solid #00000045;
  border-radius: 5px;
  line-height: 30px;
  padding-left: 9px;
}
.custom-multiselect .p-multiselect-overlay {
  padding-left: 9px;
  background: #fff !important;
  border: 1px solid #00000042 !important;
  border-radius: 6px;
}
.cccd {
  width: 100%;
  height: 200px;
  object-fit: contain;
}
</style>
