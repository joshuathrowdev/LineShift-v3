<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn
          color="primary-darken-1"
          variant="flat"
          prepend-icon="mdi-arrow-left-thin"
          text="Back"
          :to="{name: 'resources'}"
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-card color="secondary"
        >
          <v-card-title class="text-h4 text-uppercase">
            {{ formattedResourceKeyword }}
          </v-card-title>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <component :is="resourceComponent" :key="keyword"/>
      </v-col>
    </v-row>

    
  </v-container>
</template>

<script setup>
import { useRoute } from 'vue-router';
import { useResourceStore } from '@/stores/resources.store';
import { storeToRefs } from 'pinia';


const {resourceComponent, resourceKeyword, formattedResourceKeyword} = storeToRefs(useResourceStore())
const {loadResourceComponent} = useResourceStore()

const route = useRoute()
const keyword = route.params.keyword


onMounted(async () => {
  resourceKeyword.value = keyword
  await loadResourceComponent()
})
</script>


<route lang="yaml">
  meta:
    requiredAuth: true
    requiresAdmin: true
</route>
