import sportsApi from '@/services/sports.api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useSnackbarStore } from '../snackbar.store';

export const useSportsStore = defineStore('sports', () => {
  const {showError} = useSnackbarStore()

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