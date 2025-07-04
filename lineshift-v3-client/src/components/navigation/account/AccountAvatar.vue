<template>
  <v-avatar
    color="ternary"
    size="45"
  >
    {{ avatarContent }}
  </v-avatar>
</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';

const authStore = useAuthStore()

const {avatarContent} = authStore
</script>