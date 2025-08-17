import axiosInstance from "./instance/api";
import parseNetworkErrorResponse from "./parsers/api.response.parser";

const leaguesApi = {
  async getAllLeagues() {
    try {
      const response = await axiosInstance.get("/leagues");
      return response.data;
    } catch (error) {
      throw parseNetworkErrorResponse(error);
    }
  },
};

export default leaguesApi;
