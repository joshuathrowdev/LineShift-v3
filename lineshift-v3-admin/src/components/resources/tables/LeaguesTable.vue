<template>
  <v-data-table
    :items="leagues"
    :headers="headers"
    :search="tableSearch"
  > 
    <template #top>
      <resource-table-search v-model="tableSearch" />
    </template>
  </v-data-table>
</template>

<script setup>
import useLeaguesStore from '@/stores/resources/leagues.store';
import { storeToRefs } from 'pinia';

const {leagues} = storeToRefs(useLeaguesStore())
const headers = [
  {title: 'League Id', value: 'leagueId', sortable: true},
  {title: 'Name', value: 'leagueName', sortable: true},
  {title: 'Level', value: 'level', sortable: false},
  {title: 'Gender', value: 'gender', sortable: false},
  {title: 'Governing Body', value: 'governingBodyId', sortable: false},
]

const tableSearch = ref('')
</script>