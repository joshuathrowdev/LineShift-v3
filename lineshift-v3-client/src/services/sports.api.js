import AxiosInstance from './api';
import { SportsEndpoint } from './globalEndpoints';

const SportsService = {

  async getAllSports() {
    try {
      const response = await AxiosInstance.get(`${SportsEndpoint}`);
      return response.data;
    } catch (error) {
      console.warn('Error fetching all sports: ', error);
      throw error;
    }
  }
};

export default SportsService;