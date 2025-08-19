import axiosInstance from './instance/api';
import { loginEndpoint, initializeMeEndpoint } from './instance/endpoints';
import parseNetworkErrorResponse from './parsers/api.response.parser';


export const authApi = {
  async login(credentials) {
    try {
      const response = await axiosInstance.post(loginEndpoint, credentials);
      console.log(response)
      return response.data;
    } catch (error) {
      throw parseNetworkErrorResponse(error)
    }
  },

  async initializeMe() {
    try {
      const response = await axiosInstance.get(initializeMeEndpoint);
      return response.data;
    }
    catch (error) {
      throw parseNetworkErrorResponse(error)
    }
  }
};