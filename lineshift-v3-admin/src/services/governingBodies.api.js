import { parseApiErrorResponse } from "@/utilities/apiResponseParser";
import axiosInstance from "./instance/api";

const governingBodiesApi = {
  async getAllGoverningBodies() {
    try {
      const response = await axiosInstance.get("/governing-bodies");
      return response.data;
    } catch (error) {
      return parseApiErrorResponse(error);
    }
  },
};

export default governingBodiesApi;
