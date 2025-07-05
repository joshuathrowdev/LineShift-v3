using lineshift_v3_backend.DataAccess.Repository.Identity;
using lineshift_v3_backend.Models.Database;
using lineshift_v3_backend.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace lineshift_v3_backend.Services.Identity
{
    #region Auth Service Interface
    public interface IAuthServices
    {
        Task<SessionUser> GetUserByIdAsync(string id);
    }
    #endregion


    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthServices> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthServices(IAuthRepository authRepository, ILogger<AuthServices> logger, UserManager<ApplicationUser> userManager)
        {
            _authRepository = authRepository;
            _logger = logger;
            _userManager = userManager;
        }

        #region Service Methods
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

                var sessionUser = new SessionUser
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
                    SubscriptionTier = userEntity.SubscriptionTier
                };

                var roles = await _userManager.GetRolesAsync(userEntity);
                sessionUser.Roles = roles.ToList();

                return sessionUser;
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Thee was an error while attempting to access the user repository.");
                throw;
            }
        }
        #endregion


        #region Interface Implementaion
        Task<SessionUser> IAuthServices.GetUserByIdAsync(string id)
        {
            return GetUserByIdAsync(id);
        }
        #endregion
    }
}
