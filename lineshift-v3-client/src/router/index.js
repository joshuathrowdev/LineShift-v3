/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router/auto';
import { setupLayouts } from 'virtual:generated-layouts';
import { routes } from 'vue-router/auto-routes';
import { useAuthStore } from '@/stores/auth.store';
import { storeToRefs } from 'pinia';
import { computed } from 'vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: setupLayouts(routes),
});

// Workaround for https://github.com/vitejs/vite/issues/11804
router.onError((err, to) => {
  if (err?.message?.includes?.('Failed to fetch dynamically imported module')) {
    if (localStorage.getItem('vuetify:dynamic-reload')) {
      console.error('Dynamic import error, reloading page did not fix it', err);
    } else {
      console.log('Reloading page to fix dynamic import error');
      localStorage.setItem('vuetify:dynamic-reload', 'true');
      location.assign(to.fullPath);
    }
  } else {
    console.error(err);
  }
});

router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload');
});


// --- GLOBAL NAVIGATION GUARD ---
router.beforeEach(async (to, from, next) => {
  //In Vue Router's navigation guards (beforeEach, beforeEnter, etc.), 
  // to is the target route being navigated to, \
  // from is the current route being navigated from
  // next is a function that resolves the hook and controls the navigation flow.

  const authStore = useAuthStore();
  const { token, isLoading, isAuthReady, isAuthenticated, sessionUser } = storeToRefs(authStore);
  const { initializeAuth } = authStore;


  // On initial load or refresh, ensure auth state is initialized
  // this prevents immediate redirect before token check completes
  if (token.value && !isAuthReady.value && !isLoading.value) {
    await initializeAuth();
  }


  // waiting until auth stats is definitely ready (via isAuthReady)
  if (token.value && !isAuthReady.value && isLoading.value) {
    // wait a tiny but for this condition to be evaluated
    await new Promise(resolve => setTimeout(resolve, 250));
  }


  // Route Meta props
  const requiresAuth = to.meta.requiresAuth;
  const requiresAdmin = to.meta.requiresAdmin;
  const isAdmin = computed(() => sessionUser.value?.roles.find(role => role === 'Admin'));
  const isModerator = computed(() => sessionUser.value?.roles.find(role => role === 'Moderator'));
  const redirectIfAuthenticated = to.meta.redirectIfAuthenticated;

  // Route Names
  const loginRouteName = 'login';
  const homeRouteName = 'home';


  // // --- DEBUGGING LOGS (Final Decision Factors) ---
  // console.groupCollapsed(`Navigating: ${from.path} -> ${to.path}`);
  // console.log('Target Route Name (to.name):', to.name);
  // console.log('Target Route Meta (to.meta):', to.meta);
  // console.log('Final isAuthenticated:', isAuthenticated.value);
  // console.log('Requires Auth:', requiresAuth);
  // console.log('Redirect If Authenticated:', redirectIfAuthenticated);
  // console.groupEnd();
  // // --- END DEBUGGING ---



  if (requiresAuth && !isAuthenticated.value) {
    next({ name: loginRouteName });
  }
  else if (redirectIfAuthenticated && isAuthenticated.value) {
    next({ name: homeRouteName });
  }
  else if (requiresAdmin && (!isAdmin.value && !isModerator.value)) {
    next({ name: homeRouteName });
  }
  else {
    next(); // all good, process to the route
  }

});

export default router;
