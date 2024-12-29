<script setup lang="ts">
import { ref } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { onMounted, computed } from 'vue'
import { useDormitoryStore } from '@/stores/dormitory'

const router = useRouter()

const goBack = () => {
  router.back()
}

const imageSrc = ref(null)
const imageBefore = ref(null)
const imageAfter = ref(null)
const registration = ref({
  Card: '',
  CardDate: null,
  AddressCard: '',
  Email: '',
  FullName: '',
  Gender: '',
  TimeIn: new Date().toISOString().slice(0, 16), 
  IDPhoto: '',
  Before: '',
  After: '',
  RoomId: ''
})
const onFileSelected = (event) => {
  const file = event.target.files[0]
  if (file) {
    const reader = new FileReader()
    reader.onload = (e) => {
      imageSrc.value = e.target.result
    }
    reader.readAsDataURL(file)
  }
}
const onFileBefore = (event) => {
  const file = event.target.files[0]
  if (file) {
    const reader = new FileReader()
    reader.onload = (e) => {
      imageBefore.value = e.target.result
    }
    reader.readAsDataURL(file)
  }
}
const onFileAfter = (event) => {
  const file = event.target.files[0]
  if (file) {
    const reader = new FileReader()
    reader.onload = (e) => {
      imageAfter.value = e.target.result
    }
    reader.readAsDataURL(file)
  }
}

const roomStore = useDormitoryStore()
const route = useRoute()
const roomId = route.params.id
const fetchRoomDetail = async () => {
  await roomStore.fetchRoomDetail(roomId)
}
const fetchGetDate = async () => {
  await roomStore.fetchGetDate(roomId)
}
const fetchGenders = async () => {
  await roomStore.fetchGenders()
}
onMounted(() => {
  fetchRoomDetail()
  fetchGetDate()
  fetchGenders()
})
const data = computed(() => roomStore.roomDetail || {})
const date = computed(() => roomStore.date || {})
const genders = computed(() => roomStore.genders || {})

const getRoomType = (roomTypeId: number) => {
  switch (roomTypeId) {
    case 3:
      return 'Phòng 8 người'
    case 4:
      return 'Phòng 6 người'
    default:
      return 'Loại phòng không xác định'
  }
}
const formatCurrency = (amount: number): string => {
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND'
  }).format(amount)
}
const formatDate = (dateString: string): string => {
  const date = new Date(dateString)
  return date.toLocaleDateString('vi-VN')
}

const submitRegistration = async () => {
  try {
    const registrationData = {
    ...registration.value,
    IDPhoto: imageSrc.value,
    Before: imageBefore.value,
    After: imageAfter.value,
  }

    const result = await roomStore.addRoomRegistration(registrationData)
   
    console.log('Room registration result:', result)
  } catch (error) {
    console.error('Error:', error)
  }
}
</script>
<template>
  <div class="container main">
    <div class="row">
      <div class="col-12 col-lg-6 col-xl-6">
        <div class="card">
          <h5 class="card-header border-bottom bg-light">Thông tin phòng</h5>
          <div class="card-body">
            <div class="bd-example">
              <div class="list-group">
                <div class="bd-example">
                  <ul class="list-group">
                    <li class="list-group-item">
                      <strong class="text-start">Toà:</strong> {{ data.areaId }}
                    </li>
                    <li class="list-group-item">
                      <strong class="text-start">Tầng:</strong> {{ data.floorId }}
                    </li>
                    <li class="list-group-item">
                      <strong class="text-start">Phòng:</strong> {{ data.roomCode }}
                    </li>
                    <li class="list-group-item">
                      <strong class="text-start">Loại phòng:</strong>
                      {{ getRoomType(data.roomTypeId) }}
                    </li>
                    <li class="list-group-item">
                      <strong class="text-start">Giá tiền:</strong>
                      {{ formatCurrency(data.roomRate) }}
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="card">
          <h5 class="card-header border-bottom bg-light">Thông tin sinh viên</h5>
          <div class="card-body">
            <form>
              <div class="mb-3">
                <label for="cccd" class="form-label">CMNND/CCCD(*)</label>
                <input type="text" class="form-control" v-model="registration.Card" id="cccd" />
              </div>
              <div class="mb-3">
                <label for="date" class="form-label">Ngày CMNND/CCCD(*)</label>
                <input type="date" class="form-control" v-model="registration.CardDate"  id="date" />
              </div>
              <div class="mb-3">
                <label for="place__issue" class="form-label">Nơi cấp CMNND/CCCD(*)</label>
                <input type="text" class="form-control" id="place__issue" v-model="registration.AddressCard"  />
              </div>
              <div class="mb-3">
                <label for="fullname" class="form-label">Email</label>
                <input type="text" class="form-control"  v-model="registration.Email"  id="fullname" />
              </div>
              <div class="mb-3">
                <label for="fullname" class="form-label">Họ và Tên</label>
                <input type="text" class="form-control"  v-model="registration.FullName"  id="fullname" />
              </div>
              <div class="mb-3">
                <label for="gender" class="form-label">Giới tính</label>

                <div class="dropdown">
                  <select class="form-select" v-model="registration.Gender" >
                    <option selected disabled value="">Giới Tính</option>
                    <option
                      v-for="gender in genders"
                      :key="gender.id"
                      :value="gender.id"
                    >
                      {{ gender.name }}
                    </option>
                  </select>
                </div>
              </div>
              <div class="mb-3">
                <label for="cccd" class="form-label">Thời gian ở</label>

                <div class="form-check" v-for="item in date" :key="item.id">
                  <input
                    class="form-check-input"
                    type="radio"
                    name="flexRadioDefault"
                    id="flexRadioDefault1"
                    :value="item.roomId"
                    v-model="registration.TimeIn" 
                  />
                  <label class="form-check-label" for="flexRadioDefault1">
                    Đến ngày {{ formatDate(item.date) }}
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
        <button type="submit" class="btn btn-primary" @click="submitRegistration">Đăng ký</button>

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
