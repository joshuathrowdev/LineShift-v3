using lineshift_v3_backend.DataAccess.Repository.Identity;
using lineshift_v3_backend.Models.Database;
using lineshift_v3_backend.Models.Identity;
using lineshift_v3_backend.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lineshift_v3_backend.Services.Identity
{
    #region Auth Service Interface
    public interface IAuthServices
    {
        Task<SessionUser> GetUserByIdAsync(string id);
        Task<Result<AuthResponse>> LoginAsync(LoginModel loginModel);
    }
    #endregion


    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthServices> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthServices(
            IAuthRepository authRepository, 
            ILogger<AuthServices> logger, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _authRepository = authRepository;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        #region Service Methods
        public async Task<Result<AuthResponse>> LoginAsync(LoginModel loginModel)
        {
            var userEntity = await _authRepository.LoginAsync(loginModel);
            if (userEntity == null) // email doesnt exist
            {

                return Result<AuthResponse>.Failure("Invalid Credentials.", "INVALID_CREDENTIALS");
            }

            if (!userEntity.IsActive || userEntity.IsDeleted)
            {
                return Result<AuthResponse>.Failure("Inactive or Deleted Account", "INACTIVE_ACCOUNT");
            }

            var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(userEntity, loginModel.Password, lockoutOnFailure: true);
            if(!isPasswordValid.Succeeded)
            {
                return Result<AuthResponse>.Failure("Invalid Credentials.", "INVALID_CREDENTIALS");
            }

            if (isPasswordValid.IsLockedOut)
            {
                return Result<AuthResponse>.Failure("Locked Out Account", "LOCKEDOUT_ACCOUNT");
            }

            if (isPasswordValid.IsNotAllowed)
            {
                return Result<AuthResponse>.Failure("Not Allowed to Sign IN", "NOT_ALLOWED_ACCOUNT");
            }

            // Update Last Login Dater
            userEntity.LastLoginDate = DateTimeOffset.UtcNow;
            await _userManager.UpdateAsync(userEntity);

            // Generate JWT Token
            var jwt_token = await GenerateJwtToken(userEntity);

            // Making Session User
            var sessionUser = await MapSessionUser(userEntity);

            var authResponse = new AuthResponse
            {
                Token = jwt_token,
                SessionUser =  sessionUser
            };

            return Result<AuthResponse>.Success(authResponse);
        }

        public async Task<SessionUser> GetUserByIdAsync(string id)
        {
            try
            {
                var userEntity = await _authRepository.GetUserByIdAsync(id);

                if (userEntity == null)
                {
                    // This indicated a discrepancy where a valid token was issued but the user was not
                    // found from this token (mayhe the token was issues and the user got deleted)
                    return null;
                }

                if (!userEntity.IsActive)
                {
                    // this mean that a valid token was issues but the user got suspended or
                    // for whatever reason their account is not not active or locked
                    return null;
                }

                var sessionUser = await MapSessionUser(userEntity);

                return sessionUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Thee was an error while attempting to access the user repository.");
                throw;
            }
        }

        #region Helper Methods
        internal async Task<SessionUser> MapSessionUser(ApplicationUser userEntity)
        {
            try
            {
                return new SessionUser
                {
                    UserId = userEntity.Id,
                    Email = userEntity.Email ?? string.Empty,
                    UserName = userEntity.UserName ?? string.Empty,
                    FirstName = userEntity.FirstName ?? string.Empty,
                    LastName = userEntity.LastName ?? string.Empty,
                    IsActive = userEntity.IsActive,
                    RegisteredDate = userEntity.RegisteredDate,
                    LastLoginDate = userEntity.LastLoginDate,
                    LastUpdatedDate = userEntity.LastUpdatedDate,
                    SubscriptionTier = userEntity.SubscriptionTier,
                    Roles = (await _userManager.GetRolesAsync(userEntity)).ToList(),
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        internal async Task<string> GenerateJwtToken(ApplicationUser userEntity)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique Id for the token
                new Claim(ClaimTypes.NameIdentifier, userEntity.Id), // Standard Claim for User ID
                new Claim(ClaimTypes.Name, userEntity.UserName ?? userEntity.UserName ?? ""), // Standard Claim for Username
                new Claim(ClaimTypes.Email, userEntity.Email ?? ""), // Standard Claim for User Email
            };

            var roles = await _userManager.GetRolesAsync(userEntity);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
