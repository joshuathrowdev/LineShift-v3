import { useSnackbarStore } from '@/stores/snackbar.store';
import axiosInstance from './instance/api';
import { loginEndpoint, initializeMeEndpoint } from './instance/endpoints';


export const authApi = {
  async login(credentials) {
    const { showError } = useSnackbarStore();
    try {
      const response = await axiosInstance.post(loginEndpoint, credentials);
      return response.data;
    } catch (error) {
      if (error.response) {
        // backend responded with code outside 2xx
        showError(error.response.data.message);
        throw error;
      }
      else if (error.request) {
        // request was made but no response was received (network error)
        showError(error.request);
        throw error;
      }
      else {
        // something happened in setting up the request that triggered an error
        showError(error);
        throw error;
      }
    }
  },

  async initializeMe() {
    const { showSuccess, showError } = useSnackbarStore();
    try {
      const response = await axiosInstance.get(initializeMeEndpoint);
      showSuccess(`Successfully logged in user '${response.data.userName}'`);
      return response.data;
    }
    catch (error) {
      showError(error.message);
      throw error;
    }
  }
};