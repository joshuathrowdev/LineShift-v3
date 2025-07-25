﻿using System.ComponentModel.DataAnnotations;

namespace lineshift_v3_backend.Models
{
    public class GoverningBody
    {
        [Key]
        public int? GoverningBodyId { get; set; }

        [Required]
        [StringLength(100)]
        public string? GoverningBodyName { get; set; }

        [Required]
        [StringLength(10)]
        public string? Abbreviation { get; set; }

        [Required]
        [StringLength(100)]
        public string? CountryOfOrigin { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public DateTimeOffset? DateFounded { get; set; }

        // Relationships

            // Navigation Property: Collection of related Leagues
            // EF Core knows 'Leagues' refers to this GoverningBody's 'Id'
        public ICollection<League> Leagues = new List<League>();

        // Internal Flags
        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
