<template>
  <v-layout>
    <v-app-bar :elevation="2" color="primary" density="compact">
      <template #prepend>
        <v-app-bar-nav-icon
          :icon="navMenuIcon"
          variant="text"
          @click="navMenu = !navMenu"
        />
      </template>

      <v-app-bar-title class="text-h5"> Welcome </v-app-bar-title>
    </v-app-bar>
    <admin-nav-links v-model="navMenu" />
  </v-layout>
</template>

<script setup>
const rest = 3;

const navMenu = ref(false);
const navMenuIcon = computed(() => {
  return navMenu.value === false
    ? "mdi-menu-right-outline"
    : "mdi-menu-left-outline";
});
</script>
