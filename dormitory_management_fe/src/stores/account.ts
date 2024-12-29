// src/stores/accountStore.js
import { defineStore } from 'pinia';
import axios from 'axios';

export const useAccountStore = defineStore('account', {
  state: () => ({
    accounts: [],
    account: null,
    error: null,
  }),
  actions: {
    async fetchAccounts() {
      try {
        const response = await axios.get('https://localhost:7175/Account/GetAllAccounts');
        this.accounts = response.data;
      } catch (error) {
        this.error = error.response ? error.response.data : error.message;
      }
    },
    async fetchAccountById(id) {
      try {
        const response = await axios.get(`https://localhost:7175/Account/GetAccountById/${id}`);
        this.account = response.data;
      } catch (error) {
        this.error = error.response ? error.response.data : error.message;
      }
    },
    async createAccount(newAccount) {
      try {
        const response = await axios.post('https://localhost:7175/Account/CreateAccount', newAccount);
        this.accounts.push(response.data); 
      } catch (error) {
        this.error = error.response ? error.response.data : error.message;
      }
    },
    async updateAccount(id, updatedAccount) {
      try {
        const response = await axios.put(`https://localhost:7175/Account/UpdateAccount/${id}`, updatedAccount);
        const index = this.accounts.findIndex(account => account.id === id);
        if (index !== -1) {
          this.accounts[index] = response.data; // Update the account in the state
        }
      } catch (error) {
        this.error = error.response ? error.response.data : error.message;
      }
    },
    async deleteAccount(id) {
      try {
        await axios.delete(`https://localhost:7175/Account/DeleteAccount/${id}`);
        this.accounts = this.accounts.filter(account => account.id !== id); // Remove the deleted account from the state
      } catch (error) {
        this.error = error.response ? error.response.data : error.message;
      }
    },
  },
});
