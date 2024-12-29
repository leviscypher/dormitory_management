// stores/auth.js
import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null,
    isLoggedIn: false,
    error: null,
  }),
  actions: {
    async login(username, password) {
      try {
        const response = await axios.post('https://localhost:7175/Auth/Login/login', {
          Username: username,
          Password: password
        })
        this.user = response.data.user 
        this.isLoggedIn = true
        console.log(response.data)
        this.error = null
      } catch (err) {
        if (err.response) {
          this.error = err.response.data.message || 'Invalid login attempt'
        } else {
          this.error = 'An error occurred'
        }
        this.isLoggedIn = false
      }
    },
    async logout() {
      try {
        await axios.post('https://localhost:7175/Auth/Logout/logout')
        this.user = null
        this.isLoggedIn = false
        this.error = null
      } catch (err) {
        this.error = 'Error logging out'
      }
    }
  }
})
