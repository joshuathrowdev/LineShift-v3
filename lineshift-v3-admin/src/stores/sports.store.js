import sportsApi from "@/services/sports.api";

const useSportsStore = defineStore("sports", () => {
  // State
  const sports = ref([]);

  // Getters

  // Actions
  const getAllSports = async () => {
    const response = await sportsApi.getAllSports();
    sports.value = response;
  };

  return {
    // State
    sports,

    // Actions
    getAllSports,
  };
});

export default useSportsStore;
