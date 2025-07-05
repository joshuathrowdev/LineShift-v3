namespace lineshift_v3_backend.Models.Identity
{
    public class SessionUser
    {

        // Basic User Information
        public string Id {  get; set; }
        public string Email { get; set; }
        public string? Username { get; set; }

        public string? FirstName{ get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }

        // Date and Time Information
        public DateTimeOffset? RegisteredDate { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public DateTimeOffset? LastUpdatedDate { get; set; }

        // Role and Claim Information
        public List<string>? Roles { get; set; }
        public string? SubscriptionTier { get; set; }


    }
}
