namespace lineshift_v3_backend.Dtos.Identity
{
    // Model for sending ApplicationUser and JWT meta data
    // For successful login/registration response
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        
        public SessionUser? SessionUser { get; set; }
    }
}
