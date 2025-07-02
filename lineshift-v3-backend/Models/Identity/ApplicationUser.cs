using Microsoft.AspNetCore.Identity;

namespace lineshift_v3_backend.Models.Identity
{
    // Custome User Model for out 
    
    // We must extend the default IdentityUser to include app-speicifc user data
    // The IdentityUser class already comes with some predifined attributes that are
        // already includes when you extend it
    public class ApplicationUser : IdentityUser
    {
        // Extended information or attrbutes that we want to assign to our users
        // this could be features and any other information that we wanted


        // --- Basic User Informatoin ---
        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        /// <summary>
        /// The date and time when the user registered.
        /// Stored in UTC 
        /// </summary>
        public DateTimeOffset? RegisteredDate { get; set; } = DateTimeOffset.UtcNow;


        // --- Application Specific Data ---
        
        /// <summary>
        ///  Represents the user's membership level (e.g., Free, Bronze, Silver, Gold, etc).
        /// </summary>
        public string? SubscriptionTier { get; set; }

        /// <summary>
        /// A flag indicating if the user's account is currently active or not.
        /// Allows for diabling accounts withiout deleting them.
        /// Indicated whether the user is currently operational and permitted to interact with the system.
        /// Temporary Suspenions and Deactivations
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The last date and time the user logged in.
        /// Useful for inactivity tracking or "last seen" features.
        /// </summary>
        public DateTimeOffset? LastLoginDate { get; set; }

        /// <summary>
        /// The date and time the user's profile was last updated
        /// </summary>
        public DateTimeOffset? LastUpdatedDate { get; set; }

        // --- Optional: Profile Picture / Avatar ---

        // --- Optional: Geographical Information ---

        // --- Optional: Soft Delete ---

        /// <summary>
        /// For soft deletion: Marks a user as deleted without removing them from the database.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// The date and time the user was soft-deleted.
        /// Indicates that an entity has been logically "deleted" from the application perspective
        /// </summary>
        public DateTimeOffset? DeletedDate { get; set; }


        // --- Navigational Properties (if you have other entities related to the user) ---

        // Example: If a user can have many orders
        // public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Example: If a user belongs to a specific team
        // public int? TeamId { get; set; }
        // public Team? Team { get; set; }
    }
}
