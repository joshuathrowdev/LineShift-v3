namespace lineshift_v3_backend.Utils
{
    // Base class for all ops results (and dont return specific data)
    public class Result
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
        protected Result(bool isSuccess, string? error, string? errorCode = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorCode = errorCode;
        }

        // Validation failure
        protected Result(Dictionary<string, string[]> validationErrors)
        {
            IsSuccess = false;
            ValidationErrors = validationErrors;
            Error = "One or more validation errors occures"; // Default message for validation error
            ErrorCode = "VALIDATION_ERROR"; // Standard code for validation issues
        }
        #endregion


        #region Factory Methods 
        public static Result Success() => new Result(true, null); // for sucess
        public static Result Failure(string error, string? errorCode = null) 
            => new Result(false, error, errorCode); // for failure
        public static Result ValidationFailure(Dictionary<string, string[]> validationErrors) 
            => new Result(validationErrors); // validation errors
        #endregion
    }


    #region Result Generic
    public class Result<T> : Result
    {
        public T? Value { get; protected set; } // The data returned on success


        #region Constructors
        // private constructor for success
        private Result(T value) : base(true, null)
        {
            Value = value;
        }


        // priavte constructor for failure
        private Result(T? value, bool isSuccess, string? error, string? errorCode = null)
            : base(isSuccess, error, errorCode)
        {
            Value = value; // Value might be default(T) or null on failure
        }

        // private constructor for validation erros
        private Result(T? value, Dictionary<string, string[]> validationErrors)
            : base(validationErrors)
        {
            Value = value;
        }
        #endregion


        #region Factory Methods
        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(string error, string? errorCode = null)
            => new Result<T>(default, false, error, errorCode);
        public static Result<T> ValidationFailure(Dictionary<string, string[]> validationErrors)
            => new Result<T>(default, validationErrors);
        #endregion
    }
    #endregion
}
