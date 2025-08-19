import { defineStore } from "pinia";
import { ref, computed, shallowRef } from "vue";
import { defineAsyncComponent } from "vue";
import { useSnackbarStore } from "./snackbar.store";

export const useResourceStore = defineStore("resources", () => {
  // Auto find and map components based on glob template we pass
  const componentGlobModules = import.meta.glob("@/components/resources/*.vue");

  // State
  const resourceKeyword = ref("");
  const resourceComponent = shallowRef(null);

  // Getters
  const formattedResourceKeyword = computed(() =>
    resourceKeyword.value
      .split("-")
      .map((word) => {
        return word.charAt(0).toUpperCase() + word.slice(1);
      })
      .join(""),
  );
  const singularNounResourceKeyword = computed(
    () =>
      resourceKeyword.value.charAt(0).toUpperCase() +
      resourceKeyword.value.slice(1, resourceKeyword.value.length - 1),
  );

  // Actions
  const loadResourceComponent = async () => {
    resourceComponent.value = null; // clears old loaded component

    // grabs components from the 'components/resources' folder only
    // tries to match the route name to a component name in the blob (component dir its looking at )
    const componentPath = `/src/components/resources/${formattedResourceKeyword.value}.vue`;
    console.log(formattedResourceKeyword.value);
    const componentLoader = componentGlobModules[componentPath];

    console.log(componentPath);
    console.log(componentLoader);
    console.log(componentGlobModules);

    if (componentLoader) {
      try {
        // wraps dynamic import, handling loading
        resourceComponent.value = defineAsyncComponent(componentLoader);
      } catch (error) {
        const { showError } = useSnackbarStore();
        showError(`Failed to load component for ${resourceKeyword.value}.`);
      }
    } else {
      const { showError } = useSnackbarStore();
      showError(`Component for keyword '${resourceKeyword.value}' not found.`);
    }
  };

  return {
    // State
    resourceKeyword,
    resourceComponent,

    // Getters
    formattedResourceKeyword,
    singularNounResourceKeyword,

    // Actions
    loadResourceComponent,
  };
});
