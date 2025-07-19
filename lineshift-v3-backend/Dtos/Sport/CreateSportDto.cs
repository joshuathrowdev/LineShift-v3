using System.ComponentModel.DataAnnotations;

namespace lineshift_v3_backend.Dtos.Sport
{
    public class CreateSportDto
    {
        [Required]
        [StringLength(50)]
        public string? SportName { get; set; }

        [Required]
        [StringLength(250)]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public string? Type { get; set; }
    }
}
