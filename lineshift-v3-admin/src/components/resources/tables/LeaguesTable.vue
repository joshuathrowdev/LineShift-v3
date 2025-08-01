<template>
  <v-data-table
    :items="leagues"
    :headers="headers"
  />
</template>

<script setup>
import useLeaguesStore from '@/stores/leagues.store';
import { storeToRefs } from 'pinia';

const {leagues} = storeToRefs(useLeaguesStore())
const headers = [
  {title: 'League Id', value: 'leagueId', sortable: true},
  {title: 'Name', value: 'leagueName', sortable: true},
  {title: 'Level', value: 'level', sortable: false},
  {title: 'Gender', value: 'gender', sortable: false},
  {title: 'Governing Body', value: 'governingBodyId', sortable: false},
]
</script>