<template>
  <v-snackbar 
    v-model="show"
    :timeout="timeout"
    :color="color"
    location="bottom right"
    multi-line
  > 
    {{ message }}
  
    <template #actions>
      <v-btn 
        icon="mdi-close"
        variant="text"
        @click="hideSnackbar"
      />
    </template>
  </v-snackbar>

  <span>Color: {{ color }}</span>

  <v-btn 
    @click="showSuccess('Success')"
  >
    Snackbar Success
  </v-btn>
  <v-btn 
    @click="showError('Error')"
  >
    Snackbar Error
  </v-btn>
  <v-btn 
    @click="showWarning('Warning')"
  >
    Snackbar Warning
  </v-btn>
  <v-btn 
    @click="showInfo('Info')"
  >
    Snackbar Info
  </v-btn>
</template>


<script setup>
import { useSnackbarStore } from '@/stores/snackbar.store';


const snackbarStore = useSnackbarStore()
const {message, color, timeout, show} = storeToRefs(snackbarStore)
const {
  showSnackbar, 
  hideSnackbar, 
  showSuccess, 
  showError,
  showWarning,
  showInfo
} =snackbarStore

</script>