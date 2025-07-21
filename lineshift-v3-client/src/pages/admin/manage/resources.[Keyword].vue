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
        <v-dialog>
          <template #activator="{props}">
            <div class="d-flex flex-row justify-end">
              <v-btn
                color="primary-darken-1"
                v-bind="props"
              >
                Create A New {{ singularNounResourceKeyword }}
              </v-btn>
            </div>
          </template>

          <template #default="{isActive}">
            <v-card
              class="pa-3 text-center"
              rounded
            >
              <v-card-title class="text-h5">
                Create {{ singularNounResourceKeyword }} Form
              </v-card-title>
              <v-card-text>
                <component
                  :is="resourceForm"
                  :key="keyword"
                  :keyword="singularNounResourceKeyword"
                />
              </v-card-text>
            </v-card>
          </template>
        </v-dialog>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { useRoute } from 'vue-router';
import { useResourceStore } from '@/stores/resources.store';
import { ref, markRaw } from 'vue';


const route = useRoute()
const keyword = route.params.Keyword

const resourceStore = useResourceStore()
const {resourceItems, resourceKeyword, formattedResourceKeyword } = storeToRefs(resourceStore)
const {getResourceByKeyword} = resourceStore

const singularNounResourceKeyword = computed(() =>resourceKeyword.value.charAt(0).toUpperCase() + resourceKeyword.value.slice(1, resourceKeyword.value.length - 1))


const tableSearch = ref('')

import SportForm from '@/components/admin/forms/SportForm.vue';
import LeagueForm from '@/components/admin/forms/LeagueForm.vue';

const formComponentMap = markRaw({
  'sports' : SportForm,
  'leagues' : LeagueForm
})

const resourceForm = computed(() => formComponentMap[resourceKeyword.value])


onMounted(async () => {
  resourceKeyword.value = keyword
  await getResourceByKeyword(keyword)
})
</script>

