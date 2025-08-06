<template>
  <v-form
    v-model="form"
    class="pa-5"
    @submit.prevent="handleFormSubmit"
  >
    <v-container>
      <v-row>
        <v-col cols="8">
          <v-text-field
            v-model="governingBodyDto.GoverningBodyName"
            label="Governing Body Name"
            variant="underlined"
            color="primary-lighten-1"
            clearable
            glow
            :rules="nameRules"
            type="text"
            required
          />
        </v-col>

        <v-col cols="4">
          <v-text-field
            v-model="governingBodyDto.Abbreviation"
            required
            :rules="AbbreviationRules"
            clearable
            label="Abbreviation"
            glow
            variant="outlined"
            type="text"
            color="secondary-lighten-1"
          />
        </v-col>

      </v-row>

      <v-row>
        <v-col cols="6">
          <v-autocomplete
            v-model="governingBodyDto.CountryOfOrigin"
            label="Country Of Origin"
            :items="orderedCountries"
            class="w-300px"
            clearable
            item-title="name"
            item-value="alpha3"
            variant="outlined"
          />
        </v-col>

        <v-col cols="6">
          <v-menu
            v-model="dateMenu"
            :close-on-content-click="false"
            offset-y
          >
            <template #activator="{props}">
              <v-text-field
                v-model="displayDate"
                readonly
                v-bind="props"
                label="Date Founded"
                :rules="foundedDateRules"
                append-inner-icon="mdi-calendar"
                variant="outlined"
                icon-color="secondary-accent"
                color="secondary-lighten-1"
              />
            </template>

            <v-date-picker
              v-model="selectedDate"
              v-model:model-value="governingBodyDto.DateFounded"
              show-adjacent-months
              color="secondary-lighten-1"
              @update:model-value="dateMenu = false"
            />
          </v-menu>
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <v-text-field
            v-model="governingBodyDto.Description"
            variant="outlined"
            label="Description"
            required
            :rules="descriptionRules"
            glow
            clearable
            type="text"
            color="secondary-lighten-1"
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <div class="d-flex flex-row justify-space-around">
            <slot name="closeBtn" /> <!-- defining where specific closeBtn slot goes from create new resource button dialog modal -->

            <create-form-submit-resource-button type="submit" :disabled="!form"/>
          </div>
        </v-col>
      </v-row>
    </v-container>
    {{ governingBodyDto }}
  </v-form>
</template>

<script setup>
import { ref, computed } from 'vue';
import { countries, popularCountries } from '@/data/countries';

const governingBodyDto = ref({
  GoverningBodyName: null,
  Abbreviation: null,
  CountryOfOrigin: null,
  DateFounded: null,
  Description: ''

})

const dateMenu = ref(false)
const selectedDate = ref(null)

const displayDate = computed(() => {
  if (!governingBodyDto.value.DateFounded) {return ''}
  return new Date(governingBodyDto.value.DateFounded).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  })
})


const orderedCountries = computed(() => {
  // We have to extract the codes first and work with them directly
  const pinnedCountriesCodes = ['US', 'GB', 'CA', 'GR','JP', 'BR', ]
  const pinnedCountries = countries.filter(country =>  pinnedCountriesCodes.includes(country.code))
  // console.log(pinnedCountries)

  const otherPopularCountries = popularCountries.filter(
    country => !pinnedCountriesCodes.includes(country.code)
  )
  // console.log(otherPopularCountries)

  const otherPopularCountriesCodes = otherPopularCountries.map(country => country.code)
  const otherCountries = countries.filter(country => !pinnedCountriesCodes.includes(country.code))
                                  .filter(country => !otherPopularCountriesCodes.includes(country.code))
  // console.log(otherCountries)


  return [...pinnedCountries, ...otherPopularCountries, ...otherCountries]
})


const nameRules = [
  v => !!v || 'Governing Body name is required',
  v => (v && v.length <=100) || 'Governing Body name must be 100 characters or less',
  v => /^[a-zA-Z\s]+$/.test(v) || "Governing Body Name can only contain letters and spaces"
]

const AbbreviationRules = [
  v => !!v || 'Abbreviation is required',
  v => (v && v.length <= 10) || 'Abbreviation must be 10 characters or less'
]

const CountryOfOriginRules = [
  v => !!v || 'Country of origin is required',
  v => (v && v.length <=3) || 'Country of origin name must be 3 characters or less',
  v => /^[a-zA-Z\s]+$/.test(v) || "Country of origin can only contain letters and spaces"
]

const foundedDateRules = [v=> !!v || 'Founded date is required']

const descriptionRules = [
  v => !!v || 'Description is required',
  v => (v && v.length <= 250) || 'Description must be 250 characters or less'
]

const form = ref(null)

const resetGoverningBodyDto = () => {
  governingBodyDto.value.GoverningBodyName = null
  governingBodyDto.value.Abbreviation = null
  governingBodyDto.value.CountryOfOrigin = null
  governingBodyDto.value.DateFounded = null
  governingBodyDto.value.Description = null
}


const handleFormSubmit = async() => {
  // cleaning form input
  governingBodyDto.value.CountryOfOrigin.toUpperCase()
  await console.log(governingBodyDto.value)

  resetGoverningBodyDto()
}
</script>