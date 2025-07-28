import axiosInstance from "./instance/api";

const authAdminApi = {
  async login(credentials) {
    try {
      const response = await axiosInstance.post("/auth/login", credentials);
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};

export default authAdminApi;
