<template>
  <v-form
    v-model="sportForm"
    @submit.prevent
  > 
    <v-container>
      <v-row>
        <v-col cols="8">
          <v-text-field 
            v-model="sportDto.SportName"
            label="Sport Name"
            variant="solo"
            :rules="sportNameRules"
            color="secondary-accent"
            clearable
            glow
            type="text"
            required
          />
        </v-col>

        <v-col cols="4">
          <v-select 
            v-model="sportDto.Type"
            label="Type"
            :items="sportTypes"
            variant="outlined"
            :rules="typeRules"
            color="altTwo"
            clearable
            required
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <v-text-field
            v-model="sportDto.Description"
            label="Description"
            variant="outlined"
            clearable
            :rules="descriptionRules"
            type="text"
            color="altTwo"
            required
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <div class="d-flex flex-row justify-space-around">
            <slot name="resourceFormModalCloseBtn" />

            <v-dialog>
              <template #activator="{props}">
                <v-btn
                  v-bind="props"
                  color="secondary"
                  :disabled="!isSportFormValid"
                >
                  <template #prepend>
                    <v-icon
                      icon="mdi-database-plus-outline"
                      color="secondary-accent"
                    />
                  </template>
                  Create
                </v-btn>
              </template> 

              <template #default="{isActive}">
                <div
                  class="d-flex flex-row justify-center"
                >
                  <v-card
                    class="pa-5"
                    rounded
                  >
                    <v-card-title class="text-wrap">
                      Are you sure you would like to create the {{ keyword }}: "{{ sportDto.SportName }}"
                    </v-card-title>

                    <v-card-actions
                      class="d-flex flex-row justify-space-around"
                    >
                      <v-btn
                        color="primary"
                        variant="flat"
                        @click="isActive.value = false"
                      >
                        No
                      </v-btn>
                      <v-btn
                        color="secondary"
                        variant="flat"
                        type="submit"
                        @click="handleSportFormSubmit(); isActive.value=false"
                      >
                        Yes
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </div>
              </template>
            </v-dialog>
          </div>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script setup>
import { ref } from 'vue';
import { useSportsStore } from '@/stores/sports.store';

const props = defineProps(['keyword'])
const {keyword} = props

const sportNameRules = [
  v => !!v || "Sport Name is required",
  v => (v && v.length <= 50) || "Sport Name must be 50 characters or less",
  v => /^[a-zA-Z\s]+$/.test(v) || "Sport Name can only contain letters and spaces"
]

const sportTypes = ["Team", "Individual"]
const typeRules = [
  v => !!v || "Type is required",
]

const descriptionRules = [
  v => !!v || "Description is required",
  v => (v && v.length <= 250) || "Description must be 250 characters or less",
]

const sportDto = ref ({
  SportName: '',
  Description: '',
  Type: ''
})

const sportForm = ref(null)
const isSportFormValid = computed(() => 
  sportForm.value && sportDto.value.SportName !== '' && sportDto.value.Type !== '' & sportDto.value.Description !== ''
)


const sportsStore = useSportsStore()
const {createSport} = sportsStore

const resetSportDto = () => {
  sportDto.value.SportName = ''
  sportDto.value.Description = ''
  sportDto.value.Type = ''
}

const handleSportFormSubmit = async() => {
  await createSport(sportDto.value)

  resetSportDto()
}
</script>