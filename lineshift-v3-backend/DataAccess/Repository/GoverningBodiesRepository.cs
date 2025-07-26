using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace lineshift_v3_backend.DataAccess.Repository
{
    public interface IGoverningBodiesRepository
    {
        Task<List<GoverningBody>> GetGoverningBodiesAsync();
    }
    public class GoverningBodiesRepository : IGoverningBodiesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GoverningBodiesRepository> _logger;

        public GoverningBodiesRepository(ApplicationDbContext dbContext, ILogger<GoverningBodiesRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Database Calls
        public async Task<List<GoverningBody>> GetGoverningBodiesAsync()
        {
            try
            {
                var result = await _dbContext.GoverningBodies.AsNoTracking().ToListAsync<GoverningBody>();
                return result;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when accessing the governing bodies queries layer");
                throw;
            }
        }

        #endregion
    }
}
