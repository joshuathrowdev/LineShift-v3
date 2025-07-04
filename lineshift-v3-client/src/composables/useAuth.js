import { authApi } from '@/services/auth.api';

// Instantiating Auth Store Instance
import { useAuthStore } from '@/stores/auth.store';
const authStore = useAuthStore();

export const useAuth = () => {

  // Login Method
  const performLogin = async (credentials) => {
    try {
      const authResponseUser = await authApi.login(credentials);
      // store setup for user (global point)
      if (authResponseUser) {
        authStore.setAuthData(authResponseUser.token, authResponseUser);
      }
    }
    catch (error) {
      console.warn('There was an error while attempting to login', error);
    }
  };

  // 

  return { performLogin };
};