<template>
  <v-form @submit.prevent="handleLoginForm">
    <v-text-field
      v-model="username"
      label="Username"
      variant="outlined"
      prepend-inner-icon="mdi-account-star-outline"
      color="secondary"
      clearable
      icon-color="primary"
      glow
      rounded
      autocomplete="username"
    />

    <v-divider>
      OR
    </v-divider>

    <v-text-field 
      v-model="email"
      label="Email"
      variant="outlined"
      color="secondary"
      prepend-inner-icon="mdi-email-outline"
      clearable
      icon-color="primary"
      glow
      rounded
      type="email"
      autocomplete="email"
    />
        
    <v-text-field 
      v-model="password"
      label="Password"
      variant="solo"
      prepend-inner-icon="mdi-onepassword"
      clearable
      color="secondary"
      glow
      icon-color="primary"
      rounded
      type="password"
      autocomplete="current-password"
    />

    <div class="d-flex justify-center">
      <v-btn
        append-icon="mdi-login"
        color="secondary"
        variant="tonal"
      >
        Login
      </v-btn>
    </div>
  </v-form>
</template>

<script setup>

const username = ref('')
const email = ref('')
const password = ref('')

const handleLoginForm = () => {
  console.log("FORMMMM")
}
</script>