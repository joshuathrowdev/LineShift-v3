import axios from "axios"

const proxyTrigger = import.meta.env.VITE_API_TARGET_ADMIN_TRIGGER

// Make env var
const apiTargetPrefix = '/api/v3'
const apiTargetURL = `${proxyTrigger + apiTargetPrefix}`

console.log(import.meta.env)

const axiosInstance = axios.create({
  baseURL: apiTargetURL,
  timeout: import.meta.env.VITE_API_TIMEOUT,
  headers: {
    'Content-Type': 'application/json',
  }
})


axiosInstance.interceptors.request.use(
  async (config) => {
    const token = import.meta.env.VITE_API_TARGET_ADMIN_TEMP_TOKEN
    config.headers.Authorization = `Bearer ${token}`

    return config

  }, (error) => {
    return Promise.reject(error)
  }
)

export default axiosInstance
