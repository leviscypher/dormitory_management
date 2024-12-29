// src/stores/adminAccountStore.js
import { defineStore } from 'pinia';
import axios from 'axios';

export const useAdminAccountStore = defineStore('adminAccount', {
  state: () => ({
    adminAccounts: [],
    adminAccount: null,
    loading: false,
    error: null,
  }),
  actions: {
    async fetchAdminAccounts() {
      this.loading = true;
      this.error = null;

      try {
        const response = await axios.get('https://localhost:7175/AdminAccount/GetAdminAccounts');
        this.adminAccounts = response.data;
      } catch (err) {
        this.error = err.message || 'An error occurred while fetching admin accounts.';
      } finally {
        this.loading = false;
      }
    },
    async fetchAccountById(id) {
      try {
        const response = await axios.get(`https://localhost:7175/AdminAccount/GetAdminAccount/${id}`);
        this.adminAccount = response.data;
      } catch (error) {
        this.error = error.response ? error.response.data : error.message;
      }
    },
    async addAdminAccount(newAccount) {
      this.loading = true;
      this.error = null;

      try {
        const response = await axios.post('https://localhost:7175/AdminAccount/PostAdminAccount', newAccount);
        this.adminAccounts.push(response.data);
      } catch (err) {
        this.error = err.message || 'An error occurred while adding an admin account.';
      } finally {
        this.loading = false;
      }
    },

    async updateAdminAccount(updatedAccount) {
      this.loading = true;
      this.error = null;

      try {
        await axios.put(`https://localhost:7175/AdminAccount/PutAdminAccount/${updatedAccount.id}`, updatedAccount);
        const index = this.adminAccounts.findIndex(account => account.id === updatedAccount.id);
        if (index !== -1) {
          this.adminAccounts[index] = updatedAccount;
        }
      } catch (err) {
        this.error = err.message || 'An error occurred while updating the admin account.';
      } finally {
        this.loading = false;
      }
    },

    async deleteAdminAccount(id) {
      this.loading = true;
      this.error = null;

      try {
        await axios.delete(`https://localhost:7175/AdminAccount/DeleteAdminAccount/${id}`);
        this.adminAccounts = this.adminAccounts.filter(account => account.id !== id);
      } catch (err) {
        this.error = err.message || 'An error occurred while deleting the admin account.';
      } finally {
        this.loading = false;
      }
    },
  },
});
