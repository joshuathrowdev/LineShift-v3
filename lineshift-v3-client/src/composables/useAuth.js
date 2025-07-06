import { computed } from 'vue';
import { useAuthStore } from '@/stores/auth.store';


// making instance of Auth Store

export const useAuth = () => {
  // Composable acts as a thin wrapper for store that allows components to more
  // easily consume the store while still maintaining the reactivity
  const authStore = useAuthStore();

  // Wrapped Getters for State/Derived State
  const sessionUser = computed(() => authStore.sessionUser);
  const token = computed(() => authStore.token);
  const isLoggedIn = computed(() => authStore.isAuthenticated);
  const isLoading = computed(() => authStore.isLoading);
  const error = computed(() => authStore.error);

  // Composable-Specific Logic
  const avatarContent = computed(() => {
    if (!isLoggedIn.value) {
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


  // Exposed Actions
  const login = authStore.login;
  const logout = authStore.logout;


  return {
    // Public reactive state for components
    isLoggedIn,
    sessionUser,
    token,
    isLoading,
    error,
    avatarContent,
    formattedRegisteredDate,

    login,
    logout,

  };
};