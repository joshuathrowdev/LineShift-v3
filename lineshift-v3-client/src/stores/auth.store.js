import { defineStore } from 'pinia';
import { ref } from 'vue';


export const useAuthStore = defineStore('auth', () => {
  // State
  const sessionUser = ref(null);


  // Getters (computed functions)


  // Actions / Mutators (reg functions)
  const setSessionUser = (responseUser) => {
    if (responseUser) {
      sessionUser.value = responseUser;
    }
  };

  return {
    // State
    sessionUser,

    // Getters

    // Actions
    setSessionUser
  };
});