import js from '@eslint/js';
import globals from 'globals';
import pluginVue from 'eslint-plugin-vue';
import eslintConfigVuetify from 'eslint-config-vuetify'; // Keep this import
import json from '@eslint/json';

// --- REQUIRED FOR FLATCOMPAT ---
import { FlatCompat } from '@eslint/eslintrc';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);
const compat = new FlatCompat({
  baseDirectory: __dirname,
});
// --- END FLATCOMPAT SETUP ---


export default [
  // 1. Base JavaScript configuration for .js, .mjs, .cjs files
  {
    files: ['**/*.{js,mjs,cjs}'],
    languageOptions: {
      ecmaVersion: 'latest',
      sourceType: 'module',
      globals: {
        ...globals.browser,
      },
    },
    rules: {
      ...js.configs.recommended.rules,
      'no-unused-vars': 'warn',
      'semi': ['error', 'always'],
      'quotes': ['error', 'single'],
      'no-console': ['error', { allow: ['warn', 'error'] }],
    },
  },

  // 2. Vue.js specific configurations
  // The 'flat/*' configs from eslint-plugin-vue are *arrays* of configs.
  // We need to spread them directly.
  ...pluginVue.configs['flat/recommended'],

  // Add an *additional* config object for Vue-specific overrides if needed.
  // This object will merge its rules with the ones from "flat/recommended".
  {
    files: ['**/*.vue'], // Explicitly target .vue files for these overrides
    rules: {
      'vue/multi-word-component-names': 'off',
      'vue/no-unused-components': 'warn',
    },
  },

  // 3. JSON specific configurations
  {
    files: ['**/*.json', '**/*.jsonc'],
    plugins: {
      json: json,
    },
    languageOptions: {
      // No explicit parser needed for JSON
    },
    rules: {
      ...json.configs.recommended.rules,
    },
  },

  // 4. Vuetify specific configurations (USING FLATCOMPAT)
  // `compat.extends()` *returns an array*, so it should be spread.
  ...compat.extends('plugin:vuetify/recommended'),


  // 6. Prettier integration (if configured - usually goes last)
  // If you are using `@vue/eslint-config-prettier`, it's an older-style config.
  // So you'd use compat for that too, and it should go AFTER all other configs
  // to ensure it disables conflicting formatting rules.
  // ...compat.extends('@vue/eslint-config-prettier'),
];