// stores/invoiceStore.js
import { defineStore } from 'pinia';
import axios from 'axios';
export const useInvoiceStore = defineStore('invoice', {
    state: () => ({
        invoices: [],
        invoice: null,
        loading: false,
        error: null,
    }),
    actions: {
        async fetchInvoices() {
            this.loading = true;
            this.error = null;
            try {
                const response = await axios.get('https://localhost:7175/Invoices/GetInvoices'); // Adjust the URL as necessary
                this.invoices = response.data;
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        },

        async fetchInvoice(id) {
            this.loading = true;
            this.error = null;
            try {
                const response = await axios.get(`https://localhost:7175/Invoices/GetInvoice/${id}`);
                this.invoice = response.data;
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        },

        async createInvoice(invoice) {
            this.loading = true;
            this.error = null;
            try {
                const response = await axios.post('https://localhost:7175/Invoices/PostInvoice', invoice);
                console.log(response)
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        },

        async updateInvoice(id, invoice) {
            this.loading = true;
            this.error = null;
            try {
                await axios.put(`https://localhost:7175/Invoices/PutInvoice/${id}`, invoice);
                // Optionally update the local state after successful update
                const index = this.invoices.findIndex(inv => inv.id === id);
                if (index !== -1) {
                    this.invoices[index] = invoice;
                }
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        },

        async deleteInvoice(id) {
            this.loading = true;
            this.error = null;
            try {
                await axios.delete(`https://localhost:7175/Invoices/DeleteInvoice/${id}`);
                this.invoices = this.invoices.filter(inv => inv.id !== id);
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        },
    },
});
