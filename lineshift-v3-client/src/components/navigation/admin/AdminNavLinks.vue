<template>
  <v-navigation-drawer>
    <v-list>
      <admin-nav-link-banner title="Manage" />
      <admin-nav-link
        text="Resources"
        icon="mdi-chart-bubble"
        :to="{name: '/admin/manage/resources'}"
      />

      <admin-nav-link 
        text="Users"
        icon="mdi-account-group"
      />

      <admin-nav-link 
        text="Reports"
        icon="mdi-alert-outline"
      />

      <admin-nav-link-banner title="Message Boards" />
      <admin-nav-link
        text="General Chat"
        icon="mdi-chat"
      />
      <admin-nav-link
        v-for="role in sessionUser.roles"
        :key="role"
        icon="mdi-chat-outline"
        :text="role"
      />

      <v-divider color="primary" />
      <admin-nav-link 
        text="Back To Hub"
        icon="mdi-hubspot"
        :to="{name: '/admin/hub'}"
      />

      <admin-nav-link 
        text="Back To Lineshift"
        icon="mdi-arrow-left-circle"
        :to="{name: 'home'}"
      />
    </v-list>

    <template #append>
      <v-btn
        color="error"
        class="w-100"
        variant="flat"
        prepend-icon="mdi-logout"
        @click="handleLogout"
      >
        Logout
      </v-btn>
    </template>
  </v-navigation-drawer>
</template>

<script setup>
import { useAuthStore } from '@/stores/auth.store';
import { useRouter } from 'vue-router';

const authStore = useAuthStore()
const {sessionUser} = storeToRefs(authStore)
const {logout} = authStore

const router = useRouter()
const handleLogout = async () => {
  await logout()
  router.push({name: 'login'})
}
</script>