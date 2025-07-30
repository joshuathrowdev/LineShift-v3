<template>
  <v-card
    variant="flat"
    class="my-3"
    color="secondary"
  >
    <v-card-title>{{ title }}</v-card-title>
  </v-card>
</template>

<script setup>
const props = defineProps(["title"]);
const { title } = props;
</script>
