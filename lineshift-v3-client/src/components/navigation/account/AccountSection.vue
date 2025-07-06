<template>
  <v-list-item> 
    <div
      class="d-flex justify-center"
    >
      <v-btn 
        icon
        variant="text"      
        :disabled="!isLoggedIn"
        @click="handleShowAccountInformation"
      >
        <account-avatar />
      </v-btn>
    </div>

    <v-card
      v-if="showAccountInformation"
    >
      <template #title>
        <span class="font-weight-bold text-primary">{{ sessionUser.userName }}</span>
      </template>

      <template #subtitle>
        {{ sessionUser.email }}
      </template>

      <template #text> 
        <account-information-chip
          title="Joined"
          :information="formattedRegisteredDate"
        />

        <account-information-chip 
          title="Role"
          :information="sessionUser.roles"
        />

        <account-information-chip 
          title="Level"
        >
          <template #membership>
            <v-chip
              color="blue-lighten-2"
              class="mx-2"
              :text="sessionUser.subscriptionTier"
            />
          </template>
        </account-information-chip>

        <div
          class="d-flex justify-center mt-4"
        >
          <v-btn
            density="compact"
            rounded
            variant="flat"
            color="red-darken-2"
          >
            Logout
          </v-btn>
        </div>
      </template>
    </v-card>
  </v-list-item>
</template>

<script setup>
import { ref } from 'vue';
import { useAuth } from '@/composables/useAuth';


const {sessionUser, isLoggedIn, formattedRegisteredDate} = useAuth()

const showAccountInformation = ref(false)


const handleShowAccountInformation = () => {
  showAccountInformation.value === false ? showAccountInformation.value = true : showAccountInformation.value = false
}
</script>