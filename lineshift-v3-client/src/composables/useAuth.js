import { authApi } from '@/services/auth.api';

export const useAuth = () => {

  // Login Method
  const performLogin = async (credentials) => {
    try {
      const user = await authApi.login(credentials);
      // store setup for user (global point)
      console.log(user);
    }
    catch (error) {
      console.warn('There was an error while attempting to login', error);
    }
  };

  return { performLogin };
};