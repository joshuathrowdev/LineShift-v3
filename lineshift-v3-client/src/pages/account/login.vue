<template>
  This is the Form Page
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
      <v-col cols="6">
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
          />

          <div class="d-flex justify-center">
            <v-btn
              append-icon="mdi-login"
              color="secondary"
              variant="tonal"
            >Login</v-btn>
          </div>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>

const username = ref('')
const email = ref('')
const password = ref('')

const handleLoginForm = () => {
  console.log("FORMMMM")
}
</script>