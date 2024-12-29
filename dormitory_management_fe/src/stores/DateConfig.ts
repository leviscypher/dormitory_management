import { defineStore } from 'pinia';
import axios from 'axios';

export const useDateConfigStore = defineStore('dateConfigStore', {
  state: () => ({
    dateConfigs: [],
    dateConfig: null,
    loading: false,
    error: null,
  }),
  
  actions: {
    async fetchDateConfigs() {
      this.loading = true;
      try {
        const response = await axios.get('https://localhost:7175/DateConfig/GetDateConfigs');
        this.dateConfigs = response.data;
      } catch (error) {
        this.error = error;
      } finally {
        this.loading = false;
      }
    },

    async fetchDateConfigById(id) {
      this.loading = true;
      try {
        const response = await axios.get(`https://localhost:7175/DateConfig/GetDateConfig/${id}`);
        this.dateConfig = response.data;
      } catch (error) {
        this.error = error;
      } finally {
        this.loading = false;
      }
    },

    async createDateConfig(dateConfig) {
      this.loading = true;
      try {
        const response = await axios.post('https://localhost:7175/DateConfig/PostDateConfig', dateConfig);
        this.dateConfigs.push(response.data);
      } catch (error) {
        this.error = error;
      } finally {
        this.loading = false;
      }
    },

    async updateDateConfig(id, dateConfig) {
      this.loading = true;
      try {
        await axios.put(`https://localhost:7175/DateConfig/PutDateConfig/${id}`, dateConfig);
        // Update the local dateConfigs array
        const index = this.dateConfigs.findIndex(config => config.id === id);
        if (index !== -1) {
          this.dateConfigs[index] = dateConfig;
        }
      } catch (error) {
        this.error = error;
      } finally {
        this.loading = false;
      }
    },

    async deleteDateConfig(id) {
      this.loading = true;
      try {
        await axios.delete(`https://localhost:7175/DateConfig/DeleteDateConfig/${id}`);
        // Remove from local dateConfigs array
        this.dateConfigs = this.dateConfigs.filter(config => config.id !== id);
      } catch (error) {
        this.error = error;
      } finally {
        this.loading = false;
      }
    },
  }
});
