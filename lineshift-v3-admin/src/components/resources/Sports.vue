<template>

  <!-- Sports Table -->
  <v-container>
    <v-row>
      <v-col>
        <sports-table />
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-btn>Create a New Sport</v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import SportsTable from './tables/SportsTable.vue';
import SportForm from './forms/SportForm.vue';

</script>