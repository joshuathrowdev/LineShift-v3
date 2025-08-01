<template>
  <!-- Leagues Table -->
  <v-card>
    <v-card-text>
      <leagues-table/>
    </v-card-text>
  </v-card>

  s
</template>

<script setup>
import useLeaguesStore from '@/stores/leagues.store';


const {getAllLeagues} = useLeaguesStore()

onMounted(async () => {
  await getAllLeagues()
})
</script>

<route lang="yaml">
  meta:
    requiresAuth: true
</route>