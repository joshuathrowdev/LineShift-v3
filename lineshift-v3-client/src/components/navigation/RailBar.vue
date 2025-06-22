<template>
  <v-navigation-drawer
    rail
    rail-width="75"
    expand-on-hover
    permanent
  >
    <template #prepend>
      <nav-logo />
    </template>

    
    <template #default>
      <v-divider
        thickness="4"
        color="primary"
      />
      <div
        class="py-10"
      >
        <nav-links />
      </div>
    </template>

    <template #append>
      <account-section />
    </template>
  </v-navigation-drawer>
</template>