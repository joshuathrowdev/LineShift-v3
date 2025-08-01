import leaguesApi from "@/services/leagues.api";
import { useSnackbarStore } from "../snackbar.store";
import { defineStore } from "pinia";

const useLeaguesStore = defineStore("leagues", () => {
  const { showError } = useSnackbarStore();

  // State
  const leagues = ref([]);

  // Getters

  // Actions
  const getAllLeagues = async () => {
    try {
      const response = await leaguesApi.getAllLeagues();
      leagues.value = response;
    } catch (error) {
      console.log(error);
      showError(error.message);
    }
  };

  return {
    // State
    leagues,

    // Actions
    getAllLeagues,
  };
});

export default useLeaguesStore;
