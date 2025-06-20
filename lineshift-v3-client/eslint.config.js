import js from "@eslint/js";
import globals from "globals";
import pluginVue from "eslint-plugin-vue";
import vuetify from 'eslint-config-vuetify' // Import for the Vuetify ESLint config
import json from "@eslint/json";
import { defineConfig } from "eslint/config";


export default defineConfig([
  // Base ESLint config (from npm init @eslint/config)
  { files: ["**/*.{js,mjs,cjs,vue}"], plugins: { js }, extends: ["js/recommended"] },
  { files: ["**/*.{js,mjs,cjs,vue}"], languageOptions: { globals: globals.browser } },
  pluginVue.configs["flat/essential"],
  { files: ["**/*.json"], plugins: { json }, language: "json/json", extends: ["json/recommended"] },

  vuetify(), // This applies the Vuetify ESLint rules
]);
