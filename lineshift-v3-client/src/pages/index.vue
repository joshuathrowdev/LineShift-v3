<template>
  <home-layout />
</template>

<script setup>
  import HomeLayout from '@/layouts/HomeLayout.vue';
</script>

<!-- This is the unplugin-vue-router specific route block -->
<route lang="yaml">
name: 'home'
meta:
  requiresAuth: true
  # redirect un-authenticated users to requires Auth route
</route>
