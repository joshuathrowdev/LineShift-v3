<template>
  <v-form
    ref="form"
    @submit.prevent="handleLoginForm"
  >
    <v-text-field
      v-model="user.username"
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
      v-model="user.email"
      label="Email"
      variant="outlined"
      color="secondary"
      prepend-inner-icon="mdi-email-outline"
      clearable
      icon-color="primary"
      :rules="emailRules"
      glow
      required
      rounded
      type="email"
      autocomplete="email"
    />
        
    <v-text-field 
      v-model="user.password"
      label="Password"
      variant="solo"
      prepend-inner-icon="mdi-onepassword"
      clearable
      required
      color="secondary"
      :rules="passwordRules"
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
        type="submit"
      >
        Login
      </v-btn>
    </div>
  </v-form>
  {{ user }}
</template>

<script setup>
const emit = defineEmits(['loginAttempt'])

const form = ref(null)

const user = ref({
  username: '',
  email: '',
  password: '',
})

const handleLoginForm = async () => {
  const {valid} = await form.value.validate()

  if(valid) {
    emit('loginAttempt', user)
  }
  resetLoginUtils()
}

const resetLoginUtils = () => {
  for (const key in user.value) {
    if (user.value.hasOwnProperty(key)) {
      user.value[key] = '' 
    }
  }
}

// Rules
const emailRules = [
  v => !!v || 'E-mail is required',
  v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
];

const passwordRules = [
  v => !!v || 'Password is required',
  // Add more later
];



// Test Helpers
const displayUser = () => {
  for(const key in user.value) {
    if (user.value.hasOwnProperty(key)) {
      const value = user.value[key]
      console.log(`${key}: ${value}`)
    }
  }
}
</script>