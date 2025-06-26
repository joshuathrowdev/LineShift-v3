import { ref } from 'vue';
import sportsService from '@/services/sports.api';

export const useSports = () => {
  // any data members of the composable initializer
  const sports = ref([]);

  // any logic that uses the data members
  // logic tied directly to var and will updated that instance
  const getAllSports = async () => {
    try {
      sports.value = await sportsService.getAllSports();
      // testing data assignment
      console.log(sports.value);
    } catch (error) {
      console.warn('Error with the Sports Composable', error);
      throw error;
    }
  };


  return { sports, getAllSports };
};