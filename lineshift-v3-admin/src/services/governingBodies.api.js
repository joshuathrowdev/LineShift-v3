import axiosInstance from "./instance/api";
import parseNetworkErrorResponse from "./parsers/api.response.parser";

const governingBodiesApi = {
  async getAllGoverningBodies() {
    try {
      const response = await axiosInstance.get("/governing-bodies");
      return response.data;
    } catch (error) {
      return parseNetworkErrorResponse(error);
    }
  },
};

export default governingBodiesApi;
