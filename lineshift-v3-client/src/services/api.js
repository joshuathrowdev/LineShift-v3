import axios from 'axios';

const proxyTrigger = '/api';

const axiosInstance = axios.create({
  baseURL: `${proxyTrigger}`,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
    // 'Authorization': 'Bearer ' + localStorage.getItem('token') // Example for adding auth token
  }
});

export default axiosInstance;

// For the Proxy server to work, axios needs to point to the relative path of our frontend
// if we use the fully qualified path for the URL, the proxy server will be bypassed entirely
// Point to the *proxy path* on your frontend's server
// goes through: https://localhost:{port of frontend (3000)}/proxyTrigger
// this will trigger the proxy server defined in the vite.config file with the corresponding trigger