<template>
  <v-app>
    <router-view />
  </v-app>

  <!-- Navigation -->
   <AdminNavigationLayout />
</template>

<script setup>
import AdminNavigationLayout from './layouts/AdminNavigationLayout.vue';

</script>
