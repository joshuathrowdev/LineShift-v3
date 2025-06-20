import axios from "axios";
import ProxyTrigger from "./globalServices";
import { proxyRefs } from "vue";


const weatherApi = axios.create({
  // For the Proxy server to work, axios needs to point to the relative path of our frontend
  // if we use the fully qualified path for the URL, the proxy server will be bypassed entirely
  // Point to the *proxy path* on your frontend's server
  // goes through: https://localhost:{port of frontend (3000)}/proxyTrgger
  // this will trigger the proxy server defined in the vite.config file with the corresponding trigger
  baseURL: `${ProxyTrigger}`, 
  timeout: 15000,
})

export default weatherApi