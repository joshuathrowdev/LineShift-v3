using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace lineshift_v3_backend.DataAccess.Repository
{
    public interface ILeaguesRepository
    {
        Task<List<League>> GetLeaguesAsync();
    }
    public class LeaguesRepository : ILeaguesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<LeaguesRepository> _logger;

        public LeaguesRepository(ApplicationDbContext dbContext, ILogger<LeaguesRepository> logger) {
            _dbContext = dbContext;
            _logger = logger; 
        }

        #region Database Calls 
        public async Task<List<League>> GetLeaguesAsync()
        {
            try
            {
                var result = await _dbContext.Leagues.AsNoTracking().ToListAsync<League>();
                return result;
            
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when accessing the leagues queries layer");
                throw;
            }
        }
        #endregion

        #region Interface Stuff
        Task<List<League>> ILeaguesRepository.GetLeaguesAsync()
        {
            return GetLeaguesAsync();
        }
        #endregion
    }
}
