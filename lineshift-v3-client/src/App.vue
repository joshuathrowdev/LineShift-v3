<template>
  <v-app>
    <NavigationLayout />

    <v-main> 
      <router-view /> <!-- the content of the page we are on -->

      <snackbar />
    </v-main>
  </v-app>
</template>

<script setup>
  import NavigationLayout from './layouts/NavigationLayout.vue';

  import { useRoute } from 'vue-router';

  const route = useRoute()
  const isAdminView = computed(() => route.path.includes('admin'))
</script>
