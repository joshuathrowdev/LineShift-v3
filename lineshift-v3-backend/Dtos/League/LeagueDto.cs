using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lineshift_v3_backend.Dtos.League
{
    public class LeagueDto
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
        public string? Gender { get; set; }

        // Relationships
        // FK Property
        public int? GoverningBodyId { get; set; }

        public int? SportId { get; set; }
    }
}
