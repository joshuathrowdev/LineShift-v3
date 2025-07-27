import authAdminApi from "@/services/auth.admin.api"

export const useAuthStore = defineStore('auth', () => {
  // State
  const user = ref(null)
  const token = ref(null)
  const isLoading = ref(false)


  // Getters


  // Action
  const login = async (credentials) => {
    try {
      isLoading.value = true

      const response = await authAdminApi.login(credentials)
      if (response) {
        user.value = response.data
        console.log("Successfully logged in")
        console.log(user.value)
      }

    } catch (error) {
      throw error
    }
    finally {
      isLoading.value = false
    }
  }
})