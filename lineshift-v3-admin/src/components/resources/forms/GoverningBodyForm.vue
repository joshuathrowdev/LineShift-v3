<template>
  <v-form>
    <v-container>
      <v-row>
        <v-col cols="8">
          <v-text-field
            label="Governing Body Name"
            variant="underlined"
            color="primary-lighten-1"
            clearable
            glow
            type="text"
            required
          />
        </v-col>

        <v-col cols="4">
          <v-text-field
            required
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
          <v-text-field
            label="Country Of Origin"
            clearable
            glow
            variant="outlined"
            type="text"
            required
            color="secondary-lighten-1"
          />
        </v-col>

        <v-col cols="6">
          <v-menu>
            <template #activator="{props}">
              <v-text-field
                readonly
                v-bind="props"
                label="Date Founded"
                append-inner-icon="mdi-calendar"
                variant="outlined"
                icon-color="secondary-accent"
                color="secondary-lighten-1"
              />
            </template>

            <v-date-picker
              v-model="selectedDate"
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
            variant="outlined"
            label="Description"
            required
            glow
            color="secondary-lighten-1"
          />
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <div class="d-flex flex-row justify-space-around">
            <slot name="closeBtn" /> <!-- defining where specific closeBtn slot goes from create new resource button dialog modal -->

            <create-form-submit-resource-button />
          </div>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script setup>
import { ref, computed } from 'vue';

const governingBodyDto = ref({
  governingBodyName: null,
  abbreviation: null,
  countryOfOrigin: null,
  dateFounded: null,
  description: ''

})

const dateMenu = ref(false)
const selectedDate = ref(null)
</script>