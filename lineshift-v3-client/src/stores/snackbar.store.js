import { defineStore } from 'pinia';
import { ref, computed } from 'vue';


const utilityColorsResolver = {
  error: 'error-lighten-1',
  info: 'info-lighten-1',
  success: 'success-lighten-1',
  warning: 'warning-lighten-1',
};


export const useSnackbarStore = defineStore('snackbar', () => {
  // State
  const message = ref('');
  const color = ref('');
  const timeout = ref(5000);
  const show = ref(false);

  // Action
  const showSnackbar = (snackbarMessage, snackBarColor) => {
    message.value = snackbarMessage;
    color.value = snackBarColor;
    show.value = true;
  };

  const resetSnackbar = () => {
    message.value = '';
    color.value = '';
  };

  const hideSnackbar = () => {
    show.value = false;
    resetSnackbar();
  };

  const showSuccess = snackBarMessage => {
    console.log(utilityColorsResolver.success)
    showSnackbar(snackBarMessage, utilityColorsResolver.success);
  };

  const showError = snackBarMessage => {
    showSnackbar(snackBarMessage, utilityColorsResolver.error);
  };

  const showWarning = snackBarMessage => {
    showSnackbar(snackBarMessage, utilityColorsResolver.warning);
  };

  const showInfo = snackBarMessage => {
    showSnackbar(snackBarMessage, utilityColorsResolver.info);
  };


  return {
    // State
    message,
    color,
    timeout,
    show,

    // Actions
    showSnackbar,
    hideSnackbar,
    showSuccess,
    showError,
    showWarning,
    showInfo
  };
});