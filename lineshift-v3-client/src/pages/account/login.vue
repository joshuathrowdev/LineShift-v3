<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-card
          color="primary"
          elevation="2"
          rounded
          class="pa-2"
          variant="tonal"
        >
          <v-card-title
            class="text-center text-h4 font-weight-bold"
          >
            Welcome to Lineshift
          </v-card-title>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <login-form />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <register-card />
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>


</script>