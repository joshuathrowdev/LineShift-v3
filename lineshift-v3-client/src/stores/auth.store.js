import { defineStore } from 'pinia';
import { ref, computed } from 'vue';


export const useAuthStore = defineStore('auth', () => {
  // State
  const sessionUser = ref(null);
  // Initialize token from localStorage when store is is first created
  // if a token already exist, it grabs that one
  const token = ref(localStorage.getItem('jwt_token') || null);


  // Getters (computed functions)
  const isAuthenticated = computed(() => !!token.value && !!sessionUser.value);
  const authToken = computed(() => token.value);

  const avatarContent = computed(() => {
    // return sessionUser.value.email.slice(0, 3);
    return 'ADM';
  });


  // Actions / Mutators (reg functions)
  const setAuthData = (newToken, authResponseUser) => {
    token.value = newToken; // sets new token
    sessionUser.value = authResponseUser;

    if (newToken) {
      // Set new token to local storage
      localStorage.setItem('jwt_token', newToken);
      console.log(isAuthenticated.value)
    }
    else {
      localStorage.removeItem('jwt_token');
    }
  };

  return {
    // State
    sessionUser,

    // Getters
    avatarContent,
    isAuthenticated,
    authToken,

    // Actions
    setAuthData
  };
});