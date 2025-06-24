import axios from 'axios';
import ProxyTrigger from './globalServices';

const AxiosInstance = axios.create({
  baseURL: `${ProxyTrigger}`,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
    // 'Authorization': 'Bearer ' + localStorage.getItem('token') // Example for adding auth token
  }
});

export default AxiosInstance;

// For the Proxy server to work, axios needs to point to the relative path of our frontend
// if we use the fully qualified path for the URL, the proxy server will be bypassed entirely
// Point to the *proxy path* on your frontend's server
// goes through: https://localhost:{port of frontend (3000)}/proxyTrigger
// this will trigger the proxy server defined in the vite.config file with the corresponding trigger