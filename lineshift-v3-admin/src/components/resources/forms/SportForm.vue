<template>
  <v-form
    v-model="form"
    class="pa-5"
    @submit.prevent
  > 
    <v-container>
      <v-row>
        <v-col cols="8">
          <v-text-field 
            v-model="sportDto.SportName"
            label="Sport Name"
            variant="underlined"
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
            <slot name="closeBtn" />

            <v-btn
              v-bind="props"
              color="primary-darken-1"
              :disabled="!isformValid"
            >
              <template #prepend>
                <v-icon
                  icon="mdi-database-plus-outline"
                  color="primary-accent"
                />
              </template>
              Create
            </v-btn>
          </div>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script setup>
import { ref } from 'vue';


const props = defineProps(['parent-display'])
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

const form = ref(null)
const isformValid = computed(() => 
  form.value && sportDto.value.SportName !== '' && sportDto.value.Type !== '' & sportDto.value.Description !== ''
)


// const sportsStore = useSportsStore()
// const {createSport} = sportsStore

const resetSportDto = () => {
  sportDto.value.SportName = ''
  sportDto.value.Description = ''
  sportDto.value.Type = ''
}

const handleformSubmit = async() => {
  // await createSport(sportDto.value)

  resetSportDto()
}
</script>