<template>
  <v-spacer/>

  <v-container class="min-h">
    {{ user }}<br/></br><br/></br><br/></br>
    {{ token }}<br/></br>

    <v-row class="">
      <v-col cols="12">
        <login-form @login-attempt="handleLoginAttempt"/>
      </v-col>
    </v-row>
  </v-container>

  <v-spacer />
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
    router.push({name: '/'})
  }
}
</script>
