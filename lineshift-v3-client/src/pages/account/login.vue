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

    <!-- Testing User Store -->
    <v-card>
      {{ authStore.sessionUser }}
    </v-card>

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
</template>

<script setup>
import { useAuth } from '@/composables/useAuth'

// de-packing Auth composable
const {performLogin} = useAuth()

const handleLoginAttempt = async (credentials) => {
  displayCredentials(credentials)
  // have to de-wrap the ref object to get the dictionary object
  await performLogin(credentials.value)
}

// Testing Auth Store 
import { useAuthStore } from '@/stores/auth.store'
const authStore = useAuthStore()
if (authStore.sessionUser) {
  for (const key in authStore.sessionUser.value) {
  if (authStore.sessionUser.value.hasOwnProperty(key)) {
    const value = authStore.sessionUser.value[key]
    console.log(`${key}: ${value}`)
  }
}
}


// Test Helpers
const displayCredentials = (user) => {
  for(const key in user.value) {
    if (user.value.hasOwnProperty(key)) {
      const value = user.value[key]
      console.log(`${key}: ${value}`)
    }
  }
}
</script>