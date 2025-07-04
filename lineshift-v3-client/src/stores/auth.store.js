import { defineStore } from 'pinia';
import { ref, computed } from 'vue';


export const useAuthStore = defineStore('auth', () => {
  // State
  const _sessionUser = ref(null);
  // Initialize token from localStorage when store is is first created
  // if a token already exist, it grabs that one
  const _token = ref(localStorage.getItem('jwt_token') || null);
  const _isLoading = ref(false); // for async ops
  const _error = ref(null); // global error messages 


  // Getters (computed functions)
  const sessionUser = computed(() => _sessionUser.value);
  const authToken = computed(() => _token.value);
  const isLoading = computed(() => _isLoading.value);
  const error = computed(() => _error.value);
  const isAuthenticated = computed(() => !!_token.value && !!_sessionUser.value);

  const avatarContent = computed(() => {
    // return _sessionUser.value.email.slice(0, 3);
    return 'ADM';
  });


  // Actions / Mutators (reg functions)
  const initializeAuth = async () => {
    // initialize the Auth Store on app/page mount 

    // if we have a token but not a _sessionUser
    if (_token.value && !isAuthenticated.value) {
      _isLoading.value = true // setting loading state
      _error.value = null

      try {
        
      }
    }
  };

  const setAuthData = (newToken, authResponseUser) => {
    _token.value = newToken; // sets new token
    _sessionUser.value = authResponseUser;

    if (newToken) {
      // Set new _token to local storage
      localStorage.setItem('jwt_token', newToken);
      console.log(isAuthenticated.value);
    }
    else {
      localStorage.removeItem('jwt_token');
    }
  };

  return {
    // State

    // Getters
    sessionUser,
    authToken,
    isLoading,
    error,
    isAuthenticated,
    avatarContent,

    // Actions
    setAuthData
  };
});