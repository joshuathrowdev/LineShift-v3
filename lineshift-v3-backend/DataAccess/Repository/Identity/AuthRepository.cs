using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.Models.Database;
using lineshift_v3_backend.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace lineshift_v3_backend.DataAccess.Repository.Identity
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public AuthRepository(ApplicationDbContext dbContext, ILogger logger)
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
    }


    #region Auth Repository Interface
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
    }
    #endregion
}
