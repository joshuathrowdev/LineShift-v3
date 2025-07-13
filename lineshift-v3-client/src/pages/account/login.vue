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
</template>

<script setup>
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/auth.store';


const authStore = useAuthStore()
const {isAuthenticated} = storeToRefs(authStore)
const {login} = authStore

const router = useRouter()

const handleLoginAttempt = async (credentials) => {
  await login(credentials.value)
  if (isAuthenticated.value) {
    router.push({name: 'home'})
  }
}
</script>

<route lang="yaml">
name: 'login'
meta:
  requiresAuth: false
  redirectIfAuthenticated: true 
  # redirect authenticated users away from this route
</route>