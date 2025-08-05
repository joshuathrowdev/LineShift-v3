<template>
  <v-form v-model="form">
    <v-container>
      <v-row>
        <v-col cols="6">
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

        <v-col cols="3">
          <v-select
            v-model="leagueDto.Level"
            label="Level"
            variant="outlined"
            clearable
            required
            color="secondary-lighten-1"
          />
        </v-col>
          
        <v-col cols="3">
          <v-select
            v-model="leagueDto.Gender"
            required
            clearable
            label="Gender"
            variant="outlined"
            color="secondary-lighten-1"
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="6"> 
          <v-select
            v-model="leagueDto.Sport"
            required
            clearable
            variant="outlined"
            color="secondary-lighten-1"
            label="Sport"
          />
        </v-col>

        <v-col cols="6">
          <v-select
            v-model="leagueDto.GoverningBody"
            required
            clearable
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

            <create-form-submit-resource-button type="submit"> 
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
import { ref } from 'vue';

const leagueDto = ref({
  LeagueName: '',
  Level: '',
  Gender: '',
  Sport: '',
  GoverningBody: ''
})

const form = ref(null)
// const isFormValid = computed(() => )
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