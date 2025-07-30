import axiosInstance from "./instance/api";

const sportsApi = {
  async getAllSports() {
    try {
      const response = await axiosInstance.get("/sports");
      console.log(response.data);
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};

export default sportsApi;
