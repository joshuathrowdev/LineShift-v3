using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.Models.Database;
using lineshift_v3_backend.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lineshift_v3_backend.DataAccess.Repository.Identity
{
    #region Auth Repository Interface
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
    }
    #endregion


    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(ApplicationDbContext dbContext, ILogger<AuthRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        #region Repository Methods
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

        #region Interface Implementation
        Task<ApplicationUser> IAuthRepository.GetUserByIdAsync(string id)
        {
            return GetUserByIdAsync(id);
        }
        #endregion
    }
}
