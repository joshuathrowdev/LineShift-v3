import { defineStore } from "pinia";
import authAdminApi from "@/services/auth.admin.api";

export const useAuthStore = defineStore("auth", () => {
  // State
  const user = ref(null);
  const token = ref(null);
  const isLoading = ref(false);

  // Getters
  const isAuthenticated = computed(() => user.value && token.value);

  // Action
  const login = async (credentials) => {
    try {
      isLoading.value = true;

      const response = await authAdminApi.login(credentials);

      if (response) {
        setAuthData(response.token, response.sessionUser);
      }
    } catch (error) {
      throw error;
    } finally {
      isLoading.value = false;
    }
  };

  const setAuthData = (responseToken, sessionUser) => {
    token.value = responseToken;
    user.value = sessionUser;
  };

  return {
    user,

    isAuthenticated,

    login,
  };
});
