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

  async createSport(sportDto) {
    const { showSuccess, showError } = useSnackbarStore();
    try {
      const response = await axiosInstance.post(sportsEndpoint, sportDto);
      showSuccess(`Successfully created sport '${sportDto.SportName}'`);
      return response.data;
    } catch (error) {
      if (error.response) {
        showError(error.response.data);
        throw error;
      }
      else if (error.request) {

        showError(error.request);
        throw error;
      }
      else {
        showError(error);
        throw error;
      }
    }
  }
};

export default sportsApi;

