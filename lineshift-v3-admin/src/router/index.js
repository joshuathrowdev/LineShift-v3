/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from "vue-router/auto";
import { setupLayouts } from "virtual:generated-layouts";
import { routes } from "vue-router/auto-routes";
import { useAuthStore } from "@/stores/auth.store";
import { storeToRefs } from "pinia";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: setupLayouts(routes),
});

// Workaround for https://github.com/vitejs/vite/issues/11804
router.onError((err, to) => {
  if (err?.message?.includes?.("Failed to fetch dynamically imported module")) {
    if (localStorage.getItem("vuetify:dynamic-reload")) {
      console.error("Dynamic import error, reloading page did not fix it", err);
    } else {
      console.log("Reloading page to fix dynamic import error");
      localStorage.setItem("vuetify:dynamic-reload", "true");
      location.assign(to.fullPath);
    }
  } else {
    console.error(err);
  }
});

router.isReady().then(() => {
  localStorage.removeItem("vuetify:dynamic-reload");
});

router.beforeEach(async (to, from, next) => {
  const { token, isAuthReady, isAuthenticated, user, isLoading } =
    storeToRefs(useAuthStore());
  const { initializeAuth } = useAuthStore();

  if (token.value && !isAuthReady.value && !isLoading.value) {
    await initializeAuth();
  }

  if (token.value && !isAuthReady.value && isLoading.value) {
    return new Promise((resolve) => setTimeout(resolve, 100));
  }

  // Route Metas
  const requiresAuth = to.meta.requiresAuth;
  const redirectIfAuth = to.meta.redirectIfAuth;

  if (requiresAuth && !isAuthenticated.value) {
    next({ name: "login" });
  } else if (redirectIfAuth && isAuthenticated.value) {
    next({ name: "hub" });
  } else {
    next();
  }
});

export default router;
