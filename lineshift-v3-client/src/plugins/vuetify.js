/**
 * plugins/vuetify.js
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Styles
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'

// Composables
import { createVuetify } from 'vuetify'

// Custom Defined Themes 
const mainTheme = {
  dark: true,
  colors: {
    background: '#212121', // default background for theme
    surface: '#292927', // default background for surface components (ex: cards) (slightly darker)
    primary: '#22577A',
    secondary: '#57CC99',
    ternary: '#80ED99',
    altOne: '#38A3A5',
    altTwo: '#C7F9CC',
    error: '#B00020',
    info: '#2196F3',
    success: '#4CAF50',
    warning: '#FB8C00'
  }
}

// https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
export default createVuetify({
  theme: {
    defaultTheme: 'mainTheme',
    themes: {
      mainTheme,
    }
  },
})
