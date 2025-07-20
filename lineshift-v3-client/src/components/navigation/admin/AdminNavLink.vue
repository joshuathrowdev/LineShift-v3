<template>
  <v-list-item :prepend-icon="icon">
    {{ text }} 
  </v-list-item>
</template>

<script setup>
const props = defineProps(['text', 'icon'])
const {text, icon} = props
console.log(props)
</script>