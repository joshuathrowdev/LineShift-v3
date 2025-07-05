import { useAuthStore } from '@/stores/auth.store';

// making instance of Auth Store
const authStore = useAuthStore();

export const useAuth = () => {

  // Composable acts as a thin wrapper for store that allows components to more
  // easily consume the store while still maintaining the reactivity

  return {};
};