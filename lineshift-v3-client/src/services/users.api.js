import axiosInstance from './api';
import { loginEndpoint } from './endpoints';


const usersService = {
  async getUser(user) {
    try {
      const response = await axiosInstance.post(loginEndpoint, user);
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.warn('Error logging in user: ', error, user);
    }
  }
};