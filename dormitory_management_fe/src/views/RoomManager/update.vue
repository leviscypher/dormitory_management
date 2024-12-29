<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoomStore } from '@/stores/roomStore'
import { useRouter } from 'vue-router'

const roomStore = useRoomStore()
const router = useRouter()

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
})

const roomId = router.currentRoute.value.params.id

const fetchRoom = async () => {
  await roomStore.fetchRoom(roomId)
  if (roomStore.room) {
    Object.assign(room.value, roomStore.room)
  }
}

const updateRoom = async () => {
  try {
    await roomStore.updateRoom(roomId, room.value)
    alert('Cập nhập thành công')
    fetchRoom()
    router.push({ path: '/roommanager' })
  } catch (error) {
    console.error('Error updating room:', error)
  }
}

onMounted(fetchRoom)
</script>

<template>
  <div>
    <h5 class="mb-3">Cập nhật thông tin phòng</h5>
    <form @submit.prevent="updateRoom">
      <div class="row">
        <div class="col-6">
          <div class="mb-3">
            <label for="roomCode" class="form-label">Mã phòng</label>
            <input type="text" class="form-control" id="roomCode" v-model="room.roomCode" />
          </div>
          <div class="mb-3">
            <label for="studentCount" class="form-label">Số sinh viên</label>
            <input
              type="number"
              class="form-control"
              id="studentCount"
              v-model="room.studentCount"
            />
          </div>
          <div class="mb-3">
            <label for="roomStatus" class="form-label">Trạng thái phòng</label>
            <div>
              <input type="radio" id="roomStatus" value="2" v-model="room.roomStatus" />
              <label for="roomStatus">Đang mở</label>
            </div>
            <div>
              <input type="radio" id="roomStatus1" value="1" v-model="room.roomStatus" />
              <label for="roomStatus1">Đang chờ</label>
            </div>
            <div>
              <input type="radio" id="roomStatus2" value="0" v-model="room.roomStatus" />
              <label for="roomStatus2">Đã đóng</label>
            </div>
          </div>
          <div class="mb-3">
            <label for="roomRate" class="form-label">Giá phòng</label>
            <input type="number" class="form-control" id="roomRate" v-model="room.roomRate" />
          </div>
        </div>
        <div class="col-6">
          <div class="mb-3">
            <label class="form-label">Giới tính</label>
            <div>
              <input
                type="radio"
                id="genderMale"
                value="2"
                v-model="room.gender"
                :checked="room.gender === 2"
              />
              <label for="genderMale">Nam</label>
            </div>
            <div>
              <input
                type="radio"
                id="genderFemale"
                value="1"
                v-model="room.gender"
                :checked="room.gender === 1"
              />
              <label for="genderFemale">Nữ</label>
            </div>
            <div>
              <input
                type="radio"
                id="genderOther"
                value="0"
                v-model="room.gender"
                :checked="room.gender === 0"
              />
              <label for="genderOther">Khác</label>
            </div>
          </div>
          <div class="mb-3">
            <label for="roomTypeId" class="form-label">Loại phòng</label>
            <select class="form-select" v-model="room.roomTypeId">
              <option selected>Chọn loại phòng</option>
              <option value="1">Phòng 8 người</option>
              <option value="2">Phòng 6 người</option>
            </select>
          </div>
          <div class="mb-3">
            <label for="floorId" class="form-label">Tầng</label>
            <input type="text" class="form-control" id="floorId" v-model="room.floorId" />
          </div>
          <div class="mb-3">
            <label for="areaId" class="form-label">Toà nhà</label>
            <input type="text" class="form-control" id="areaId" v-model="room.areaId" />
          </div>
        </div>
        <div class="w-25">
          <div class="d-flex gap-3">
            <div class="col-6 col-lg-6">
              <div class="col-12">
                <button class="btn btn-danger" @click="router.push('/roommanager')">
                  Quay lại
                </button>
              </div>
            </div>
            <div class="col-6 col-lg-6">
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
