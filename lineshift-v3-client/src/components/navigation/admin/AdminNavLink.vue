<template>
  <v-list-item :prepend-icon="icon">
    <span class="font-weight-bold text-h6">{{ text }}</span>
  </v-list-item>
</template>

<script setup>
const props = defineProps(['text', 'icon'])
const {text, icon} = props
</script>