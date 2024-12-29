<script setup lang="ts">
import { onMounted, computed } from 'vue'
import { useRoomStore } from '@/stores/roomStore'
import { useRouter } from 'vue-router';



const roomStore = useRoomStore()

const fetchRooms = async () => {
  await roomStore.fetchRooms()

}
const fetchGender = async () => {
  await roomRegistrationStore.fetchGender()
  
}

onMounted(async () => {
  await fetchRooms()
})

const rooms = computed(() => roomStore.rooms)
const genders = computed(() => roomStore.gender)

const deleteRoom = async (roomId: number) => {
  if (confirm('Bạn có chắc muốn xóa phòng này?')) {
    await roomStore.deleteRoom(roomId)
  }
}
const getGenderNameByCode = (genderCode: any) => {
  const codeAsString = String(genderCode); // Convert to string
  const gender = genders.value.find(g => g.code === codeAsString);
  return gender ? gender.name : 'Không xác định';  
}

const formatCurrency = (amount: number): string => {
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND'
  }).format(amount)
}
const getRoomStatusText = (statusCode: string) => {
  switch (statusCode) {
    case '0':
      return 'Đóng'
    case '1':
      return 'Đang chờ'
    case '2':
      return 'Đang mở'
    default:
      return 'Không xác định'
  }
}
</script>

<template>
  <div>
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page">Quản lý phòng</li>
      </ol>
    </nav>

    <h5 class="mb-3">Danh sách phòng</h5>
    <div class="mb-3">
      <router-link to="/roommanager/create" class="btn btn-primary"> Thêm Phòng </router-link>
      <!-- <button class="btn btn-primary" @click="router.push('/roommanagement/create')">Quay lại</button> -->
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
            <th scope="col">Mã phòng</th>
            <th scope="col">Loại phòng</th>
            <th scope="col">Số lượng giường</th>
            <th scope="col">Giới tính</th>
            <th scope="col">Giá</th>
            <th scope="col">Trạng thái</th>
            <th scope="col">Tính năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(room, index) in rooms" :key="room.id">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ room.roomCode }}</td>
            <td> {{ room.roomTypeId === 1 ? 'Phòng 8 người' : room.roomTypeId === 2 ? 'Phòng 6 người' : '' }}</td>
            <td>{{ room.studentCount }}</td>
            <td>{{ getGenderNameByCode(room.gender) }}</td>
            <td>{{ formatCurrency(room.roomRate) }}</td>
            <td>{{ getRoomStatusText(room.roomStatus) }}</td>
            <td class="d-flex flex-column gap-2">
              <router-link
                :to="{ name: 'updateRoom', params: { id: room.id } }"
                class="btn btn-primary btn-sm"
              >
                Cập nhật
              </router-link>
              <router-link
                :to="{ name: 'detailRoom', params: { id: room.id } }"
                class="btn btn-primary btn-sm"
              >
                Chi tiết
              </router-link>
              <button @click="deleteRoom(room.id)" class="btn btn-danger btn-sm">Xóa</button>
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
