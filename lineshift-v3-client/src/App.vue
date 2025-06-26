<template>
  <v-app>
    <!-- Any top level content that we want to persist between pages -->
    <NavigationLayout />

    <v-main> 
      <router-view /> <!-- the content of the page we are on -->
    </v-main>
  </v-app>
</template>

<script setup>
  import NavigationLayout from './layouts/navigationLayout.vue';
  
  // Test for sports composable vertical
  import { useSports } from './composables/useSports';

  const {sports, getAllSports} = useSports()

  onMounted(async () => {
    getAllSports()
    console.log(sports.value)
  })

  
</script>
