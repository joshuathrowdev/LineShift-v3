/**
 * main.js
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins';

// Components
import App from './App.vue';

// Composables
import { createApp } from 'vue';


// Styles
import 'unfonts.css';

const app = createApp(App);


registerPlugins(app);

// Initialization of Auth Store
import { useAuthStore } from './stores/auth.store';
const { initializeAuth } = useAuthStore();
// attempts to initialize session user based on local 
// stored jwt token if available
initializeAuth(); 


app.mount('#app');
