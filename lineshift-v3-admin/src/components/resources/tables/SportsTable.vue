<template>
  <v-data-table
    :items="sports"
    :search="tableSearch"
    :headers="headers"
    class="pa-4"
  > 
    <template #top>
      <v-text-field 
        v-model="tableSearch"
        variant="outlined"
        label="Search"
        color="primary-accent"
        prepend-inner-icon="mdi-card-search-outline"
        class="ml-3"
      />
    </template>
  </v-data-table>
</template>

<script setup>
  import useSportsStore from '@/stores/sports.store';
  import { storeToRefs } from 'pinia';

 
  const {sports} = storeToRefs(useSportsStore())
  
  const tableSearch = ref('')
  const headers = [
    {title: 'Sport Id', value: 'sportId', sortable: true},
    {title: 'Name', value: 'sportName', sortable: true},
    {title: 'Type', value: 'type'},
    {title: 'Description', value: 'description'},
  ]
</script>