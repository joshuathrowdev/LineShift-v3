import { defineStore } from "pinia";
import { ref, computed, shallowRef } from "vue";
import { defineAsyncComponent } from "vue";
import sportsApi from "@/services/sports.api";

const resourceApisGetHttpMethod = {
  sports: sportsApi.getAllSports,
};

export const useResourceStore = defineStore("resources", () => {
  // AUto find and map components based on glob template we pass
  const componentGlobModules = import.meta.glob("@/components/resources/*.vue");

  // State
  const resourceKeyword = ref("");
  const resourceItems = ref([]);
  const resourceComponent = shallowRef(null);

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
      resourceItems.value = await resourceApisGetHttpMethod[keyword]();
    } catch (error) {
      throw error;
    }
  };

  const loadResourceComponent = async () => {
    resourceComponent.value = null; // clears old loaded component

    const componentPath = `/src/components/resources/${formattedResourceKeyword.value}.vue`;
    const componentLoader = componentGlobModules[componentPath];

    console.log(componentPath);
    console.log(componentLoader);
    console.log(componentGlobModules);

    if (componentLoader) {
      try {
        // wraps dynamic import, handling loading
        resourceComponent.value = defineAsyncComponent(componentLoader);
      } catch (error) {
        console.error(
          `Failed to load component for ${resourceKeyword.value}: `,
          error,
        );
      }
    } else {
      console.warn(
        `Component for keyword '${resourceKeyword.value}' not found`,
      );
    }
  };

  return {
    // State
    resourceKeyword,
    resourceItems,
    resourceComponent,

    // Getters
    formattedResourceKeyword,
    singularNounResourceKeyword,

    // Actions
    getResourceByKeyword,
    loadResourceComponent,
  };
});
