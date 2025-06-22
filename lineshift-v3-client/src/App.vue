<template>
  <v-app>
    <!-- Any top level content that we want to persist between pages -->
     <NavigationLayout/>

    <v-main> 
      <router-view /> <!-- the content of the page we are on -->
    </v-main>
  </v-app>
</template>

<script setup>
  import NavigationLayout from './layouts/navigationLayout.vue';
  
  // Weather API Test
  import { ref } from 'vue';
  import weatherApi from './services/weather.api';
  
  const weatherData = ref(weatherApi.get('/WeatherForecast'))
  console.log(weatherData.value)
  
</script>
