import axiosInstance from './instance/api';
import { loginEndpoint, initializeMeEndpoint } from './instance/endpoints';


export const authApi = {
  async login(credentials) {
    try {
      const response = await axiosInstance.post(loginEndpoint, credentials);
      return response.data;
    } catch (error) {
      if (error.response) {
        // backend responded with code outside 2xx
        throw error;
      }
      else if (error.request) {
        // request was made but no response was received (network error)
        throw error;
      }
      else {
        // something happened in setting up the request that triggered an error
        throw error;
      }
    }
  },

  async initializeMe() {
    try {
      const response = await axiosInstance.get(initializeMeEndpoint);
      return response.data;
    }
    catch (error) {
      console.warn('Error initializing session user from local jwt token: ', error);
    }
  }
};