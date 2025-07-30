<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card
          class="pa-3"
          rounded
          variant="outlined"
          color="secondary"
        >
          <v-card-title class="text-h5 text-uppercase text-white"> Welcome {{ user?.userName }} </v-card-title>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';
import { storeToRefs } from 'pinia';

const {user} = storeToRefs(useAuthStore())

</script>

<route lang="yaml">
  name: 'hub'
  meta:
    requiresAuth: true
</route>
