// .eslintrc.cjs
module.exports = {
  root: true, // ESLint configuration is rooted here

  env: {
    browser: true, // Browser global variables
    es2021: true, // ECMAScript 2021 global variables
    node: true, // Node.js global variables
    // 'vue/setup-compiler-macros': true, // Vue 3 <script setup> macros
  },

  parser: "vue-eslint-parser", // Parser for .vue files

  parserOptions: {
    ecmaVersion: "latest", // Allow latest ECMAScript syntax
    sourceType: "module", // Use ES Modules
    parser: "espree", // JavaScript parser for <script> blocks
    extraFileExtensions: [".vue"], // Allow parsing JavaScript in .vue files
  },

  globals: {
    // Vue 3 Composition API globals for <script setup>
    ref: "readonly",
    reactive: "readonly",
    computed: "readonly",
    watch: "readonly",
    watchEffect: "readonly",
    onMounted: "readonly",
    onUnmounted: "readonly",
    defineProps: "readonly",
    defineEmits: "readonly",
    defineExpose: "readonly",
    withDefaults: "readonly",
  },

  extends: [
    "eslint:recommended", // ESLint's recommended rules
    "plugin:vue/recommended", // Vue 3 recommended rules (legacy format)
    "plugin:prettier/recommended", // Integrates Prettier rules into ESLint
  ],

  plugins: ["vue", "prettier"], // ESLint plugins

  rules: {
    // General JavaScript rule overrides
    "no-console": "warn",
    "no-debugger": process.env.NODE_ENV === "production" ? "warn" : "off",
    "no-undef": "error",
    "no-unused-vars": ["warn", { argsIgnorePattern: "^_$" }],

    // Vue specific formatting rules
    "vue/multi-word-component-names": "off",
    "vue/html-self-closing": [
      "error",
      { html: { void: "always", normal: "always", component: "always" } },
    ],
    "vue/max-attributes-per-line": [
      "error",
      { singleline: { max: 2 }, multiline: { max: 1 } }, // Enforce 1 attribute per line
    ],
    "vue/html-indent": ["error", 2], // 2-space indentation for Vue templates
    "vue/singleline-html-element-content-newline": "off",
    "vue/multiline-html-element-content-newline": "off",

    "vue/first-attribute-linebreak": [
      "error",
      {
        singleline: "beside",
        multiline: "below", // Force attributes to new line for multi-line elements
      },
    ],

    // Prettier rule for non-Vue files
    "prettier/prettier": [
      "error",
      {
        endOfLine: "lf", // Enforce LF line endings
      },
    ],
  },

  overrides: [
    {
      files: ["*.vue"], // Apply these overrides only to .vue files
      rules: {
        "prettier/prettier": "off", // Disable Prettier rule for Vue templates
      },
    },
  ],
};
