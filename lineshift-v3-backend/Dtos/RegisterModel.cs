using System.ComponentModel.DataAnnotations;

namespace lineshift_v3_backend.Dtos
{
    // Registration Model for capturing information related to registering a 
    // new user 
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // Optional Fields for mapping to our ApplicationUser
        // provides additional information about the new registered user

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LastName { get; set; }


    }
}
