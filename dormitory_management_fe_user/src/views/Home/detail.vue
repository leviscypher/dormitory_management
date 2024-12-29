<template>
  <div class="container main">
    <div class="row">
      <div class="col-12 col-lg-6 col-xl-6">
        <div class="card">
          <h5 class="card-header border-bottom bg-light">Thông tin phòng</h5>
          <div class="card-body">
            <table class="table table-striped">
              <thead>
                <tr>
                  <th scope="col">Toà nhà</th>
                  <th scope="col">Tầng</th>
                  <th scope="col">Phòng</th>
                  <th scope="col">Loại Phòng</th>
                  <th scope="col">Giá phòng</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <th scope="row">{{ data.areaId }}</th>
                  <td>{{ data.floorId }}</td>
                  <td>{{ data.roomCode }}</td>
                  <td>{{ getRoomType(data.roomTypeId) }}</td>
                  <td>{{ formatCurrency(data.roomRate)  }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div class="col-12 col-lg-6 col-xl-6">
        <div class="card">
          <h5 class="card-header border-bottom bg-light">Chỗ còn chống</h5>
          <div class="card-body">
            <div class="bd-example">
              <div class="list-group">
                <label class="list-group-item disabled ">
                  Số lượng giường còn lại:  {{data.registeredCount}}/{{ data.studentCount }}
                </label>
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
      <div class="col-12 col-lg-6 col-xl-6" >
        <router-link :to="`/bookroom/${ data.id}`" type="submit" class="btn btn-primary">Kế tiếp</router-link>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { useRouter, useRoute } from 'vue-router';
import { onMounted,computed } from 'vue';
import { useDormitoryStore } from '@/stores/dormitory';

const roomStore = useDormitoryStore();
const route = useRoute(); 
const roomId = route.params.id; 
const fetchRoomDetail = async () => {
    await roomStore.fetchRoomDetail(roomId);
};

onMounted(() => {
    fetchRoomDetail(); // Gọi hàm fetchRoomDetail khi component được mounted
});
const data = computed(() => roomStore.roomDetail || {}); 
const router = useRouter();
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
const goBack = () => {
    router.back();
}
</script>
<style lang="css" scoped>
.main {
  padding: 15px 20px;
}
</style>
