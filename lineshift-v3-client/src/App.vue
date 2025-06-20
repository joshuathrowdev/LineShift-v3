<template>
  <v-app>
    <router-view />
  </v-app>
</template>

<script setup>
  // Weather API Test
  import { ref } from 'vue';
  import weatherApi from './services/weather.api';
  
  const weatherData = ref(weatherApi.get('/WeatherForecast'))
  console.log(weatherData.value)
  
</script>
