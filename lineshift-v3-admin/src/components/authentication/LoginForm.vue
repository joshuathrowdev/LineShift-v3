<template>
  <v-card
    class="pa-10"
    rounded
    variant="flat"
  >
    <v-form v-model="isFormValid" > 
      <v-card-text>
        <v-text-field
          v-model="credentials.email"
          label="U Email"
          color="secondary"
          prepend-inner-icon="mdi-email-outline"
          glow
          :rules="emailRules"
          type="email"
          rounded
          variant="underlined"
          clearable
        />

        <v-text-field
          v-model="credentials.password"
          label="Key"
          color="secondary"
          type="password"
          clearable
          :rules="passwordRules"
          autocomplete
          variant="underlined"
          prepend-inner-icon="mdi-onepassword"
        />
        {{ credentials }}
      </v-card-text>

      <v-card-actions class="d-flex flex-row justify-space-around">
        <v-btn
          color="primary-darken-1"
          variant="flat"
          class="px-5"
          :disabled="!isFormValid"
          type="submit"
          prepend-icon="mdi-door-closed-lock"
          @click="handleLoginAttempt"><span class="text-primary-accent">Login</span></v-btn>
      </v-card-actions>
    </v-form>
  </v-card>
</template>

<script setup>

const emits = defineEmits(['login-attempt'])

const credentials = ref({
  email: null,
  password: null,
})

const isFormValid = ref(null)

const handleLoginAttempt = async () => {

  if (isFormValid.value) {
    emits('login-attempt', credentials.value)
    resetForm()
  }
}


const resetForm = () => {
  credentials.value.email = null
  credentials.value.password = null
}

const emailRules = [
  v => !!v || "U Email is required",
  v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
  v => !!v && v.length < 50 || "U email must be less than 75 characters"
]

const passwordRules = [
  v => !!v || "Passkey is required",
  v => (v && v.length >= 6) || 'Passkey must be at least 6 characters',
  v => !!v && v.length < 50 || "Passkey must be less than 50 characters"
]
</script>
