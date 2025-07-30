import sportsApi from "@/services/sports.api";
import { useSnackbarStore } from "./snackbar.store";

const useSportsStore = defineStore("sports", () => {
  const { showError } = useSnackbarStore();

  // State
  const sports = ref([]);

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

    // Actions
    getAllSports,
  };
});

export default useSportsStore;
