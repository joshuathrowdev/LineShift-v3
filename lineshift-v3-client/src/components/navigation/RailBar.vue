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

    <div v-if="hasPermissions">
      <v-divider color="primary" />
      <v-list-item
        class="py-5"
        rounded
        :to="{name: '/admin/hub'}"
      >
        <template #prepend>
          <v-icon
            icon="mdi-shield-account-variant-outline"
            color="secondary"
            size="35"
          />
        </template>
        <v-list-item-title
          class="font-weight-bold text-h6"
        >
          Admin Panel
        </v-list-item-title>
      </v-list-item>
    </div>

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

const { isAuthenticated, sessionUser, isAdmin, isModerator } = storeToRefs(useAuthStore())

const rail = ref(true)

const hasPermissions = computed(() => !!isAdmin.value || !!isModerator.value)

const isRailExpanded = ref(false)
</script>
