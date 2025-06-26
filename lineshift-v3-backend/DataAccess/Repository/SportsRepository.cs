using lineshift_v3_backend.DataAccess.Queries;
using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace lineshift_v3_backend.DataAccess.Repository
{
    // Data Access Layer (should be lean and only focused on returning data that we need
    // Return Type: IEnnumerable (not modifying the data)
    #region Repository Interface
    public interface ISportsRepository
    {
        Task<ICollection<Sport>> GetSportsAsync();
    }
    #endregion
    public class SportsRepository : ISportsRepository
    {
        // we have to pass the repository layer the DbContext so the Query Layer
        // can interact and query the database
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SportsRepository> _logger;

        public SportsRepository(ApplicationDbContext context, ILogger<SportsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Methods
        public async Task<ICollection<Sport>> GetSportsAsync()
        {
            try
            {
                var result = await _context.Sports.GetSportsAsync().AsNoTracking().ToListAsync();
                _logger.LogInformation("Accessing the sport queries from the sport repo layer");
                return result;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An occured has occured when accessing the sports queries layer");
                throw; 
            }
        }
        #endregion


        #region Interface Implementation
        Task<ICollection<Sport>> ISportsRepository.GetSportsAsync()
        {
            return GetSportsAsync();
        }
        #endregion
    }
}
