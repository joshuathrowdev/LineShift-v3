<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card
          variant="flat"
          color="secondary-lighten-1"
        >
          <v-card-title class="text-h4">
            Manage Resources
          </v-card-title>
        </v-card>
      </v-col>
    </v-row>

    <v-row class="d-flex flex-row justify-space-around">
      <v-col cols="4">
        <v-card
          :to="{name: '/admin/manage/resources.[ResourceName]', params: {'ResourceName': 'sports'}}"
        >
          <v-card-title>Sports</v-card-title>
        </v-card>
      </v-col>

      <v-col cols="4">
        <v-card>
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
