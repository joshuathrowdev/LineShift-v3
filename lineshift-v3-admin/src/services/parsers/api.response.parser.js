export default function parseNetworkErrorResponse(error) {
  let message = "An unexpected error occurred";
  let status = 500;

  if (error.response) {
    message = error.response.data?.message || error.message;
    status = error.response.data?.status || error.status;
  } else if (error.request) {
    // request was made but no response
    message = "A network error occured. Please check your internet connection";
    status = 0;
  } else {
    // somethig happened when creating request
    message = error.message;
  }

  return {
    message,
    status,
  };
}
