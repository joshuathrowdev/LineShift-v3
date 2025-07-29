import axiosInstance from "./instance/api";
import { useAuthStore } from "@/stores/auth.store";

const authAdminApi = {
  async initializeFromLocalToken() {
    try {
      const response = await axiosInstance.get("/auth/me");
      return response.data;
    } catch (error) {
      throw error;
    }
  },

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
