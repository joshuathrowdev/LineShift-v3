<template>
  <v-navigation-drawer
    v-if="isAuthenticated"
    :rail="rail"
    rail-width="75"
    expand-on-hover
    permanent
    @mouseenter="isRailExpanded = true"
    @mouseleave="isRailExpanded = false"
  >
    <v-list>
      <div
        class="d-flex justify-center"
      >
        <account-avatar v-if="!isRailExpanded" />
      </div>

      <account-section v-if="isRailExpanded" />
    </v-list>

    <v-divider />
    
    <v-list
      class="d-flex flex-column justify-center"
    >
      <nav-links />
    </v-list>

    <template #append>
      <v-list-item>
        Logo
      </v-list-item>
    </template>
  </v-navigation-drawer>
</template>

<script setup>
import { ref } from 'vue';
import { useAuthStore } from '@/stores/auth.store';

const { isAuthenticated } = storeToRefs(useAuthStore())

const rail = ref(true)

const isRailExpanded = ref(false)
</script>
