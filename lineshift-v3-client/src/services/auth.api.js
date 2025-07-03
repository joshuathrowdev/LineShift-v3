import axiosInstance from './api';
import { loginEndpoint } from './endpoints';


export const authApi = {
  async login(credentials) {
    try {
      const response = await axiosInstance.post(loginEndpoint, credentials);
      return response.data;
    } catch (error) {
      console.warn('Error logging in user: ', error, credentials);
    }
  }
};