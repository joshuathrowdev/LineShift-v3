<template>
  <v-app-bar
    :elevation="2"
    color="primary"
    density="compact"
  >
    <template #prepend>
      <v-app-bar-nav-icon
        :icon="navMenuIcon"
        @click="navMenu = !navMenu"
      />
    </template>

    <v-app-bar-title class="text-h5">
      Welcome <span class="font-italic">{{ sessionUser.userName }}</span>
    </v-app-bar-title>
  </v-app-bar>
</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';

const {sessionUser} = storeToRefs(useAuthStore())

const navMenu = ref(false)
const navMenuIcon = computed(() => {
  return navMenu.value === false ? 'mdi-menu-open' : 'mdi-menu-close'
})
</script>