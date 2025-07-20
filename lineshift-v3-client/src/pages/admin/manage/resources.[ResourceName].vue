<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn
          color="primary-darken-1"
          variant="flat"
          :to="{name: '/admin/manage/resources'}"
        >
          <span class="text-primary-accent">Back</span>
        </v-btn>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-card
          color="secondary-darken-1"
        >
          <v-card-title class="text-h4">
            Current Sports
          </v-card-title>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        Manage Resource Page for {{ resourceName }}
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { useRoute } from 'vue-router';


const route = useRoute()
const resourceName = route.params.ResourceName


</script>

