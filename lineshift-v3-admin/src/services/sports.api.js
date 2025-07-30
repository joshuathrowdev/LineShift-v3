import { useSnackbarStore } from "@/stores/snackbar.store";
import axiosInstance from "./instance/api";
import { parseApiErrorResponse } from "@/utilities/apiResponseParser";

const sportsApi = {
  async getAllSports() {
    try {
      const response = await axiosInstance.get("/sports");
      console.log(response.data);
      return response.data;
    } catch (error) {
      throw parseApiErrorResponse(error);
    }
  },
};

export default sportsApi;
