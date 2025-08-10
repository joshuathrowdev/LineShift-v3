<template>
  <v-app>
    <!-- Navigation -->
    <AdminNavigationLayout v-if="isAuthenticated" />
    
    <v-main>
      <router-view />
    </v-main>

    <!-- Snackbar  -->
    <snackbar />
  </v-app>

</template>

<script setup>
import AdminNavigationLayout from "./layouts/AdminNavigationLayout.vue";
import { useAuthStore } from "./stores/auth.store";
import { storeToRefs } from "pinia";


const {isAuthenticated} = storeToRefs(useAuthStore())

</script>
