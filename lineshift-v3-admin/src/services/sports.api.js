import axiosInstance from "./instance/api";
import { parseApiErrorResponse } from "@/utilities/apiResponseParser";

const sportsApi = {
  async getAllSports() {
    try {
      const response = await axiosInstance.get("/sports");
      return response.data;
    } catch (error) {
      throw parseApiErrorResponse(error);
    }
  },
};

export default sportsApi;

// async createSport(sportDto) {
//     const { showSuccess, showError } = useSnackbarStore();
//     try {
//       const response = await axiosInstance.post(sportsEndpoint, sportDto);
//       showSuccess(`Successfully created sport '${sportDto.SportName}'`);
//       return response.data;
//     } catch (error) {
//       if (error.response) {
//         showError(error.response.data);
//         throw error;
//       }
//       else if (error.request) {

//         showError(error.request);
//         throw error;
//       }
//       else {
//         showError(error);
//         throw error;
//       }
//     }
//   }

// Create an api parser class that parses the error and throws an object up so that
// the caller (more than likely a store) and handle the showing of an error
