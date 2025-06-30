using lineshift_v3_backend.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;

namespace lineshift_v3_backend.Controllers.Identity
{
    [ApiController]
    [Route("api/v3/[Contoller]")]
    [Produces(MediaTypeNames.Application.Json)] // Specifies that the controller actions produce JSON responses
    [Consumes(MediaTypeNames.Application.Json)] // Specifies that the controller actions consume JSON requests
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        // Interface that provides access to application's configuration settings
        // readonly from appsettings.json, enviroment vars, etc
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // Register a new account



        // Authenticate a user and issue a JWT
        // (loggin a existing user)
        // localhost:port/api/v3/auth/login
        [HttpPost("login")]
        [AllowAnonymous] // Allows unaithenticated acccess to this endpoint
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            // [FromBody] ->Instructinng ASP.NET model binding system to
            // 1. Read the request from body: look for data for this param within the HTTP request body
            // 2. Attempt to deserialize the content of the request body into the type of the param
            // 3. Primary use Content-Type header of the incoming request to determine which input formatter to use
            // This helps when receiving complex data from the clients request body

            // 1. Input Validation
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
                // ModelState (inherited by ControllerBase) is a dict like object that represents the state of 
                // model binding and model validation for the current HTTP request
                // Stores Validation Errors (EX: if a [Required] attribute was missing) and Model Binding Errors
                // that occured during the process of mapping incoming request data (from body, route, or query string)
                // in action method parameters 
            }

            // 2 Find User
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials." }); // 401 Unauthorized

            }

            // 3. Check Acccount Status: Ensure the user is active (not suspended or soft deleted)
            if (!user.IsActive || user.IsDeleted)
            {
                return Unauthorized(new { Message = "Account is inactive or deleted" });
            }

            // 4. Check Password
            var result = await _signInManager.CheckPasswordSignInAsync(user, user.PasswordHash, lockoutOnFailure: true);
            // lockout: true -> enables lockout after a predetermined amount of failures

            if (result.Succeeded)
            {
                // Update LastLoginDate
                user.LastLoginDate = DateTimeOffset.UtcNow;
                await _userManager.UpdateAsync(user); // updates the user in the persistence store (database) based on what attributes changed

                // 6. Generate JWT and Return Success
                var token = await GenerateJwtToken(user);

                return Ok(new AuthResponse
                {
                    Token = token,
                    UserId = user.Id,
                    Username = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.FirstName ?? string.Empty,
                    LastName = user.LastName ?? string.Empty,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList()

                });
            }

            // 7. Handle Specific Failure Casses
            if (result.IsLockedOut)
            {
                return Unauthorized(new { Message = "User account locked out." });
            }
            if (result.IsNotAllowed)
            {
                return Unauthorized(new { Message = "User not allowed to sign in." });
            }
            // if user account requires 2FA, etc


            // 8. Generic Failure (EX: incorrect password)
            return Unauthorized(new { Message = "Invalid credentials." });

        }


        // Log out a user
        // (client should discard their JWT)



        // Fetch detaills of the currently authenticated user
        // Require a valid JWT in Authirization header



        // Helper Method that generate JWT for a given ApplicationUser
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            { 
                // A Claim is a piece of information, expressed as a name-value pair
                // that asserts something about a subject 
                // For JWT, they are statements about the authenticated user that are securly
                // encoded within the token
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? user.Email ?? user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID for the token
                new Claim(ClaimTypes.NameIdentifier, user.Id), // Standard Claim for user ID
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? ""), // Standard claim for username
                new Claim(ClaimTypes.Email, user.Email ?? "") // Standard claim for user email

            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // add each roles to claim
            }

            // Any Additional custom claims for ApplicationUser Model 
            // EX: if we wanted the users FirstName and LastName for display purposes

            // Get JWT config from appsetting.json
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration.");
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found in configuration.");
            var jwtAudience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not found in configuration.");
            var expireDays = Convert.ToDouble(_configuration["Jwt:ExpireDays"] ?? "7");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(expireDays);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}


