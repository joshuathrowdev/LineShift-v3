<template>


  <v-container class="">
    <v-row class="h-100">
      <v-col cols="12">
        <login-form @login-attempt="handleLoginAttempt"/>
      </v-col>
    </v-row>
  </v-container>

</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';
import { storeToRefs } from 'pinia';
import { useRouter } from 'vue-router';

const authStore = useAuthStore()
const {user, token, isAuthenticated} = storeToRefs(authStore)
const {login} = authStore

const router = useRouter()
const handleLoginAttempt = async (credentials) => {
  await login(credentials)

  if(isAuthenticated.value) {
    router.push({name: 'hub'})
  }
}
</script>

<route lang="yaml">
  name: 'login'
  meta:
    requiresAuth: false
    redirectIfAuth: true
</route>
