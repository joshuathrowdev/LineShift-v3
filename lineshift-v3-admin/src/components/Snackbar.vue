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
</template>


<script setup>
import { useSnackbarStore } from '@/stores/snackbar.store';
import { storeToRefs } from 'pinia';


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