using System.ComponentModel.DataAnnotations;

namespace lineshift_v3_backend.Models
{
    public class Sport
    {
        [Key]
        public int? SportId { get; set; }

        [Required]
        public string? SportName { get; set; }
    }
}
