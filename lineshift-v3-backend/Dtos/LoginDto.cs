using System.ComponentModel.DataAnnotations;

namespace lineshift_v3_backend.Dtos
{
    // Login Model for capturing information related to an already
    // existing user 
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
