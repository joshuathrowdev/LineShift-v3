<template>
  <v-data-table
    :items="governingBodies"
    :headers="headers"
    :search="tableSearch"
    class="pa-4"
  >
    <template #top>
      <resource-table-search v-model="tableSearch" />
    </template>
  </v-data-table>
</template>

<script setup>
import useGoverningBodiesStore from '@/stores/resources/governingBodies.store';
import { storeToRefs } from 'pinia';

const {governingBodies} = storeToRefs(useGoverningBodiesStore())

const headers = [
  {title: 'Governing Body Id', value: 'governingBodyId', sortable: true},
  {title: 'Name', value: 'governingBodyName', sortable: true},
  {title: 'Abbreviation', value: 'abbreviation', sortable: true},
  {title: 'Description', value: 'description', sortable: false},
  {title: 'Country', value: 'countryOfOrigin', sortable: true},
  {title: 'Date Founded', value: 'dateFounded', sortable: false}
]

const tableSearch = ref('')
</script>