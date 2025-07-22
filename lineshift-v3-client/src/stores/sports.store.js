import sportsApi from '@/services/sports.api';
import { defineStore } from 'pinia';
import { ref } from 'vue';

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
      // Snackbar popup
      console.log(error);
      throw error;
    }
  };


  const createSport = async (sportDto) => {
    try {
      const response = await sportsApi.createSport(sportDto);
      console.log(response);
    } catch (error) {
      // Snakbar pop
      console.warn(error);
      throw error;
    }
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