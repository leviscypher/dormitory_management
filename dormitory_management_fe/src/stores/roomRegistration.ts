// src/stores/roomRegistrationStore.js
import { defineStore } from 'pinia'
import axios from 'axios'

export const useRoomRegistrationStore = defineStore('roomRegistration', {
  state: () => ({
    registrations: [],
    gender: [],
    loading: false,
    error: null
  }),
  actions: {
    async fetchRoomRegistrations() {
      this.loading = true
      this.error = null
      try {
        const response = await axios.get('https://localhost:7175/RoomRegistrations/GetRoomRegistrations')
        this.registrations = response.data
         this.fetchGender()
      } catch (err) {
        this.error = err.message
      } finally {
        this.loading = false
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
    async createStudent(formData) {
        const response = await axios.post('/api/RoomRegistration', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        });
        return response;
      },
    async updateRoomRegistration(id, roomRegistrationData) {
      this.loading = true
      this.error = null
      try {
        await axios.put(`/api/RoomRegistration/${id}`, roomRegistrationData)
        const index = this.registrations.findIndex((r) => r.id === id)
        if (index !== -1) {
          this.registrations[index] = { ...this.registrations[index], ...roomRegistrationData }
        }
      } catch (err) {
        this.error = err.message
      } finally {
        this.loading = false
      }
    },
    async deleteRoomRegistration(id) {
      this.loading = true
      this.error = null
      try {
        await axios.delete(`/api/RoomRegistration/${id}`)
        this.registrations = this.registrations.filter((r) => r.id !== id)
      } catch (err) {
        this.error = err.message
      } finally {
        this.loading = false
      }
    }
  }
})
