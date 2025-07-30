<template>
  <v-navigation-drawer floating>
    <v-list>
      <admin-nav-link
        text="Hub"
        icon="mdi-hubspot"
        :to="{name: 'hub'}"/>

      <admin-nav-link-banner title="Manage" />

      <admin-nav-link text="Resources" icon="mdi-chart-bubble" />

      <admin-nav-link text="Users" icon="mdi-account-group" />

      <admin-nav-link text="Reports" icon="mdi-alert-outline" />

      <admin-nav-link-banner title="Message Boards" />
      <admin-nav-link text="General Chat" icon="mdi-chat" />
      <admin-nav-link
        v-for="role in user?.roles"
        :key="role"
        icon="mdi-chat-outline"
        :text="role"
      />

      <v-divider color="primary" :thickness="6" />

      <admin-nav-link
        text="Back To Lineshift"
        icon="mdi-arrow-left-circle"
        :href="clientDomain"
        target="blank"
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
import { useAuthStore } from "@/stores/auth.store";
import { useRouter } from "vue-router";
import { storeToRefs } from "pinia";

const authStore = useAuthStore();
const { user } = storeToRefs(authStore);
const { logout } = authStore;

const router = useRouter();
const handleLogout = async () => {
  await logout();
  console.log(router.getRoutes())
  router.push({ name: "login" });
};

const clientDomain = computed(() => import.meta.env.VITE_REG_CLIENT_DOMAIN)
console.log(clientDomain.value)
</script>
