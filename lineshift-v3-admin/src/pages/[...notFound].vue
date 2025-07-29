<template>
  <v-card>
    <v-card-title>
      404 Page Not Found 
    </v-card-title>

    <v-card-text>
      This page you are looking for does not exist
    </v-card-text>

    <v-card-actions>
      <v-btn :to="{name: '/'}"
      >Hub</v-btn>
    </v-card-actions>
  </v-card>
</template>

<route lang="yaml">
  meta:
    requiresAuth: true
</route>