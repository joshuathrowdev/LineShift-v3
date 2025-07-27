<template>
  <v-app>
    <router-view />
  </v-app>
</template>

<script setup>
  import sportsAdminApi from './services/sports.admin.api';

  const sports = await sportsAdminApi.getAllSports()
</script>
