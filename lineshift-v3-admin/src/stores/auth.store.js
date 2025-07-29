import { defineStore } from "pinia";
import authAdminApi from "@/services/auth.api";

export const useAuthStore = defineStore("auth", () => {
  // State
  const user = ref(null);
  const token = ref(localStorage.getItem("jwt_token") || null);
  const isAuthReady = ref(null);
  const isLoading = ref(false);

  // Getters
  const isAuthenticated = computed(() => user.value && token.value);

  // Action
  const initializeAuth = async () => {
    // if we have a token but not a user
    if (token.value && !isAuthenticated.value) {
      isLoading.value = true; // setting loading state
      isAuthReady.value = false;

      try {
        const userData = await authAdminApi.initializeFromLocalToken();
        setAuthData(token.value, userData);
      } catch (error) {
        console.error(error);
      } finally {
        isLoading.value = false;
        isAuthReady.value = true;
      }
    }
  };

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

    if (responseToken) {
      localStorage.setItem("jwt_token", responseToken);
    } else {
      localStorage.removeItem("jwt_token");
    }
  };

  return {
    user,
    token,
    isAuthReady,
    isLoading,

    isAuthenticated,

    initializeAuth,
    login,
  };
});
