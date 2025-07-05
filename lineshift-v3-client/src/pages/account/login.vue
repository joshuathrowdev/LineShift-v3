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
        <login-form @login-attempt="handleLoginAttempt" />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <register-card />
      </v-col>
    </v-row>
  </v-container>

  <!-- Testing Session User and Auth Store -->
  <v-card>
    <v-card-title>
      Testing For User Assignment and Data Mapping
    </v-card-title>

  
    <v-card>
      <v-card-title>Raw Session User from Response</v-card-title>
      
      <v-card-text>
        Session User: {{ authStore.sessionUser }}
        <br><br><br>

        Auth Token: {{ authStore.token }}
        <br><br><br>

        Is Authenticated: {{ authStore.isAuthenticated }}
        <br><br><br>

        Ready For Initialization: {{ authStore.token && !authStore.isAuthenticated }}
        <br><br><br>

        Loading: {{ authStore.isLoading }}
        <br><br><br>

        Error: {{ authStore.error }}
        <br><br>
      </v-card-text>
    </v-card>
  </v-card>
</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';


const {login} = useAuthStore()
const authStore = useAuthStore()

const handleLoginAttempt = async (credentials) => {
  await login(credentials.value)
}
</script>