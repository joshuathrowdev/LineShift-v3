import leaguesApi from "@/services/leagues.api";
import { useSnackbarStore } from "../snackbar.store";
import { defineStore } from "pinia";

const useLeaguesStore = defineStore("leagues", () => {
  const { showError } = useSnackbarStore();

  // State
  const leagues = ref([]);
  const leagueLevels = ref([
    { title: "Professional", value: "professional" },
    { title: "Collegiate", value: "collegiate" },
    { title: "International", value: "international" },
    { title: "Miscellaneous", value: "miscellaneous" },
  ]);

  const leagueGenders = ref([
    { title: "Men", value: "men" },
    { title: "Women", value: "women" },
    { title: "Mixed", value: "mixed" },
  ]);

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
    leagueLevels,
    leagueGenders,

    // Actions
    getAllLeagues,
  };
});

export default useLeaguesStore;
