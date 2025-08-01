import { useSnackbarStore } from "@/stores/snackbar.store";
import axiosInstance from "./instance/api";
import { useAuthStore } from "@/stores/auth.store";
import { parseApiErrorResponse } from "@/utilities/apiResponseParser";

const authAdminApi = {
  async initializeFromLocalToken() {
    const { showError } = useSnackbarStore();
    try {
      const response = await axiosInstance.get("/auth/me");
      return response.data;
    } catch (error) {
      showError(
        "An error occurred while attempting to login from session token",
      );
    }
  },

  async login(credentials) {
    try {
      const response = await axiosInstance.post("/auth/login", credentials);
      return response.data;
    } catch (error) {
      throw parseApiErrorResponse(error);
    }
  },
};

export default authAdminApi;
