import axiosInstance from './instance/api';
import { sportsEndpoint } from './instance/endpoints';


const sportsService = {

  async getAllSports() {
    try {
      const response = await axiosInstance.get(sportsEndpoint);
      return response.data;
    } catch (error) {
      console.warn('Error fetching all sports: ', error);
      throw error;
    }
  }
};

export default sportsService;