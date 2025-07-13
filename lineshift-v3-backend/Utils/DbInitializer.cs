using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace lineshift_v3_backend.Utils
{
    // Dummy class for logger category
    public class DbInitializerLoggerCategory { }


    /// <summary>
    /// Static class responsible for initializing the database with essential
    /// Identity roles, user, and claims during application startup
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize the database with predefined roles, users, and their associated claims.
        /// Designed to be idempotent
        /// </summary>
        /// <param name="serviceProvider">The application's service provider</param>
        /// <param name="logger">Logger instance for loggin messages and errors during seeding</param>
        public static async Task Initialize(
            IServiceProvider serviceProvider, ILogger<DbInitializerLoggerCategory> logger
        )
        {
            // Resolving necessary services from service provider
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>> ();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            logger.LogInformation("Stating database initialization...");


            // Attempt to seed database
            try
            {
                // Seeing if the database exist and all migrations have been applied
                // Migrations and syncing is a must for EFCore Identity
                await context.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");


                // --- 1. Seed Roles ---
                logger.LogInformation("Seeding roles...");
                string[] roleNames =
                {
                    "Admin",
                    "Moderator",
                    "Free",
                    "Basic",
                    "Pro",
                    "Edge"
                };

                foreach (var roleName in roleNames)
                {
                    // Check if the roles already exist (to prevent duplicates)
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"Role '{roleName}' created successfully.");
                        }
                        else
                        {
                            logger.LogInformation($"Failed to create role '{roleName}': " +
                                $"{string.Join(", ", result.Errors.Select(e => e.Description))}.");
                        }
                    }
                    else
                    {
                        logger.LogInformation($"Role '{roleName}' already exist. Skipping creation.");
                    }
                }

                // --- 2. Seed Users and Assign Roles/Claims
                // Define test users for each tier with their specific roles and claims

                #region Seeding Users
                // Admin User: Full access, top tier subscription implicitly
                await SeedUser(
                    userManager,
                    roleManager,
                    logger,
                    "adminTest@lineshift.com",
                    "AdminPassword123$",
                    "AdminTest",
                    "Admin",
                    "User",
                    "Admin",
                    new List<Claim>
                    {
                        new Claim("CanManageUsers", "true"),
                        new Claim("CanManageContent", "true"),
                        new Claim("CanAccessAdminPanel", "true"),
                        new Claim("CanManageSubscriptions", "true"),
                        new Claim("SubscriptionTier", "Edge"), // Admin implicitly has highest tier features
                    }
                );

                // Basic User: Basic access, entry-level subscription
                await SeedUser(
                    userManager,
                    roleManager,
                    logger,
                    "basicTest@lineshift.com",
                    "BasicPassword123$",
                    "BasicTest",
                    "Basic",
                    "User",
                    "Basic",
                    new List<Claim>
                    {
                        new Claim("SubscriptionTier", "Bsic")
                    }
                );

                // Pro User: Mid-tier access, endhanced subscription
                await SeedUser(
                    userManager,
                    roleManager,
                    logger,
                    "proTest@lineshift.com",
                    "ProPassword123$",
                    "ProTest",
                    "Pro",
                    "User",
                    "Pro",
                    new List<Claim>
                    {
                        new Claim("SubscriptionTier", "Pro")
                    }
                );

                // Edge User: Premium access, elite subscription
                await SeedUser(
                    userManager,
                    roleManager,
                    logger,
                    "edgeTest@lineshift.com",
                    "EdgePassword123$",
                    "EdgeTest",
                    "Edge",
                    "User",
                    "Edge",
                    new List<Claim>
                    {
                        new Claim("SubscriptionTier", "Edge")
                    }
                );

                // Free User: Authenticated but no paid subscription
                await SeedUser(
                    userManager,
                    roleManager,
                    logger,
                    "freeTest@lineshift.com",
                    "FreePassword123$",
                    "FreeTest",
                    "Free",
                    "User",
                    "Free",
                    new List<Claim>
                    {
                        new Claim("SubscriptionTier", "Free")
                    }
                );
                #endregion

                // End of seeding
                logger.LogInformation("Database initialization comeplete.");
            }
            catch (Exception ex)
            {
                // Loggin unhandled exceptions during seeding process
                logger.LogError(ex, "An error occured while seeding databae.");
            }
        }


        /// <summary>
        /// Helper method to create a user, assign a role, and add claims
        /// Ensures idempotency for each step
        /// </summary>
        private static async Task SeedUser(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger logger,
            string email, 
            string password,
            string username,
            string firstName,
            string lastName,
            string roleName,
            IEnumerable<Claim> claims)
        {
            // Check if the user already exist by email (to prevent duplicates)
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = username,
                    RegisteredDate = DateTimeOffset.UtcNow,
                };

                // Create the user with the provided password. UserManger handles hashing
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    logger.LogInformation($"User '{email}' created successfully.");

                    // Asign the user to the specific role (if it exist)
                    if (await roleManager.RoleExistsAsync(roleName))
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                        logger.LogInformation($"User '{email}' added to role '{roleName}'.");
                    }
                    else
                    {
                        logger.LogWarning($"Role '{roleName}' does not exist for user '{email}. Skipping role assignment.'");
                    }

                    // Add claims to the user (if claim exist and already assigned)
                    var existingClaims = await userManager.GetClaimsAsync(user);
                    foreach (var claim in existingClaims)
                    {
                        // Check if a claim of the same type and value already exist for user
                        if (!existingClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                        {
                            await userManager.AddClaimAsync(user, claim);
                            logger.LogInformation($"Claim '{claim.Type}': '{claim.Value}' added to user '{email}'.");
                        }
                        else
                        {
                            logger.LogInformation($"Claim '{claim.Type}': '{claim.Value}' already exist for user '{email}'. Skipping claim assignment.");
                        }
                    }
                }
                else
                {
                    // Log erros if user creation failed
                    logger.LogError($"Failed to create user '{email}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                // User already exists in database
                logger.LogInformation($"User '{email}' already exist. Skipping user creation");

                // We might still want to ensure their roles and claims are up-to-date.
                // This is especially useful if you modify the seeding logic for existing users.
                var existingUser = await userManager.FindByEmailAsync(email);

                // Ensure role is assigned
                if (existingUser != null && !await userManager.IsInRoleAsync(existingUser, roleName))
                {
                    if (await roleManager.RoleExistsAsync(roleName))
                    {
                        await userManager.AddToRoleAsync(existingUser, roleName);
                        logger.LogInformation($"Existing user '{email}' added to missing role '{roleName}'.");
                    }
                }

                // Ensure claims are assigned/updated
                var currentClaims = await userManager.GetClaimsAsync(existingUser);
                foreach (var claim in claims)
                {
                    var existingClaim = currentClaims.FirstOrDefault(c => c.Type == claim.Type);
                    if (existingClaim == null)
                    {
                        await userManager.AddClaimAsync(existingUser, claim);
                        logger.LogInformation($"Missing claim '{claim.Type}: {claim.Value}' added to existing user '{email}'.");
                    }
                    else if (existingClaim.Value != claim.Value)
                    {
                        // If the claim exists but its value is different, replace it
                        await userManager.ReplaceClaimAsync(existingUser, existingClaim, claim);
                        logger.LogInformation($"Claim '{claim.Type}' updated for existing user '{email}' from '{existingClaim.Value}' to '{claim.Value}'.");
                    }
                }
            }
        }
    }
}
