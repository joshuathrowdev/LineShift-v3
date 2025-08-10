<template>
  <!-- Governing Bodies Tables -->
  <v-card>
    <v-card-text>
      <governing-bodies-table />
    </v-card-text>
  </v-card>

  <div class="d-flex justify-end w-100 mt-4">
    <create-new-resource-button 
      single-noun-resource-key="Governing Body"
      :resource-post-form="GoverningBodyForm"
    />
  </div>
</template>

<script setup>
import useGoverningBodiesStore from '@/stores/resources/governingBodies.store';
import GoverningBodyForm from './forms/GoverningBodyForm.vue';

const {getAllGoverningBodies} = useGoverningBodiesStore()

onMounted(async () => {
  await getAllGoverningBodies()
})
</script>