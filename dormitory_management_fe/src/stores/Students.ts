import { defineStore } from 'pinia';
import axios from 'axios';

// Khởi tạo store với Pinia
export const useStudentStore = defineStore('student', {
  state: () => ({
    students: [],
    student: null,
  }),

  actions: {
    // Lấy danh sách sinh viên
    async fetchStudents() {
      try {
        const response = await axios.get('https://localhost:7175/Student/GetStudents');
         this.students = response.data;

      } catch (error) {
        console.error('Error fetching students:', error);
      }
    },

    // Lấy sinh viên theo ID
    async fetchStudent(studentId:any) {
      try {
        const response = await axios.get(`https://localhost:7175/Student/GetStudent/${studentId}`);
        this.student = response.data;
      } catch (error) {
        console.error('Error fetching student:', error);
      }
    },

    // Thêm sinh viên mới
    async createStudent(student:any) {
      try {
        await axios.post('https://localhost:7175/Student/CreateStudent', student);
        await this.fetchStudents(); 
      } catch (error) {
        console.error('Error creating student:', error);
      }
    },

    // Chỉnh sửa sinh viên
    async editStudent(studentId:any, updatedStudent:any) {
      try {
        await axios.put(`https://localhost:7175/Student/EditStudent/${studentId}`, updatedStudent);
        await this.fetchStudents(); 
      } catch (error) {
        console.error('Error editing student:', error);
      }
    },

    // Xóa sinh viên
    async deleteStudent(studentId:any) {
      try {
        await axios.delete(`https://localhost:7175/Student/DeleteStudent/${studentId}`);
        await this.fetchStudents(); 
      } catch (error) {
        console.error('Error deleting student:', error);
      }
    },
  },
});
