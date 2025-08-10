<template>

  <v-app-bar
    v-if="isAuthenticated"
    :elevation="2" 
    color="secondary"
    density="compact">
    <template #prepend>
      <v-app-bar-nav-icon
        :icon="navMenuIcon"
        variant="text"
        @click="navMenu = !navMenu"
      />
    </template>
  </v-app-bar>
  
  <admin-nav-links v-model="navMenu" />
</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';
import { storeToRefs } from 'pinia';

const {user, isAuthenticated} = storeToRefs(useAuthStore())

const navMenu = ref(false);
const navMenuIcon = computed(() => {
  return navMenu.value === false
    ? "mdi-menu-right-outline"
    : "mdi-menu-left-outline";
});
</script>
