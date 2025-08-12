
namespace lineshift_v3_backend.Models.Errors
{
    public class ErrorDetails
    {
        public int status { get; set; }
        public string message { get; set; } = string.Empty;

        //public Dictionary<string, string[]>? Errors { get; set; }
    }
}
