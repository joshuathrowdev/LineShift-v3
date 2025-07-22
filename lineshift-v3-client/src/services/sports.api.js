import axiosInstance from './instance/api';
import { sportsEndpoint } from './instance/endpoints';


const sportsApi = {

  async getAllSports() {
    try {
      const response = await axiosInstance.get(sportsEndpoint);
      return response.data;
    } catch (error) {
      console.warn('Error fetching all sports: ', error);
      throw error;
    }
  },

  async createSport(sportDto) {
    try {
      const response = await axiosInstance.post(sportsEndpoint, sportDto);
      return response.data;
    } catch (error) {
      console.warn('Error posting sport: ' + error);
    }
  }
};

export default sportsApi;