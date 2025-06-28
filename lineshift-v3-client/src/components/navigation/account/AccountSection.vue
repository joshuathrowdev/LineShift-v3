<template>
  <v-list-item> 
    <div
      class="d-flex justify-center"
    >
      <account-avatar />
    </div>
    <v-card>
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

</script>