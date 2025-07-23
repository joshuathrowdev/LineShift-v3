<template>
  <v-app>
    <!-- Any top level content that we want to persist between pages -->
    <AdminNavigationLayout v-if="isAdminView" />
    <NavigationLayout v-else />

    <v-main> 
      <router-view /> <!-- the content of the page we are on -->

      <snackbar />
    </v-main>
  </v-app>
</template>

<script setup>
  import AdminNavigationLayout from './layouts/admin/AdminNavigationLayout.vue';
  import NavigationLayout from './layouts/NavigationLayout.vue';

  import { useRoute } from 'vue-router';

  const route = useRoute()
  const isAdminView = computed(() => route.path.includes('admin'))
</script>
