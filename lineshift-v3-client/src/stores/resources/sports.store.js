import sportsApi from '@/services/sports.api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useSnackbarStore } from '../snackbar.store';

export const useSportsStore = defineStore('sports', () => {
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
      const {showError} = useSnackbarStore()
      showError(error.message);
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