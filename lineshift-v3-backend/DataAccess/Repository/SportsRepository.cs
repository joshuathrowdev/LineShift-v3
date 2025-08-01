﻿
using lineshift_v3_backend.Dtos.Sport;
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
        Task<int> CreateSportAsync(Sport sport);
    }
    #endregion
    public class SportsRepository : ISportsRepository
    {
        // we have to pass the repository layer the DbContext so the Query Layer
        // can interact and query the database
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SportsRepository> _logger;

        public SportsRepository(ApplicationDbContext context, ILogger<SportsRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        #region Database Calls
        public async Task<ICollection<Sport>> GetSportsAsync()
        {
            try
            {
                var result = await _dbContext.Sports.AsNoTracking().ToListAsync<Sport>();
                return result;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred when accessing the sports queries layer");
                throw; 
            }
        }

        public async Task<int> CreateSportAsync(Sport sport)
        {
            await _dbContext.Sports.AddAsync(sport);
            var recordSaved = await _dbContext.SaveChangesAsync();

            return recordSaved;
        }
        #endregion


        #region Interface Implementation
        Task<ICollection<Sport>> ISportsRepository.GetSportsAsync()
        {
            return GetSportsAsync();
        }

        Task<int> ISportsRepository.CreateSportAsync(Sport sport)
        {
            return CreateSportAsync(sport);
        }
        #endregion
    }
}
