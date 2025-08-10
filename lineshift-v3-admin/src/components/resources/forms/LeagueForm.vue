<template>
  <v-form v-model="form">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            v-model="leagueDto.LeagueName"
            type="text"
            variant="underlined"
            color="primary-lighten-1"
            label="League Name"
            required
            :rules="[rules.required('League Name'), rules.maxLength(100, 'League Name')]"
            clearable
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="6">
          <v-select
            v-model="leagueDto.Level"
            label="Level"
            :rules="[rules.required('Level'), rules.maxLength(50, 'Level')]"
            :items="leagueLevels"
            item-title="title"
            item-value="value"
            variant="outlined"
            clearable
            required
            color="secondary-lighten-1"
          />
        </v-col>
          
        <v-col cols="6">
          <v-select
            v-model="leagueDto.Gender"
            required
            :rules="[rules.required('Gender'), rules.maxLength(20, 'Gender')]"
            :items="leagueGenders"
            item-title="title"
            item-value="value" 
            clearable
            label="Gender"
            variant="outlined"
            color="secondary-lighten-1"
          />
          <!-- The item-title is a vue property that allows you to choose what property of the items objects to display -->
          <!-- item-value is the actual value associated with the displayed property -->
          <!-- The 'title' and 'value' are properties of the objects in leagueGenders and are not default -->
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="6"> 
          <v-autocomplete
            v-model="leagueDto.SportId"
            required
            autocomplete="off"
            :rules="[rules.required('Sport')]"
            clearable
            :items="sports"
            item-title="sportName"
            item-value="sportId"
            variant="outlined"
            color="secondary-lighten-1"
            label="Sport"
          />
        </v-col>

        <v-col cols="6">
          <v-autocomplete
            v-model="leagueDto.GoverningBodyId"
            required
            :rules="[rules.required('Governing Body')]"
            clearable
            autocomplete="off"
            :items="governingBodies"
            item-title="governingBodyName"
            item-value="governingBodyId"
            variant="outlined"
            color="secondary-lighten-1"
            label="Governing Body"
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <div class="d-flex flex-row justify-space-around">
            <slot name="closeBtn" />

            <create-form-submit-resource-button type="submit" :disabled="!form"> 
              <template #CloseBtn>
                <v-btn/>
              </template>
            </create-form-submit-resource-button>
          </div>
        </v-col>
      </v-row>
    </v-container>
    {{ leagueDto }}

  </v-form>
</template>

<script setup>
import useSportsStore from '@/stores/resources/sports.store';
import useGoverningBodiesStore from '@/stores/resources/governingBodies.store';
import useLeaguesStore from '@/stores/resources/leagues.store';
import { useFormValidation } from '@/composables/useFormValidation';
import { ref } from 'vue';
import { storeToRefs } from 'pinia';



const form = ref(null)

const {leagueLevels, leagueGenders} = storeToRefs(useLeaguesStore())
const {sports} = storeToRefs(useSportsStore())
const {governingBodies} = storeToRefs(useGoverningBodiesStore())

const leagueDto = ref({
  LeagueName: null,
  Level: null,
  Gender: null,
  SportId: null,
  GoverningBodyId: null
})

const {rules} = useFormValidation()


</script>