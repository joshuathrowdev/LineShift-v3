<template>
  <v-form
    ref="form"
    v-model="isFormValid"
    @submit.prevent="handleLoginForm"
  >
    <v-text-field
      label="Username"
      variant="outlined"
      prepend-inner-icon="mdi-account-star-outline"
      color="secondary"
      clearable
      disabled
      icon-color="primary"
      glow
      rounded
      autocomplete="username"
    />

    <v-divider>
      OR
    </v-divider>

    <v-text-field 
      v-model="credentials.email"
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
      v-model="credentials.password"
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
        :disabled="!isFormValid"
      >
        Login
      </v-btn>
    </div>
  </v-form>
  {{ credentials }}
</template>

<script setup>
const emit = defineEmits(['loginAttempt'])

const form = ref(null)
const isFormValid = ref(null)

const credentials = ref({
  email: '',
  password: '',
})

const handleLoginForm = async () => {

  if(isFormValid.value) {
    emit('loginAttempt', credentials)
    resetLoginUtils()
  }
}

const resetLoginUtils = () => {
  for (const key in credentials.value) {
    if (credentials.value.hasOwnProperty(key)) {
      credentials.value[key] = '' 
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
  v => !!v && (v.length >= 6) || "Password must be 6 characters or more"
  // Add more later
];

</script>