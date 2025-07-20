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
  const isAuthReady = ref(false); // for definitive authentication value
  const error = ref({
    message: '',
    code: null
  }); // global error messages 


  // Getters (computed functions)
  const isAuthenticated = computed(() => !!token.value && !!sessionUser.value);

  const isAdmin = computed(() => sessionUser.value.roles.find(role => role == 'Admin'));

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
      isAuthReady.value = false;
      error.value = null;

      try {
        const sessionUserData = await authApi.initializeMe();
        setAuthData(token.value, sessionUserData);
      }
      catch (error) {
        console.warn('Error occurred while trying to initialize session user', error);
      }
      finally {
        // resetting loading value
        isLoading.value = false;
        isAuthReady.value = true;
      }
    }
  };

  const login = async (credentials) => {
    try {
      isLoading.value = true;
      isAuthReady.value = false;

      const authResponse = await authApi.login(credentials);
      if (authResponse) {
        setAuthData(authResponse.token, authResponse.sessionUser);
      }
    } catch (caughtError) {
      console.log(caughtError.response);
      console.log(caughtError.request);
      console.log(caughtError);

      if (caughtError.response) {
        error.value.message = caughtError.response.data.message;
        error.value.code = caughtError.response.status;
        console.log(error.value);
      }
    }
    finally {
      isLoading.value = false;
      isAuthReady.value = true;
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

    setAuthData(null, null);
    isAuthReady.value = true;
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
    isAuthReady,
    error,

    // Getters
    isAuthenticated,
    isAdmin,
    avatarContent,
    formattedRegisteredDate,

    // Actions
    login,
    logout,
    setAuthData,
    initializeAuth,
  };
});




// isLoading indicates an asynchronous operation is currently active (e.g., fetching data, logging in).
// isAuthReady signifies that the authentication state has been definitively evaluated and settled, meaning isAuthenticated now holds its final, reliable value.
// We need isAuthReady to prevent race conditions in navigation guards, ensuring they only make redirection decisions after the authentication status is fully known, especially on page load or refresh.