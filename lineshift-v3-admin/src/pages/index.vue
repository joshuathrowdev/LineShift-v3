<template>
  This is the index/home page+ page
</template>

<script setup>
  import sportsAdminApi from '../services/sports.admin.api';
  import { ref } from 'vue';

  const sports = ref([])

  onMounted(async() => {
    sports.value = await sportsAdminApi.getAllSports()
    console.log(sports.value)
  })
</script>
