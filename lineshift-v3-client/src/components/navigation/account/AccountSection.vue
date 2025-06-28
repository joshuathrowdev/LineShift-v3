<template>
  <v-list-item> 
    <div
      class="d-flex justify-center"
    >
      <v-btn 
        icon
        variant="text"      
        @click="handleShowAccountInformation"
      >
        <account-avatar />
      </v-btn>
    </div>
    <v-card
      v-if="showAccountInformation"
    >
      <template #prepend />
      <template #title>
        Admin (username)
      </template>
      <template #subtitle>
        admin@lineshift.com
      </template>

      <template #text> 
        <div class="pb-2">
          <v-chip
            color="ternary"
            variant="tonal"
          >
            Joined
          </v-chip>
          June 28, 2025
        </div>

        <div class="pb-2">
          <v-chip
            color="ternary"
            variant="tonal"
          >
            Role
          </v-chip>
          ADMIN
        </div>

        <div class="pb-2">
          <v-chip
            color="ternary"
            variant="tonal"
          >
            Level
          </v-chip>
          <v-chip
            color="blue-lighten-2"
            class="mx-2"
            text="PLT"
          />
        </div>
      </template>
    </v-card>
  </v-list-item>
</template>

<script setup>
import { ref } from 'vue';

const showAccountInformation = ref(false)


const handleShowAccountInformation = () => {
  showAccountInformation.value === false ? showAccountInformation.value = true : showAccountInformation.value = false
}
</script>