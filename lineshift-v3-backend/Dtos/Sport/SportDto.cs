using System.ComponentModel.DataAnnotations;

namespace lineshift_v3_backend.Dtos.Sport
{
    public class SportDto
    {
        [Key]
        public int? SportId { get; set; }

        [Required]
        [StringLength(50)]
        public string? SportName { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public string? Type { get; set; }
    }
}
