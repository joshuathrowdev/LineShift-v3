/**
 * plugins/vuetify.js
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Styles
import '@mdi/font/css/materialdesignicons.css';
import 'vuetify/styles';

// Composables
import { createVuetify } from 'vuetify';

// Custom Defined Themes 
const mainTheme = {
    dark: true, // This confirms it's a dark theme, affecting default text/icon colors
    colors: {
        // --- Core Palette ---
        background: '#212121', // Default background for the app
        surface: '#292927',    // Default background for surface components (ex: cards)

        primary: '#22577A',
        secondary: '#57CC99',
        ternary: '#80ED99',
        altOne: '#38A3A5',
        altTwo: '#C7F9CC', // Very light, often used as a subtle highlight or text color

        error: '#B00020',
        info: '#2196F3',
        success: '#4CAF50',
        warning: '#FB8C00',

        // --- 'on-' Colors (Text/Icons on top of the corresponding base color) ---
        // These are crucial for ensuring contrast and readability.
        'on-background': '#E0E0E0', // Light gray text on dark background
        'on-surface': '#F5F5F5',    // Lighter text on surface
        'on-primary': '#FFFFFF',    // White text on primary
        'on-secondary': '#000000',  // Black text on secondary (as #57CC99 is relatively light)
        'on-ternary': '#000000',    // Black text on ternary (as #80ED99 is light)
        'on-altOne': '#FFFFFF',     // White text on altOne
        'on-altTwo': '#000000',     // Black text on altTwo (as #C7F9CC is very light)
        'on-error': '#FFFFFF',
        'on-info': '#FFFFFF',
        'on-success': '#FFFFFF',
        'on-warning': '#FFFFFF',

        // --- Surface Variants (for subtle depth/distinction in dark mode) ---
        'surface-light': '#3A3A3A', // A slightly lighter dark gray than 'surface'
        'surface-dark': '#252525',  // A slightly darker dark gray than 'surface', closer to background
        'surface-border': '#424242', // A subtle border color for surfaces

        // --- Primary Color Variants ---
        'primary-lighten-1': '#3D79A2', // Lighter shade of primary
        'primary-darken-1': '#003554',  // Darker shade of primary
        'primary-accent': '#4FC3F7',    // A brighter, more vibrant primary-related accent

        // --- Secondary Color Variants ---
        'secondary-lighten-1': '#80E0B8', // Lighter shade of secondary
        'secondary-darken-1': '#3EAC83',  // Darker shade of secondary
        'secondary-accent': '#90EE90',    // A brighter, more vibrant secondary-related accent

        // --- Ternary Color Variants ---
        'ternary-darken-1': '#5FCF7A', // Darker shade of ternary (useful for active states)

        // --- AltOne Color Variants ---
        'altOne-lighten-1': '#5FC9CB', // Lighter shade of altOne
        'altOne-darken-1': '#177D80',  // Darker shade of altOne

        // --- AltTwo Color Variants (already very light, focus on darker for contrast) ---
        'altTwo-darken-1': '#A3EBB0', // Darker shade of altTwo (useful for hover/active backgrounds)

        // --- Text/Icon Emphasis Variants (useful for specific text states) ---
        'text-high-emphasis': '#F5F5F5', // Typically on-surface
        'text-medium-emphasis': '#B0B0B0', // Slightly subdued text
        'text-disabled': '#757575',      // Very subdued text for disabled elements
    },
};

// https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
export default createVuetify({
  theme: {
    defaultTheme: 'mainTheme',
    themes: {
      mainTheme,
    }
  },
});
