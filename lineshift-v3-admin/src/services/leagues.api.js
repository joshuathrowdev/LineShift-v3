import { parseApiErrorResponse } from "@/utilities/apiResponseParser";
import axiosInstance from "./instance/api";

const leaguesApi = {
  async getAllLeagues() {
    try {
      const response = await axiosInstance.get("/leagues");
      console.log(response.data);
      return response.data;
    } catch (error) {
      throw parseApiErrorResponse(error);
    }
  },
};

export default leaguesApi;
