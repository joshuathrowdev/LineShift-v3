import { defineStore } from 'pinia';
import { ref, computed } from 'vue';

import { authApi } from '@/services/auth.api';

// Store is responsible for managing its own state
// (includes api calls to get data, mapping, etc)


export const useAuthStore = defineStore('auth', () => {
  // State
  const sessionUser = ref(null);
  // Initialize token from localStorage when store is is first created
  // if a token already exist, it grabs that one
  const token = ref(localStorage.getItem('jwt_token') || null);
  const isLoading = ref(false); // for async ops
  const error = ref(null); // global error messages 


  // Getters (computed functions)
  const isAuthenticated = computed(() => !!token.value && !!sessionUser.value);

  const avatarContent = computed(() => {
    if (!isAuthenticated.value) {
      return '';
    }

    const userName = sessionUser.value.userName;
    const email = sessionUser.value.email;

    return userName ? userName.slice(0, 3).toUpperCase() : email.slice(0, 3).toUpperCase();
  });

  const formattedRegisteredDate = computed(() => {
    const isoDateTimeOffset = sessionUser.value.registeredDate;
    const dateObject = new Date(isoDateTimeOffset);
    const formattedDate = dateObject.toLocaleString('en-Us', {
      year: 'numeric',
      month: 'long',
      day: '2-digit'
    });

    return formattedDate;
  });






  // Actions / Mutators (reg functions)

  // Core Actions
  const initializeAuth = async () => {
    // initialize the Auth Store on app/page mount 

    // if we have a token but not a _sessionUser
    if (token.value && !isAuthenticated.value) {
      isLoading.value = true; // setting loading state
      error.value = null;

      try {
        const sessionUserData = await authApi.initializeMe();
        setAuthData(token.value, sessionUserData);

        // resetting loading value
        isLoading.value = false;
      }
      catch (error) {
        console.warn('Error occurred while trying to initialize session user', error);
      }
    }
  };

  const login = async (credentials) => {
    try {
      const authResponse = await authApi.login(credentials);
      if (authResponse) {
        setAuthData(authResponse.token, authResponse.sessionUser);
      }
    } catch (error) {
      console.warn('Error while attempting to login', error);
    }
  };

  const logout = async () => {
    const storageToken = localStorage.getItem('jwt_token');

    if (!storageToken) {
      console.warn('No JWT token currently in local storage memory');
      return;
    }
    if (!token.value) {
      console.warn('No token stored in auth store session user');
      return;
    }

    token.value = null;
    sessionUser.value = null;
    localStorage.removeItem('jwt_token');
  };

  const setAuthData = (newToken, authResponseUser) => {
    token.value = newToken; // sets new token
    sessionUser.value = authResponseUser;

    if (newToken) {
      // Set new _token to local storage
      localStorage.setItem('jwt_token', newToken);
    }
    else {
      localStorage.removeItem('jwt_token');
    }
  };


  return {
    // State
    sessionUser,
    token,
    isLoading,
    error,

    // Getters
    isAuthenticated,
    avatarContent,
    formattedRegisteredDate,

    // Actions
    login,
    logout,
    setAuthData,
    initializeAuth,
  };
});