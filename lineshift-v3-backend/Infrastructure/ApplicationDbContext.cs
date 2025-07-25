using lineshift_v3_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lineshift_v3_backend.Infrastructure
{
    // we must to inherit from IdentityDbContext
    // specialized version of DbContext that automatically comes with the tables
    // and DBSets associated with Identity data models
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor: This is where DbContextOptions are passed, typically via Dependency Injection
        // in ASP.NET Core applications. These options contain configuration like the database connection string.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties: These represent the collections of your entities that map to database tables.
        // When you query ApplicationDbContext.Books, EF Core knows to interact with the 'Books' table.
        public DbSet<Sport> Sports { get; set; }
        public DbSet<GoverningBody> GoverningBodies { get; set; }
        public DbSet<League> Leagues { get; set; }


        // OnModelCreating method: This is where you configure your database model using the Fluent API.
        // It's used to define relationships, constraints, table/column names, etc., that go beyond
        // what can be expressed with data annotations on the entity classes.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base implementation

            // Configure each Model more specifically if you need to
            modelBuilder.Entity<Sport>(entity =>
            {
                // Explicitly defining the PK for the Sport Entity
                entity.HasKey(e => e.SportId);
                entity.Property(e => e.SportId)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                // Configure other properties (validation, constraints, Indexes)
                entity.Property(e => e.SportName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => new { e.SportName })
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(250);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();

                // Configure Any Relationship that between Sport and other tables
                // Define Relationships and how navigating those relationships work
                // Have to have the model for relating models(tables) defined
            });

            modelBuilder.Entity<GoverningBody>(entity =>
            {
                entity.HasKey(e => e.GoverningBodyId);
                entity.Property(e => e.GoverningBodyId)
                    .IsRequired()
                    .ValueGeneratedOnAdd(); // Auto Increment PK

                entity.Property(e => e.GoverningBodyName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.HasIndex(e => new { e.GoverningBodyName })
                    .IsUnique();

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CountryOfOrigin)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.DateFounded);

                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();

                // Relationships
                entity.HasMany(e => e.Leagues)
                    .WithOne(league => league.GoverningBody) // Nav Property
                    // Above fully describes the one to many relationship
                    .HasForeignKey(league => league.GoverningBodyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<League>(entity =>
            {
                entity.HasKey(e => e.LeagueId);
                entity.Property(e => e.LeagueId)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LeagueName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.HasIndex(e => e.LeagueName)
                    .IsUnique();

                entity.Property(e => e.Level).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Gender).IsRequired().HasMaxLength(20);

                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
            });


            // Identity.EFCore Tables (For user management and authentication)
            // Renaming the default tables for clarity
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            // claims specifically associated with the users
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            // External login provides (like Google, Facebook, etc) linked to user
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            // many to many relationship between users and roles
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            // Stores auth tokens for users (refresh tokens, 2FA provider tokens, etc)
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            // claims specifically associated with roles (any user with that role gets these claims)
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            // Resource Tables Explicit Configs
            modelBuilder.Entity<GoverningBody>().ToTable("GoverningBodies");
        }
    }
}
