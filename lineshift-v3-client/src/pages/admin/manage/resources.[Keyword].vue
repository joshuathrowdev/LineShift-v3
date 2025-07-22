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
                @click="handleDataTableRefresh"
                elevation="1"
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
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-dialog
          class="w-75"
        >
          <template #activator="{props}">
            <div class="d-flex flex-row justify-end">
              <v-btn
                color="primary"
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
                >
                  <template #resourceFormModalCloseBtn>
                    <v-btn
                      color="primary"
                      @click="isActive.value = false"
                    >
                      Close
                    </v-btn>
                  </template>
                </component>
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

const handleDataTableRefresh = async () => {
  await getResourceByKeyword(keyword)
}


onMounted(async () => {
  resourceKeyword.value = keyword
  await getResourceByKeyword(keyword)
})
</script>

