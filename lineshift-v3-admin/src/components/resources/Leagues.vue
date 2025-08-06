<template>
  <!-- Leagues Table -->
  <v-card>
    <v-card-text>
      <leagues-table/>
    </v-card-text>
  </v-card>

  <div class="d-flex justify-end w-100 mt-4">
    <create-new-resource-button 
      single-noun-resource-key="League"
      :resource-post-form="LeagueForm"
    />
  </div>
</template>

<script setup>
import useLeaguesStore from '@/stores/resources/leagues.store'
import LeagueForm from './forms/LeagueForm.vue'
import useSportsStore from '@/stores/resources/sports.store'
import useGoverningBodiesStore from '@/stores/resources/governingBodies.store'

const {getAllLeagues} = useLeaguesStore()
const {getAllSports} = useSportsStore()
const {getAllGoverningBodies} = useGoverningBodiesStore()

onMounted(async () => {
  await getAllLeagues()
  await getAllSports()
  await getAllGoverningBodies()
})
</script>

<route lang="yaml">
  meta:
    requiresAuth: true
</route>