<template>
  <v-app>
    <!-- Navigation -->
    <AdminNavigationLayout v-if="isAuthenticated" app />
    
    <v-main>
      <router-view />
    </v-main>

  </v-app>

</template>

<script setup>
import AdminNavigationLayout from "./layouts/AdminNavigationLayout.vue";
import { useAuthStore } from "./stores/auth.store";


const {isAuthenticated} = storeToRefs(useAuthStore())

</script>
