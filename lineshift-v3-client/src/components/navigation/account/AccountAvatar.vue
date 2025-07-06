<template>
  <v-avatar
    color="ternary"
    size="45"
  >
    {{ avatarContent }}
  </v-avatar>
</template>

<script setup>
import { useAuth } from '@/composables/useAuth';

const {avatarContent} = useAuth()
</script>