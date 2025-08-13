namespace lineshift_v3_backend.Models.Auth
{
    // Base class for all ops results (and dont return specific data)
    public class AuthResult
    {
        // general response ops success 
        public bool IsSuccess { get; protected set; } 
 
        // general, human-readable message describing the overall reason for the failure
        // Concise sentence or phrase
        // EX: "Invalid Credentials"
        public string? Error { get; protected set; } // General error message
        
        // Machine-readable, consistent, short code that reps the type of error (programmatic use)
        // CONSTANT_IDENTIFIER for a specific error condition
        // Front end can use this error code to trigger specific UI responses
        // EX: "INVALID_CREDENTIALS" or "USER_NOT_FOUND"
        public string? ErrorCode { get; protected set; } // Specific, Machine-readble error code

        // For validation error (EX: 400 Bad Request responses)
        // specific details about input validations failures
        // Key: Field Name, Value: Array of error messages for that field
        // EX: { "email": ["Email is required"], "password": ["Password must be at least 8 characters long", "Password must contain a number"] }
        public Dictionary<string, string[]>? ValidationErrors { get; protected set; }



        #region Constructors

        // General ops failure
        protected AuthResult(bool isSuccess, string? error, string? errorCode = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorCode = errorCode;
        }

        // Validation failure
        protected AuthResult(Dictionary<string, string[]> validationErrors)
        {
            IsSuccess = false;
            ValidationErrors = validationErrors;
            Error = "One or more validation errors occures"; // Default message for validation error
            ErrorCode = "VALIDATION_ERROR"; // Standard code for validation issues
        }
        #endregion


        #region Factory Methods 
        public static AuthResult Success() => new AuthResult(true, null); // for sucess
        public static AuthResult Failure(string error, string? errorCode = null) 
            => new AuthResult(false, error, errorCode); // for failure
        public static AuthResult ValidationFailure(Dictionary<string, string[]> validationErrors) 
            => new AuthResult(validationErrors); // validation errors
        #endregion
    }


    #region Result Generic
    public class AuthResponse<T> : AuthResult
    {
        public T? Value { get; protected set; } // The data returned on success


        #region Constructors
        // private constructor for success
        private AuthResponse(T value) : base(true, null)
        {
            Value = value;
        }


        // private constructor for failure
        private AuthResponse(T? value, bool isSuccess, string? error, string? errorCode = null)
            : base(isSuccess, error, errorCode)
        {
            Value = value; // Value might be default(T) or null on failure
        }

        // private constructor for validation errors
        private AuthResponse(T? value, Dictionary<string, string[]> validationErrors)
            : base(validationErrors)
        {
            Value = value;
        }
        #endregion


        #region Factory Methods
        public static AuthResponse<T> Success(T value) => new AuthResponse<T>(value);
        public static AuthResponse<T> Failure(string error, string? errorCode = null)
            => new AuthResponse<T>(default, false, error, errorCode);
        public static AuthResponse<T> ValidationFailure(Dictionary<string, string[]> validationErrors)
            => new AuthResponse<T>(default, validationErrors);
        #endregion
    }
    #endregion
}
