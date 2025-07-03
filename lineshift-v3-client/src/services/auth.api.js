import axiosInstance from './api';
import { loginEndpoint } from './endpoints';


export const authApi = {
  async login(credentials) {
    try {
      console.log(credentials);
      const response = await axiosInstance.post(loginEndpoint, credentials);
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.warn('Error logging in user: ', error, credentials);
    }
  }
};