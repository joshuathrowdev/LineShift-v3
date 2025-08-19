import axiosInstance from "./instance/api";
import parseNetworkErrorResponse from "./parsers/api.response.parser";

const authAdminApi = {
  async initializeFromLocalToken() {
    try {
      const response = await axiosInstance.get("/auth/me");
      return response.data;
    } catch (error) {
      throw parseNetworkErrorResponse(error);
    }
  },

  async login(credentials) {
    try {
      const response = await axiosInstance.post("/auth/login", credentials);
      return response.data;
    } catch (error) {
      throw parseNetworkErrorResponse(error);
    }
  },
};

export default authAdminApi;
