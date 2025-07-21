<template>
  <v-form v-model="validSportForm"> 
    <v-container>
      <v-row>
        <v-col cols="8">
          <v-text-field 
            v-model="sportDto.SportName"
            label="Sport Name"
            variant="outlined"
          />
        </v-col>

        <v-col cols="4">
          <v-text-field 
            v-model="sportDto.Type"
            label="Type"
            variant="outlined"
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <v-text-field
            v-model="sportDto.Description"
            label="Description"
            variant="outlined"
          />
        </v-col>
      </v-row>

      
      <!-- Testing -->
      {{ sportDto }}

      <v-row>
        <v-col>
          <v-dialog>
            <template #activator="{props}">
              <v-btn
                v-bind="props"
                class="w-100"
                color="primary-darken-1"
              >
                <template #prepend>
                  <v-icon
                    icon="mdi-database-plus-outline"
                    color="primary-accent"
                  />
                </template>
                Create
              </v-btn>
            </template> 

            <template #default>
              <v-card>
                Confirmation Modal
              </v-card>
            </template>
          </v-dialog>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script setup>
import { ref } from 'vue';

const sportDto = ref ({
  SportName: '',
  Description: '',
  Type: ''
})

const validSportForm = ref()
</script>