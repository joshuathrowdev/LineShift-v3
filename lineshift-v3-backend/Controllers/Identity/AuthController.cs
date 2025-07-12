using lineshift_v3_backend.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using lineshift_v3_backend.Models.Database;
using lineshift_v3_backend.Services.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace lineshift_v3_backend.Controllers.Identity
{
    [ApiController]
    [Route("api/v3/[controller]")]
    [Produces(MediaTypeNames.Application.Json)] // Specifies that the controller actions produce JSON responses
    [Consumes(MediaTypeNames.Application.Json)] // Specifies that the controller actions consume JSON requests
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthServices _authServices;
        private readonly ILogger<AuthController> _logger;
        // Interface that provides access to application's configuration settings
        // readonly from appsettings.json, enviroment vars, etc
        private readonly IConfiguration _configuration;


        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,

            IAuthServices authServices,

            ILogger<AuthController> logger,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authServices = authServices;
            _logger = logger;
            _configuration = configuration;
        }

        // Register a new account



        // Authenticate a user and issue a JWT
        // (loggin a existing user)
        // localhost:port/api/v3/auth/login
        [HttpPost("login")]
        [AllowAnonymous] // Allows unaithenticated acccess to this endpoint
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Model State was invalid: {ModelState}.");
                return BadRequest(ModelState);
                // ModelState (inherited by ControllerBase) is a dict like object that represents the state of 
                // model binding and model validation for the current HTTP request
                // Stores Validation Errors (EX: if a [Required] attribute was missing) and Model Binding Errors
                // that occured during the process of mapping incoming request data (from body, route, or query string)
                // in action method parameters 
            }


            try
            {
                var result = await _authServices.LoginAsync(loginModel);

                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                else
                {
                    if (result.ErrorCode == "INVALID_CREDENTIALS")
                    {
                        return Unauthorized(new { Message = result.Error });
                    }

                    if (result.ErrorCode == "INACTIVE_ACCOUNT")
                    {
                        return Unauthorized(new { Message = result.Error });
                        // Technically, a 403 Forbid / Forbidden would be right be add a slight security risk
                        // attackers could index what usernames and passwords are valid 
                    }

                    if (result.ErrorCode == "LOCKEDOUT_ACCOUNT")
                    {
                        return Unauthorized(new { Message = result.Error });
                    }

                    if (result.ErrorCode == "NOT_ALLOWED_ACCOUNT")
                    {
                        return Unauthorized(new { Message = result.Error });
                    }

                    return StatusCode(500, "An unexpected server error occurred during login.");
                }
            } 
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: "An error occured while accessing the auth services.");
                throw;
            }
        }


        // Log out a user
        // (client should discard their JWT)



        // Fetch detaills of the currently authenticated user
        // Require a valid JWT in Authirization header
        // EX: If client refreshes but a token is in local storage, grab user info based on that token
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult> GetMe()
        { 
            try
            {
                // LOCAL/CLIENT JWT is passed in request headers under the Bearer key

                // 1. Extract User ID from the JWT (ClaimsPrincipal)
                // The ClaimTypes.NameIdentifier claim (often 'sub') typically holds the user's unique ID
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    // this should theoretically not happen 
                    _logger.LogError($"A valid token is present for '{userId}', but no claim id found.");
                    return Unauthorized(); // Token valid, but no Id claim found
                }

                // Extracting UserProfile from Auth Services
                var sessionUser = await _authServices.GetUserByIdAsync(userId);

                if (sessionUser == null)
                {
                    _logger.LogWarning($"UserId '{userId}' could not be found from Claim Types.");
                    return NotFound(); // valid token but the user could not be found
                }

                // If session user found from claim id and is active
                return Ok(sessionUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while attempting to access the Auth services.");
                throw;
            }
        }



        #region Helper Methods
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            { 
                // A Claim is a piece of information, expressed as a name-value pair
                // that asserts something about a subject 
                // For JWT, they are statements about the authenticated user that are securly
                // encoded within the token
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
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
        #endregion

    }
}


