using lineshift_v3_backend.Models;
using lineshift_v3_backend.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace lineshift_v3_backend.Exceptions
{
    public class ExceptionDetailsMapper
    {
        public static (int statusCode, string message) Map(Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "An unexpected server error occurred. Please try again later.";

            switch (exception)
            {
                // More efficient than if-else
                case DbUpdateException: // database constraint violation (ex: duplicate sport names)
                    statusCode = (int)HttpStatusCode.Conflict;
                    message = "The request could not be processed due to invalid data.";
                    break;

                case OperationCanceledException: // Async operation canceled (timeout)
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    message = "Operation canceled";
                    break;

                default:
                    // Handles all other exception with a generic 500 error.
                    break;
            }

            return (statusCode, message);
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
