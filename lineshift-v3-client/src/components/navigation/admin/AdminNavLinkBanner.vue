<template>
  <v-card
    variant="flat"
    color="secondary-lighten-1"
    class="my-3"
  >
    <v-card-title>{{ title }}</v-card-title>
  </v-card>
</template>

<script setup>
  const props = defineProps(['title'])
  const {title} = props
</script>