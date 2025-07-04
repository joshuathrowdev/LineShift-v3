import axios from 'axios';

const clientProxyTrigger = '/api'; // client side proxy server trigger
const backendApiPrefix = '/api/v3'; // backend api prefix (incase backend is service multiple applications)


const axiosInstance = axios.create({
  baseURL: `${clientProxyTrigger + backendApiPrefix}`,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
    // 'Authorization': 'Bearer ' + localStorage.getItem('token') // Example for adding auth token
  }
});


// Auth Token Bearer Dynamic Configuration
import { useAuthStore } from '@/stores/auth.store';

// intercepts all request and conditionally adds auth token if present
axiosInstance.interceptors.request.use(
  (config) => {
    // Initializing local instance of Auth Store
    const authStore = useAuthStore();

    const token = authStore.authToken;

    // if a token is present from local storage, the auth store will auto grab it
    // if not present this if statement is skipped
    if (token) { config.headers.Authorization = `Bearer ${token}`; }

    return config;
  }, (error) => {
    return Promise.reject(error);
  });

export default axiosInstance;

// For the Proxy server to work, axios needs to point to the relative path of our frontend
// if we use the fully qualified path for the URL, the proxy server will be bypassed entirely
// Point to the *proxy path* on your frontend's server
// goes through: https://localhost:{port of frontend (3000)}/proxyTrigger
// this will trigger the proxy server defined in the vite.config file with the corresponding trigger