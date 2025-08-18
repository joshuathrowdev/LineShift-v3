using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using lineshift_v3_backend.Services.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using lineshift_v3_backend.Dtos;
using lineshift_v3_backend.Models;
using lineshift_v3_backend.Models.Errors;
using System.Net;

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
        // readonly from appsettings.json, environment vars, etc
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
        // (logging in a existing user)
        // localhost:port/api/v3/auth/login
        [HttpPost("login")]
        [AllowAnonymous] // Allows unauthenticated access to this endpoint
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Model State was invalid: {ModelState}.");
                return BadRequest(ModelState);
                // ModelState (inherited by ControllerBase) is a dict like object that represents the state of 
                // model binding and model validation for the current HTTP request
                // Stores Validation Errors (EX: if a [Required] attribute was missing) and Model Binding Errors
                // that occurred during the process of mapping incoming request data (from body, route, or query string)
                // in action method parameters 
            }


            var result = await _authServices.LoginAsync(loginDto);

            if (!result.IsSuccess)
            {
                if (result.ErrorCode == "INVALID_CREDENTIALS")
                {
                    return Unauthorized(new ErrorDetails
                    {
                        status = (int)HttpStatusCode.Unauthorized,
                        message = result.Error ?? string.Empty
                    });
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

                
            }

            return Ok(result.Value);
        }


        // Log out a user
        // (client should discard their JWT)



        // Fetch details of the currently authenticated user
        // Require a valid JWT in Authorization header
        // EX: If client refreshes but a token is in local storage, grab user info based on that token
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult> GetMe()
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
            var result = await _authServices.GetUserByIdAsync(userId);

            if (result.ErrorCode == "VALID_TOKEN_NO_USER" || result.ErrorCode == "INACTIVE_USER")
            {
                    
                return NotFound(result.Error); // valid token but the user could not be found
            }

            // If session user found from claim id and is active
            return Ok(result.Value);
        }


        [HttpDelete("deleteseed/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSeedUserByEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }


        #region Helper Methods
        
        #endregion
    }
}


