import { useSnackbarStore } from '@/stores/snackbar.store';
import axiosInstance from './instance/api';
import { sportsEndpoint } from './instance/endpoints';


const sportsApi = {
  async getAllSports() {
    const { showError } = useSnackbarStore();
    try {
      const response = await axiosInstance.get(sportsEndpoint);
      return response.data;
    } catch (error) {
      showError(`Error fetching all sports: ${error}`);
      throw error;
    }
  },
};

export default sportsApi;

