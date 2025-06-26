<template>
  <v-list-item
    class="py-5"
  >
    <template #prepend>
      <v-icon :icon="icon" />
    </template>
    <v-list-item-title
      class="font-weight-bold text-h6"
    >
      {{ title }}
    </v-list-item-title>
  </v-list-item>
</template>

<script setup>
  const props = defineProps(['title', 'icon'])
</script>