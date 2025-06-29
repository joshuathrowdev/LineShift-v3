using lineshift_v3_backend.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace lineshift_v3_backend.Controllers.Identity
{
    [ApiController]
    [Route("[Contoller]")]
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



        // Log out a user
        // (client should discard their JWT)



        // Fetch detaills of the currently authenticated user
        // Require a valid JWT in Authirization header

    }

    // Helper Method that generate JWT for a given ApplicationUser
}
