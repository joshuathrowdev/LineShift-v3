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
            clearable
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="6">
          <v-select
            v-model="leagueDto.Level"
            label="Level"
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
            v-model="leagueDto.Sport"
            required
            autocomplete="off"
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
            v-model="leagueDto.GoverningBody"
            required
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
import { ref } from 'vue';
import { storeToRefs } from 'pinia';

const form = ref(null)

const leagueDto = ref({
  LeagueName: null,
  Level: null,
  Gender: null,
  SportId: null,
  GoverningBodyId: null
})

const {leagueLevels, leagueGenders} = storeToRefs(useLeaguesStore())
const {sports} = storeToRefs(useSportsStore())
const {governingBodies} = storeToRefs(useGoverningBodiesStore())
console.log(sports.value)
console.log(governingBodies.value)


</script>

 <!-- [Key]
 public int? LeagueId { get; set; }

 [Required]
 [StringLength(100)]
 public string? LeagueName { get; set; }

 [Required]
 [StringLength(50)]
 public string? Level { get; set; }

 [Required]
 [StringLength(20)]
 public string? Gender { get; set; }

 // Relationships
 // FK Property
 public int? GoverningBodyId { get; set; }

 public int? SportId { get; set; } -->