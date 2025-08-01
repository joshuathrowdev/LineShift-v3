<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card
          variant="flat"
          color="secondary"
        >
          <v-card-title class="text-h4">
            Manage Resources
          </v-card-title>
        </v-card>
      </v-col>
    </v-row>

    <v-row class="d-flex flex-row justify-space-around">
      <v-col cols="4">
        <v-card :to="{name: '/manage/resources.[keyword]', params: {'keyword':'sports'}}"
        >
          <v-card-title>Sports</v-card-title>
        </v-card>
      </v-col>

      <v-col cols="4">
        <v-card :to="{name: '/manage/resources.[keyword]', params: {'keyword': 'leagues'}}">
          <v-card-title>League</v-card-title>
        </v-card>
      </v-col>

      <v-col cols="4">
        <v-card>
          <v-card-title>Teams</v-card-title>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import router from '@/router';

console.log(router.getRoutes())
</script>

<route lang="yaml">
  name: 'resources'
  meta:
   requiresAuth: true
</route>