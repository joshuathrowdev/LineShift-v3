<template>
  <v-container>
    <v-row>
      <v-col
        v-for="sport in sports"
        :key="sport.sportId"
        cols="4"
      >
        <!-- We have to bind the data of what we are trying to use -->
        <!-- We cannot directly access the index value when using v-component -->
        <!-- default slots / html attributes -->
        <v-card
          :title="sport.sportName"
          :subtitle="sport.sportId"
        />
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
  import { useSportsStore } from '@/stores/sports.store';

  const sportsStore = useSportsStore()
  const {sports} = storeToRefs(sportsStore)
  const {getAllSports} = sportsStore

  onMounted(async () => {
    await getAllSports()
  })
</script>

<route lang="yaml">
meta:
  requiresAuth: true
</route>
