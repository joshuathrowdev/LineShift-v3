import { ref } from "vue";

export const useFormValidation = () => {
  const rules = {
    required:
      (fieldName = "This field") =>
      (v) =>
        !!v || `${fieldName} is required`,

    email: (v) => {
      const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return !v || pattern.test(v) || "Please enter a valid email address";
    },

    minLength:
      (min, fieldName = "Field") =>
      (v) => {
        return (
          !v ||
          v.length >= min ||
          `${fieldName} must be at least ${min} characters or more`
        );
      },

    maxLength:
      (max, fieldName = "Field") =>
      (v) => {
        return (
          !v ||
          v.length <= max ||
          `${fieldName} must be ${max} characters or less`
        );
      },

    charactersOnly:
      (fieldName = "Field") =>
      (v) => {
        const pattern = /^[a-zA-Z\s]+$/;
        return (
          !v ||
          pattern.test(v) ||
          `${fieldName} can only contain letters and spaces`
        );
      },
  };

  return {
    rules,
  };
};
