import sportsApi from "@/services/sports.api";
import { useSnackbarStore } from "../snackbar.store";
import { defineStore } from "pinia";

const useSportsStore = defineStore("sports", () => {
  const { showError } = useSnackbarStore();

  // State
  const sports = ref([]);
  const sportTypes = ref([
    { title: "Individual", value: "individual" },
    { title: "Team", value: "team" },
    { title: "Combination", value: "combination" },
    { title: "Mass Participation", value: "mass_participation" },
  ]);

  // Getters

  // Actions
  const getAllSports = async () => {
    try {
      const response = await sportsApi.getAllSports();
      sports.value = response;
    } catch (error) {
      console.log(error);
      showError(error.message);
    }
  };

  return {
    // State
    sports,
    sportTypes,

    // Actions
    getAllSports,
  };
});

export default useSportsStore;
