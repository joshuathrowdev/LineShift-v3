<template>
  <v-data-table
    :items="resourceItems"
    :search="tableSearch"
    class="pa-4"
          
    color="primary"
  >
    <template #top>
      <div class="d-flex flex-row justify-space-between">
        <v-btn
          class="mr-3"
          variant="flat"
          icon="mdi-refresh"
          color="primary"
          elevation="1"
          @click="handleDataTableRefresh"
        />
              

        <v-text-field 
          v-model="tableSearch"
          variant="outlined"
          label="Search"
          color="primary-accent"
          prepend-inner-icon="mdi-card-search-outline"
          class="ml-3"
        />
      </div>
    </template>
  </v-data-table>
</template>

<script setup>
import { useResourceStore } from '@/stores/resources.store';

const props = defineProps(['keyword'])
const {keyword} = props
console.log(keyword)


const resourceStore = useResourceStore()
const {resourceItems} = storeToRefs(resourceStore)
const {getResourceByKeyword} = resourceStore


const handleDataTableRefresh = async () => {
  await getResourceByKeyword(keyword)
}

const tableSearch = ref('')

onMounted(async () => {
  await getResourceByKeyword(keyword)
})
</script>