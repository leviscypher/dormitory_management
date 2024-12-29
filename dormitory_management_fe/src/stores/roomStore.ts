import { defineStore } from 'pinia';
import axios from 'axios';

export const useRoomStore = defineStore('roomStore', {
    state: () => ({
        rooms: [],
        gender:[],
        room: null,
        error: null,
        loading: false,
    }),
    actions: {
        // Lấy danh sách các phòng
        async fetchRooms() {
            this.loading = true;
            try {
                const response = await axios.get('https://localhost:7175/Room/GetAllRoom');
                this.rooms = response.data;
                this.fetchGender()
            } catch (error) {
                this.error = error.response ? error.response.data : error.message;
            } finally {
                this.loading = false;
            }
        },
        async fetchGender() {
            this.loading = true
            this.error = null
            try {
              const response = await axios.get('https://localhost:7175/RoomRegistrations/GetGenders')
              this.gender = response.data
      
            } catch (err) {
              this.error = err.message
            } finally {
              this.loading = false
            }
          },
        // Lấy thông tin một phòng cụ thể
        async fetchRoom(id) {
            this.loading = true;
            try {
                const response = await axios.get(`https://localhost:7175/Room/GetRoom/${id}`);
                this.room = response.data;
            } catch (error) {
                this.error = error.response ? error.response.data : error.message;
            } finally {
                this.loading = false;
            }
        },

        // Tạo mới một phòng
        async createRoom(newRoom) {
            this.loading = true;
            try {
                const response = await axios.post('https://localhost:7175/Room/CreateRoom', newRoom);
                this.rooms.push(response.data); // Cập nhật danh sách với phòng vừa tạo
            } catch (error) {
                this.error = error.response ? error.response.data : error.message;
            } finally {
                this.loading = false;
            }
        },

        // Cập nhật thông tin một phòng
        async updateRoom(id, updatedRoom) {
            this.loading = true;
            try {
                await axios.put(`https://localhost:7175/Room/UpdateRoom/${id}`, updatedRoom);
                const index = this.rooms.findIndex(r => r.id === id);
                if (index !== -1) {
                    this.rooms[index] = { ...this.rooms[index], ...updatedRoom };
                }
            } catch (error) {
                this.error = error.response ? error.response.data : error.message;
            } finally {
                this.loading = false;
            }
        },

        // Xóa một phòng
        async deleteRoom(id) {
            this.loading = true;
            try {
                await axios.delete(`https://localhost:7175/Room/DeleteRoom/${id}`);
                this.rooms = this.rooms.filter(r => r.id !== id); // Xoá phòng khỏi danh sách
            } catch (error) {
                this.error = error.response ? error.response.data : error.message;
            } finally {
                this.loading = false;
            }
        }
    }
});
