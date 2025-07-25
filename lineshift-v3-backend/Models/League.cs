using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lineshift_v3_backend.Models
{
    public class League
    {
        [Key]
        public int? LeagueId { get; set; }

        [Required]
        [StringLength(100)]
        public string? LeagueName { get; set; }

        [Required]
        [StringLength(50)]
        public string? Level { get; set; }

        [Required]
        [StringLength(20)]
        public string? Gender{ get; set; }

        // Relationships
            // FK Property
        public int? GoverningBodyId { get; set; }

        // Navigation Property: Reference to the single related GoverningBody
        // The [ForeignKey] attribute explicitly links this navigation property
        // to the GoverningBodyId FK property.
        [ForeignKey("GoverningBodyId")]
        public GoverningBody GoverningBody { get; set; } = null!; // null! for non-null-able FK

        // Internal Flags
        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
