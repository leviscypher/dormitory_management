<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useDormitoryStore } from '@/stores/dormitory'
import TheCard from '@/components/card/index.vue'

const dormitoryStore = useDormitoryStore()

const selectedArea = ref('')
const selectedRoomType = ref('')
const selectedFloor = ref('')
const selectedGender = ref('')

onMounted(() => {
  dormitoryStore.fetchAreas()
  dormitoryStore.fetchRoomTypes()
  dormitoryStore.fetchFloors()
  dormitoryStore.fetchGenders()
})

const canSearch = () => {
  return selectedArea.value && selectedRoomType.value && selectedFloor.value && selectedGender.value
}

const searchRooms = () => {
  if (canSearch()) {
    dormitoryStore.searchRooms(
      selectedGender.value,
      selectedRoomType.value,
      selectedFloor.value,
      selectedArea.value
    )
  }
}
watch([selectedArea, selectedRoomType, selectedFloor, selectedGender], () => {
  searchRooms()
})

const getRoomTypeName = (roomTypeId: any) => {
  console.log(roomTypeId)
  const roomType = dormitoryStore.roomTypes.find(rt => rt.id == 3)
  return roomType ? roomType.name : 'Unknown'
}
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
</script>
<template>
  <div class="select_header">
    <div class="d-flex justify-content-center gap-3">
      <div class="dropdown">
        <select class="form-select" v-model="selectedArea">
          <option disabled value="">Toà nhà</option>
          <option v-for="area in dormitoryStore.areas" :key="area.id" :value="area.id">
            {{ area.areaNumber }}
          </option>
        </select>
      </div>
      <div class="dropdown">
        <select class="form-select" v-model="selectedRoomType" aria-label="Loại phòng">
          <option disabled value="">Loại phòng</option>
          <option
            v-for="roomType in dormitoryStore.roomTypes"
            :key="roomType.id"
            :value="roomType.id"
          >
            {{ roomType.typeDescription }}
          </option>
        </select>
      </div>
      <div class="dropdown">
        <select class="form-select" v-model="selectedFloor" aria-label="Tầng">
          <option disabled value="">Tầng</option>
          <option v-for="floor in dormitoryStore.floors" :key="floor.id" :value="floor.id">
            {{ floor.floorNumber }}
          </option>
        </select>
      </div>
      <div class="dropdown">
        <select class="form-select" v-model="selectedGender" aria-label="Giới Tính">
          <option disabled value="">Giới Tính</option>
          <option v-for="gender in dormitoryStore.genders" :key="gender.id" :value="gender.id">
            {{ gender.name }}
          </option>
        </select>
      </div>
    </div>
  </div>
  <div class="main">
    <div class="row">
      <div class="col-12 col-lg-4 col-xl-3">
        <div v-for="room in dormitoryStore.rooms" :key="room.id">
          <router-link v-if="room.registeredCount < room.studentCount" :to="`/detail/${room.id}`">
            <div class="card text-dark">
              <div class="card-body">
                <h5 class="card-title">{{ room.roomCode }}</h5>
                <p class="card-text">{{ getRoomType(room.roomTypeId) }}</p>
                <p class="card-text"><strong>Số lượng giường còn lại:</strong> {{room.registeredCount}}/{{ room.studentCount }}</p>
              </div>
            </div>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="css" scoped>
@import url(@/assets/css/home/home.css);
</style>
