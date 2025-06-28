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
      <v-list-item
        v-if="!isRailExpanded"
      >
        <template #prepend>
          <v-avatar
            color="ternary"
            size="45"
          >
            ADM
          </v-avatar>
        </template>
      </v-list-item>

      <v-list-item
        v-if="isRailExpanded"
      > 
        <div
          class="d-flex justify-center"
        >
          <v-avatar
            color="ternary"
            size="45"
          >
            ADM
          </v-avatar>
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
              <v-chip color="blue-lighten-2" class="mx-2" text="PLT"/>
            </div>
          </template>
        </v-card>
      </v-list-item>
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