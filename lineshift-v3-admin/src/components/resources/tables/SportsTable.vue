<template>
  <v-data-table
    :items="resourceItems"
    :search="tableSearch"
    class="pa-4"
  > 
    <template #top>
      <v-text-field 
        v-model="tableSearch"
        variant="outlined"
        label="Search"
        color="primary-accent"
        prepend-inner-icon="mdi-card-search-outline"
        class="ml-3"
      />
    </template>
  </v-data-table>
</template>

<script setup>
  import { useResourceStore } from '@/stores/resources.store';
  import { storeToRefs } from 'pinia';

  const {resourceItems} = storeToRefs(useResourceStore())
  const {getResourceByKeyword} = useResourceStore()

  const tableSearch = ref('')


  onMounted(async () => {
    await getResourceByKeyword('sports')
  })
</script>