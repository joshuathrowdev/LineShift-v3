import axiosInstance from './instance/api';
import { sportsEndpoint } from './instance/endpoints';
import parseNetworkErrorResponse from './parsers/api.response.parser';


const sportsApi = {
  async getAllSports() {
    try {
      const response = await axiosInstance.get(sportsEndpoint);
      return response.data;
    } catch (error) {
      throw parseNetworkErrorResponse(error);
    }
  },
};

export default sportsApi;

