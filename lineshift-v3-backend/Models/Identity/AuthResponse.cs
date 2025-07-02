
namespace lineshift_v3_backend.Models.Identity
{
    // Model for sending ApplicationUser and JWT meta data
    // For successful login/registration response
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName {  get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;

        // Additional information related to our implementation of ApplicationUser
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTimeOffset? RegisteredDate { get; set; }
        public string? SubscriptionTier { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public DateTimeOffset? LastUpdatedDate { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
