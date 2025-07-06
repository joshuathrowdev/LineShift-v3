<template>
  <div class="pb-2">
    <v-chip
      color="ternary"
      variant="tonal"
    >
      {{ title }}
    </v-chip>
    <span
      v-if="information"
      class="ml-2"
    >{{ information }}</span>

    <slot name="membership" />
  </div>
</template>


<script setup>
const props = defineProps(['title', 'information'])

const {title, information} = props
</script>