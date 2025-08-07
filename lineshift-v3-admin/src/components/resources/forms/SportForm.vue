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
            :rules="[rules.required('Sport Name'), rules.maxLength(50, 'Sport Name'), rules.charactersOnly('Sport Name')]"
            color="primary-lighten-1"
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
            item-title="title"
            item-value="value"
            variant="outlined"
            :rules="[rules.required('Type')]"
            color="secondary-lighten-1"
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
            :rules="[rules.required('Description'), rules.maxLength(250, 'Description')]"
            type="text"
            color="secondary-lighten-1"
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
              :disabled="!isFormValid"
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
import useSportsStore from '@/stores/resources/sports.store';
import { useFormValidation } from '@/composables/useFormValidation';
import { ref } from 'vue';
import { storeToRefs } from 'pinia';


const props = defineProps(['parent-display'])
const {keyword} = props


const descriptionRules = [
  v => !!v || "Description is required",
  v => (v && v.length <= 250) || "Description must be 250 characters or less",
]

const sportDto = ref ({
  SportName: '',
  Description: '',
  Type: ''
})
const{sportTypes} = storeToRefs(useSportsStore())


const {rules} = useFormValidation()
const form = ref(null)
const isFormValid = computed(() => 
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