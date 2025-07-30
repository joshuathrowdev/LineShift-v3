import { defineStore } from "pinia";
import { ref, computed } from "vue";
import sportsApi from "@/services/sports.api";

const resourceApisGetHttpMethod = {
  sports: sportsApi.getAllSports,
};

export const useResourceStore = defineStore("resources", () => {
  // State
  const resourceKeyword = ref("");
  const resourceItems = ref([]);
  const loading = ref(false);

  // Getters
  const formattedResourceKeyword = computed(
    () =>
      resourceKeyword.value.charAt(0).toUpperCase() +
      resourceKeyword.value.slice(1),
  );

  // Actions
  const getResourceByKeyword = async (keyword) => {
    try {
      loading.value = true;
      resourceItems.value = await resourceApisGetHttpMethod[keyword]();
    } catch (error) {
    } finally {
      loading.value = false;
    }
  };

  return {
    // State
    resourceKeyword,
    resourceItems,

    // Getters
    formattedResourceKeyword,

    // Actions
    getResourceByKeyword,
  };
});
