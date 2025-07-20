<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn
          color="primary-darken-1"
          variant="flat"
          :to="{name: '/admin/manage/resources'}"
        >
          <span class="text-primary-accent">Back</span>
        </v-btn>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-card
          color="secondary-darken-1"
        >
          <v-card-title class="text-h4">
            Current {{ formattedResourceKeyword }}
          </v-card-title>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-data-table
          :items="resourceItems"
          :search="tableSearch"
          class="pa-3"
          color="primary"
        >
          <template #top>
            <v-text-field 
              v-model="tableSearch"
              variant="outlined"
              label="Search"
              color="primary-accent"
              prepend-inner-icon="mdi-card-search-outline"
            />
          </template>
        </v-data-table>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <div class="d-flex flex-row justify-end">
          <v-btn
            color="primary-darken-1"
          >
            Create A New {{ singularNounResourceKeyword }}
          </v-btn>
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { useRoute } from 'vue-router';
import { useResourceStore } from '@/stores/resources.store';


const route = useRoute()
const keyword = route.params.Keyword

const resourceStore = useResourceStore()
const {resourceItems, resourceKeyword, formattedResourceKeyword } = storeToRefs(resourceStore)
const {getResourceByKeyword} = resourceStore

const singularNounResourceKeyword = computed(() => resourceKeyword.value.slice(0, resourceKeyword.value.length - 1))

console.log(resourceItems.value)

const tableSearch = ref('')

onMounted(async () => {
  resourceKeyword.value = keyword
  await getResourceByKeyword(keyword)
  console.log(resourceItems.value)
})
</script>

