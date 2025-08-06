<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn
          color="primary"
          variant="outlined"
          prepend-icon="mdi-arrow-left-thin"
          text="Back"
          :to="{name: 'resources'}"
        />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-card color="primary"
        >
          <v-card-title class="text-h4 text-uppercase">
            {{ displayResourceKeyword }}
          </v-card-title>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
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


const displayResourceKeyword = computed(() => formattedResourceKeyword.value.replace(/([A-Z])/g, ' $1').trim()) // Find all uppercase letters, replace each with space + that letter, then trim whitespace

const route = useRoute()
const keyword = route.params.keyword

onMounted(async () => {
  resourceKeyword.value = keyword
  await loadResourceComponent()
})
</script>


<route lang="yaml">
  meta:
    requiresAuth: true
</route>
