export function parseApiErrorResponse(error) {
  let message = "An unknown error occurred.";
  let status = 500;
  let details = null;

  if (error.response) {
    message = error.response.data?.message || error.message;
    status = error.status;
    details = error.response.data?.details || null;
  } else if (error.requeste) {
    // request was made but no response
    message =
      "A network error has returned. No response form server. Please check your internet connection.";
    status = 0;
  } else {
    // something happened when creating request
    message = error.message;
  }

  return {
    message,
    status,
    details,
  };
}
