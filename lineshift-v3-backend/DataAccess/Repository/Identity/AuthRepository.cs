using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.Models.Database;
using lineshift_v3_backend.Models.Identity;
using lineshift_v3_backend.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lineshift_v3_backend.DataAccess.Repository.Identity
{
    #region Auth Repository Interface
    public interface IAuthRepository
    {
        Task<ApplicationUser?> LoginAsync(LoginModel loginModel);
        Task<ApplicationUser> GetUserByIdAsync(string id);
    }
    #endregion


    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(
            ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ILogger<AuthRepository> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        #region Repository Methods
        public async Task<ApplicationUser?> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var applicationUser = await _userManager.FindByEmailAsync(loginModel.Email);
                if (applicationUser == null)
                {
                    _logger.LogWarning($"Application user '{loginModel.Email}' cout not be found due to invalid email crendentials");
                    return null;
                }

                return applicationUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was an exception while attempting to find '{loginModel.Email}: {ex}.'");
                throw;
            }
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            try
            {
                var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while attempting to get user by Id from Users table.");
                throw;
            }
        }
        #endregion

        #region Helper Methods
        //internal SessionUser MapSessionUser(ApplicationUser applicationUser)
        //{
        //   try
        //    {
        //        return new SessionUser
        //        {
                    
        //            UserId = applicationUser.Id,
        //            UserName = applicationUser.UserName,
        //            Email = applicationUser.Email ?? string.Empty,
        //            FirstName = applicationUser.FirstName,
        //            LastName = applicationUser.LastName,
        //            IsActive = applicationUser.IsActive,
        //            RegisteredDate = applicationUser.RegisteredDate,
        //            LastLoginDate = applicationUser.LastLoginDate,
        //            LastUpdatedDate = applicationUser.LastUpdatedDate,
        //            SubscriptionTier = applicationUser.SubscriptionTier,
        //            Roles = null
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogWarning(exception: ex, message: "An error occured while attempting to map an application user to session user.");
        //        throw;
        //    }

        //}


        #region Interface Implementation
        Task<ApplicationUser?> IAuthRepository.LoginAsync(LoginModel loginModel)
        {
            return LoginAsync(loginModel);
        }
        #endregion

        Task<ApplicationUser> IAuthRepository.GetUserByIdAsync(string id)
        {
            return GetUserByIdAsync(id);
        }
        #endregion
    }
}
