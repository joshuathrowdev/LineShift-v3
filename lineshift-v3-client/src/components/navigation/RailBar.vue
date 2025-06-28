<template>
  <v-navigation-drawer
    :rail="rail"
    rail-width="75"
    expand-on-hover
    permanent
    @mouseenter="isRailExpanded = true"
    @mouseleave="isRailExpanded = false"
  >
    <v-list>
      <div class="d-flex justify-center">
        <account-avatar v-if="!isRailExpanded" />
      </div>

      <account-section v-if="isRailExpanded" />
    </v-list>

    <v-divider />
    
    <v-list>
      <div
        class="py-10"
      >
        <v-list 
          density="compact"
        >
          <nav-links />
        </v-list>
      </div>
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


const rail = ref(true)

const isRailExpanded = ref(false)
</script>



<!-- <v-list>
      <v-list-item>
        <template #prepend>
          <v-avatar
            color="ternary"
            size="45"
          >
            ADM
          </v-avatar>
        </template>

        <template #title>
          <v-chip
            color="primary"
            variant="flat"
          > 
            <span class="font-weight-bold">admin (username)</span>
          </v-chip>
        </template>

        <template #subtitle> 
          admin@lineshift.com
        </template>
      </v-list-item>
    </v-list> -->