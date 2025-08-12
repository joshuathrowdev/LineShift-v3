using lineshift_v3_backend.Models;
using lineshift_v3_backend.Models.Errors;
using lineshift_v3_backend.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace lineshift_v3_backend.Exceptions
{
    public class ExceptionDetailsMapper
    {
        public static ErrorDetails Map(Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "An unexpected server error occurred. Please try again later.";

            switch (exception)
            {
                // More efficient than if-else
                case ResourceCreationException ex:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    //message = ex.Message ?? "An error occurred while attempting to add the resource.";
                    message = ex.Message;
                    break;
                case DuplicateResourceException ex: // database constraint violation (ex: duplicate sport names)
                    statusCode = (int)HttpStatusCode.Conflict;
                    //message = ex.InnerException?.Message ?? ex.Message ?? "The request could not be processed due to invalid data.";
                    message = ex.Message;
                    break;

                case DatabaseOperationException ex:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;

                case OperationCanceledException ex: // Async operation canceled (timeout)
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    //message = ex.InnerException?.Message ?? ex.Message ?? "Operation canceled.";
                    message = ex.Message;
                    break;

                default:
                    // Handles all other exception with a generic 500 error.
                    break;
            }

            return new ErrorDetails{ status = statusCode, message = message };
        }
    }
}

// need to make a custom exception for when attempting to
// post something to the database fails and no rows were affected
//if (recordsAffected < 1)
//{
//    return Result<Sport>.Failure("An error occurred while attempting to add sport", "INVALID_OPERATION");
//}
//return Result<Sport>.Success(sport);
