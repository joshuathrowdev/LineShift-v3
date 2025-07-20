<template>
  <v-card
    variant="flat"
    color="secondary"
  >
    <v-card-title>{{ title }}</v-card-title>
  </v-card>
</template>

<script setup>
  const props = defineProps(['title'])
  const {title} = props
</script>