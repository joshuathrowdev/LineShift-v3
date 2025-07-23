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
      // Snackbar popup
      console.log(error)
      showError(error);
      throw error;
    }
  };


  const createSport = async (sportDto) => {
    try {
      const response = await sportsApi.createSport(sportDto);
      return response;
    } catch (error) { }
  };


  return {
    // State
    sports,

    // Getters

    // Action
    getAllSports,
    createSport,
  };
});