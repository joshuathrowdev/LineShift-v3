<template>
  <v-avatar
    color="primary"
    size="45"
    variant="flat"
    icon="mdi-account-outline"
  />
</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';

const {avatarContent} = storeToRefs(useAuthStore())
</script>