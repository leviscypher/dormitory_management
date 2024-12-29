<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoomStore } from '@/stores/roomStore'; 
import { useRouter } from 'vue-router'; 

const roomStore = useRoomStore(); 
const router = useRouter(); 
const room = ref({
  roomCode: '',
  roomType: '',
  studentCount: 0,
  roomStatus: '',
  roomRate: 0,
  gender: '',
  roomTypeId: '',
  floorId: '',
  areaId: ''
});

const fetchRoomId = router.currentRoute.value.params.id; 

const fetchRoom = async () => {
  await roomStore.fetchRoom(fetchRoomId); 
  if (roomStore.room) {
    Object.assign(room.value, roomStore.room); // Gán dữ liệu từ roomStore vào room
  }
};

onMounted(fetchRoom); 

const formatDate = (dateString: string): string => {
  const date = new Date(dateString);
  return date.toLocaleDateString('vi-VN');
};
const formatCurrency = (amount: number): string => {
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND'
  }).format(amount)
}
const getGenderNameByCode = (genderCode: any) => {
  const code = String(genderCode);

  switch (code) {
    case '2':
      return 'Nam';
    case '1':
      return 'Nữ';
    case '0':
      return 'Khác';
    default:
      return 'Không xác định';
  }
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
    <h5 class="mb-3">Thông tin phòng</h5>
    <div class="row">
      <div class="col-6">
        <div class="mb-3">
          <label class="form-label">Mã phòng</label>
          <div>{{ room.roomCode }} </div>
        </div>
        <div class="mb-3">
          <label class="form-label">Loại phòng</label>
          <div>{{ room.roomType }} </div>
        </div>
        <div class="mb-3">
          <label class="form-label">Số lượng giường</label>
          <div>{{ room.studentCount }} giường </div>
        </div>
        <div class="mb-3">
          <label class="form-label">Trạng thái</label>
          <div>{{ getRoomStatusText(room.roomStatus)  }}</div>
        </div>
      </div>
      <div class="col-6">
        <div class="mb-3">
          <label class="form-label">Giới tính</label>
          <div>{{  getGenderNameByCode(room.gender)  }} </div>
        </div>
        <div class="mb-3">
          <label class="form-label">Giá phòng</label>
          <div>{{ formatCurrency(room.roomRate )}} </div>
        </div>
        <div class="mb-3">
          <label class="form-label">Toà nhà</label>
          <div>{{ room.floorId }} </div>
        </div>
        <div class="mb-3">
          <label class="form-label">Tầng</label>
          <div>{{ room.areaId }} </div>
        </div>
        <div class="mb-3">
          <label class="form-label">Phòng</label>
          <div>{{ room.areaId }} </div>
        </div>
      </div>
      <div class="col-12">
        <button class="btn btn-primary" @click="router.push('/roommanager')">Quay lại</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
</style>
