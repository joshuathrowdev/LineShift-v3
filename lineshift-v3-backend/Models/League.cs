using System.ComponentModel.DataAnnotations;

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


        // Internal Flags
        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
