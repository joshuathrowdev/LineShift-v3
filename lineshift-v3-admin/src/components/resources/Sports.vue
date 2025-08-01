<template>

  <!-- Sports Table -->
  <v-card variant="flat">
    <v-card-text>
      <sports-table />
    </v-card-text>
  </v-card>
  

  <div class="d-flex justify-end w-100 mt-4">
    <create-new-resource-button
      single-noun-resource-key="Sport"
      :resource-post-form="SportForm" />
  </div>
</template>

<script setup>
import SportForm from './forms/SportForm.vue'
import useSportsStore from '@/stores/resources/sports.store'

useSportsStore

const {getAllSports} = useSportsStore()


onMounted(async () => {
  await getAllSports()
})
</script>

<route lang="yaml">
  meta:
    requiresAuth: true
</route>