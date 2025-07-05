import axiosInstance from './instance/api';
import { loginEndpoint, initializeMeEndpoint } from './instance/endpoints';


export const authApi = {
  async login(credentials) {
    try {
      const response = await axiosInstance.post(loginEndpoint, credentials);
      return response.data;
    } catch (error) {
      console.warn('Error attempting to login user: ', error, credentials);
    }
  },

  async initializeMe() {
    try {
      const response = await axiosInstance.get(initializeMeEndpoint);
      console.log(response.data);
      return response.data;
    }
    catch (error) {
      console.warn('Error initializing session user from local jwt token: ', error);
    }
  }
};