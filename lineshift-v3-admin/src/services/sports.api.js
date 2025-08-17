import axiosInstance from "./instance/api";
import parseNetworkErrorResponse from "./parsers/api.response.parser";

const sportsApi = {
  async getAllSports() {
    try {
      const response = await axiosInstance.get("/sports");
      return response.data;
    } catch (error) {
      throw parseNetworkErrorResponse(error);
    }
  },
};

export default sportsApi;
