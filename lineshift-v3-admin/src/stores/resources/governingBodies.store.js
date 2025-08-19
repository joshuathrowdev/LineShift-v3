import { useSnackbarStore } from "../snackbar.store";
import { ref } from "vue";
import governingBodiesApi from "@/services/governingBodies.api";
import { defineStore } from "pinia";

const useGoverningBodiesStore = defineStore("governing-bodies", () => {
  // State
  const governingBodies = ref([]);

  // Getters

  // Action
  const getAllGoverningBodies = async () => {
    try {
      const response = await governingBodiesApi.getAllGoverningBodies();
      governingBodies.value = response;
    } catch (error) {
      console.log(error);
      const { showError } = useSnackbarStore();
      showError(error.message);
    }
  };

  return {
    governingBodies,

    getAllGoverningBodies,
  };
});

export default useGoverningBodiesStore;
