using lineshift_v3_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace lineshift_v3_backend.Infrastructure
{
    public class ApplicationDbContext : DbContext
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


        // OnModelCreating method: This is where you configure your database model using the Fluent API.
        // It's used to define relationships, constraints, table/column names, etc., that go beyond
        // what can be expressed with data annotations on the entity classes.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base implementatioon

            // Configure each Model more specifically if you need to
            modelBuilder.Entity<Sport>(entity =>
            {
                // Explicitly defining the PK for the Sport Entity
                entity.HasKey(e => e.SportId);

                // Configute other properties (validation, contraints, Indexes)
                entity.Property(entity => entity.SportName)
                    .IsRequired()
                    .HasMaxLength(255);

                    // Index Contraint
                entity.HasIndex(entity => new {entity.SportName}).IsUnique();

                

                // Configure Any Relationship that between Sport and other tables
                // Define Relationships and how navigating those relationships work
                // Have to have the model for relating models(tables) defined
            });
        }
    }
}
