using AutoMapper;
using lineshift_v3_backend.DataAccess.Repository.Identity;
using lineshift_v3_backend.Dtos;
using lineshift_v3_backend.Dtos.Identity;
using lineshift_v3_backend.Models;
using lineshift_v3_backend.Models.Auth;
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
        Task<AuthResponse<AuthDto>> LoginAsync(LoginDto loginDto);
        Task<AuthResponse<SessionUser>> GetUserByIdAsync(string id);
    }
    #endregion


    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthServices> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AuthServices(
            IAuthRepository authRepository, 
            ILogger<AuthServices> logger, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenServices tokenServices,
            IMapper mapper)
        {
            _authRepository = authRepository;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }


        public async Task<AuthResponse<AuthDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var userEntity = await _authRepository.LoginAsync(loginDto);
                if (userEntity == null) // email doesn't exist
                {
                    return AuthResponse<AuthDto>.Failure("Invalid Credentials.", "INVALID_CREDENTIALS");
                }

                if (!userEntity.IsActive || userEntity.IsDeleted)
                {
                    return AuthResponse<AuthDto>.Failure("Inactive or Deleted Account.", "INACTIVE_ACCOUNT");
                }

                var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(userEntity, loginDto.Password, lockoutOnFailure: true);
                if (!isPasswordValid.Succeeded)
                {
                    return AuthResponse<AuthDto>.Failure("Invalid Credentials.", "INVALID_CREDENTIALS");
                }

                if (isPasswordValid.IsLockedOut)
                {
                    return AuthResponse<AuthDto>.Failure("Locked Out Account.", "LOCKEDOUT_ACCOUNT");
                }

                if (isPasswordValid.IsNotAllowed)
                {
                    return AuthResponse<AuthDto>.Failure("Not Allowed to Sign In.", "NOT_ALLOWED_ACCOUNT");
                }

                // Update Last Login Dater
                userEntity.LastLoginDate = DateTimeOffset.UtcNow;
                await _userManager.UpdateAsync(userEntity);

                // Generate JWT Token
                var jwt_token = await _tokenServices.GenerateJwtToken(userEntity);

                // Making Session User
                var sessionUser = _mapper.Map<SessionUser>(userEntity);
                sessionUser.Roles = (await _userManager.GetRolesAsync(userEntity)).ToList();


                var authDto = new AuthDto
                {
                    Token = jwt_token,
                    SessionUser = sessionUser
                };

                return AuthResponse<AuthDto>.Success(authDto);
            
            } catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error attempting to access the user repository.");
                throw;
            }
        }

        public async Task<AuthResponse<SessionUser>> GetUserByIdAsync(string id)
        {
            try
            {
                var userEntity = await _authRepository.GetUserByIdAsync(id);

                if (userEntity == null)
                {
                    // This indicated a discrepancy where a valid token was issued but the user was not
                    // found from this token (maybe the token was issues and the user got deleted)
                    
                    _logger.LogWarning($"UserId '{id}' could not be found from Claim Types.");
                    return AuthResponse<SessionUser>.Failure("User could not be found.", "VALID_TOKEN_NO_USER");
                }

                if (!userEntity.IsActive)
                {
                    // this means that a valid token was issues but the user got suspended or
                    // for whatever reason their account is not not active or locked

                    //_logger.LogWarning($"UserId '{id}' is currently not active.");
                    return AuthResponse<SessionUser>.Failure("User could not be found.", "INACTIVE_USER");
                }

                var sessionUser = _mapper.Map<SessionUser>(userEntity);
                sessionUser.Roles = (await _userManager.GetRolesAsync(userEntity)).ToList();

                return AuthResponse<SessionUser>.Success(sessionUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while attempting to access the user repository.");
                throw;
            }
        }

        #region Helper Methods

        #endregion
    }
}
