import { defineStore } from 'pinia'
import axios from 'axios'

export const useDormitoryStore = defineStore('dormitory', {
  state: () => ({
    areas: [],
    roomTypes: [],
    floors: [],
    genders: [],
    date: [],
    roomDetail: null,
    loading: false,
    error: null
  }),
  actions: {
    // Fetch Areas
    async fetchAreas() {
      this.loading = true
      this.error = null
      try {
        const response = await axios.get('https://localhost:7175/Users/GetAreas')
        this.areas = response.data
      } catch (error) {
        this.error = 'Failed to fetch areas'
      } finally {
        this.loading = false
      }
    },

    // Fetch Room Types
    async fetchRoomTypes() {
      this.loading = true
      this.error = null
      try {
        const response = await axios.get('https://localhost:7175/Users/GetRoomType')
        this.roomTypes = response.data
      } catch (error) {
        this.error = 'Failed to fetch room types'
      } finally {
        this.loading = false
      }
    },

    // Fetch Floors
    async fetchFloors() {
      this.loading = true
      this.error = null
      try {
        const response = await axios.get('https://localhost:7175/Users/GetFloor')
        this.floors = response.data
      } catch (error) {
        this.error = 'Failed to fetch floors'
      } finally {
        this.loading = false
      }
    },

    // Fetch Genders
    async fetchGenders() {
      this.loading = true
      this.error = null
      try {
        const response = await axios.get('https://localhost:7175/Users/GetGender')
        this.genders = response.data
      } catch (error) {
        this.error = 'Failed to fetch genders'
      } finally {
        this.loading = false
      }
    },
    async fetchGetDate() {
      this.loading = true
      this.error = null

      try {
        const response = await axios.get(`https://localhost:7175/Users/GetDate/${5}`)
        this.date = response.data
      } catch (error) {
        this.error = 'Failed to fetch date config'
        console.error(error)
      } finally {
        this.loading = false
      }
    },
    async searchRooms(gender = null, roomTypeId = null, floorId = null, areaId = null) {
      const params = {
        gender,
        roomTypeId,
        floorId,
        areaId
      }
      const response = await axios.get('https://localhost:7175/Users/SearchRoom', { params })
      this.rooms = response.data
    },
    async fetchRoomDetail(id) {
      this.loading = true
      this.error = null
      try {
        const response = await axios.get(`https://localhost:7175/Users/GetDetail?id=${id}`)
        this.roomDetail = response.data
      } catch (err) {
        this.error = err
        console.error(err) // In ra lỗi nếu có
      } finally {
        this.loading = false
      }
    },
    async addRoomRegistration(roomData) {
      try {
        const formData = new FormData();
        
        formData.append('Card', roomData.Card);
        formData.append('AddressCard', roomData.AddressCard);
        formData.append('Email', roomData.Email);
        formData.append('FullName', roomData.FullName);
        formData.append('Gender', roomData.Gender);
        formData.append('TimeIn', roomData.TimeIn);
        formData.append('RoomId', roomData.RoomId);
    
        if (roomData.IDPhoto) { 
          formData.append('idPhoto', roomData.IDPhoto);
        }
        if (roomData.Before) { 
          formData.append('before', roomData.Before);
        }
        if (roomData.After) { 
          formData.append('after', roomData.After);
        }
    
    
        // Post the data using axios
        const response = await axios.post('https://localhost:7175/Users/AddRoomRegistration', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        });
    
        return response.data; // Handle the response as needed
      } catch (error) {
        console.error('Error adding room registration:', error);
        throw error; // Optional: rethrow the error for further handling
      }
    }
    
  }
})
