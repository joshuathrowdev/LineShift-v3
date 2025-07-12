namespace lineshift_v3_backend.Dtos
{
    public class SessionUser
    {

        // Basic User Information
        public string UserId {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;

        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

        // Date and Time Information
        public DateTimeOffset? RegisteredDate { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public DateTimeOffset? LastUpdatedDate { get; set; }

        // Role and Claim Information
        public List<string>? Roles { get; set; }
        public string? SubscriptionTier { get; set; } = string.Empty;


    }
}
