import sportsApi from '@/services/sports.api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useSnackbarStore } from './snackbar.store';

export const useSportsStore = defineStore('sports', () => {
  const snackbarStore = useSnackbarStore();
  const { showSuccess, showError } = snackbarStore;

  // State
  const sports = ref([]);

  // Getters

  // Action
  const getAllSports = async () => {
    try {
      sports.value = await sportsApi.getAllSports();
    }
    catch (error) {
      console.error(error);
      showError(error);
      throw error;
    }
  };





  return {
    // State
    sports,

    // Getters

    // Action
    getAllSports,
  };
});