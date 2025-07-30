import { defineStore } from "pinia";
import { ref, computed } from "vue";
import sportsApi from "@/services/sports.api";
import Sports from "@/components/resources/Sports.vue";

const resourceApisGetHttpMethod = {
  sports: sportsApi.getAllSports,
};

export const useResourceStore = defineStore("resources", () => {
  // State
  const resourceKeyword = ref("");
  const resourceItems = ref([]);
  const loading = ref(false);

  const resourceComponentMap = ref({
    sports: () => import(Sports),
  });

  // Getters
  const formattedResourceKeyword = computed(
    () =>
      resourceKeyword.value.charAt(0).toUpperCase() +
      resourceKeyword.value.slice(1),
  );
  const singularNounResourceKeyword = computed(
    () =>
      resourceKeyword.value.charAt(0).toUpperCase() +
      resourceKeyword.value.slice(1, resourceKeyword.value.length - 1),
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
    resourceComponentMap,

    // Getters
    formattedResourceKeyword,
    singularNounResourceKeyword,

    // Actions
    getResourceByKeyword,
  };
});
