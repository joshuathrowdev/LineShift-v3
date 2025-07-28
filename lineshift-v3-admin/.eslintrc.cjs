// .eslintrc.cjs
module.exports = {
  // Specify the root directory for ESLint configuration
  root: true,

  // Define the environment variables (e.g., browser for client-side JavaScript)
  env: {
    browser: true,
    es2021: true, // For modern JS features (ES2021, equivalent to ES12)
    node: true, // For Node.js specific globals, useful for build scripts etc.
  },

  // Specify the parser to use for ESLint
  // For standard JavaScript in Vue SFCs
  parser: "vue-eslint-parser", // Use vue-eslint-parser for .vue files

  // Configure parserOptions for the JavaScript parser
  parserOptions: {
    ecmaVersion: "latest", // Allow parsing of latest ECMAScript features
    sourceType: "module", // Use ES Modules
    // This tells vue-eslint-parser which parser to use for the <script> block
    // Since not using TypeScript, we use ESLint's default parser (espree)
    parser: "espree", // Or consider "@babel/eslint-parser" if you use Babel extensively
    // Removed: project: "./tsconfig.json", // Not needed without TypeScript
    extraFileExtensions: [".vue"], // Allow parsing JavaScript in .vue files
  },

  // Define global variables (for Vue 3 Composition API macros in <script setup>)
  globals: {
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
    // Add other Vue 3 Composition API functions you use without explicit import
  },

  // Extend recommended configurations (the "rulesets")
  extends: [
    "eslint:recommended", // ESLint's own recommended JavaScript rules
    // Removed: "plugin:@typescript-eslint/recommended", // Not needed without TypeScript
    "plugin:vue/recommended", // Vue recommended rules (for Vue 3 in Legacy Config)
    "plugin:prettier/recommended", // (Optional) Integrates Prettier rules
  ],

  // Register plugins used in 'extends' or 'rules'
  plugins: [
    // Removed: "@typescript-eslint", // Not needed without TypeScript
    "vue", // For Vue rules
    "prettier", // For Prettier integration
  ],

  // Custom rules for your project (these override rules from 'extends')
  rules: {
    // General JavaScript rule overrides
    "no-console": "warn", // Warn for console.log
    // Re-enabled default `no-undef` and `no-unused-vars` since TypeScript won't handle them
    "no-undef": "error", // Ensure undefined variables are caught
    "no-unused-vars": ["warn", { argsIgnorePattern: "^_" }], // Warn for unused variables (allow _ for ignored args)

    // Vue specific rule overrides
    "vue/multi-word-component-names": "off", // As you wanted
    "vue/html-self-closing": [
      "error",
      { html: { void: "always", normal: "always", component: "always" } },
    ],
    "vue/max-attributes-per-line": [
      "error",
      { singleline: { max: 1 }, multiline: { max: 1 } },
    ],
    "vue/html-indent": ["error", 2], // Consistent indentation
    "vue/singleline-html-element-content-newline": "off", // Allow elements on single line
    "vue/multiline-html-element-content-newline": "off", // Allow multiline for readability

    // Prettier-related (if you included plugin:prettier/recommended)
    "prettier/prettier": "error",
  },
};
