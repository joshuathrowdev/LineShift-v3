using System.ComponentModel.DataAnnotations;

namespace lineshift_v3_backend.Models.Identity
{
    // Login Model for capturing information related to an already
    // existing user 
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
