using lineshift_v3_backend.DataAccess.Repository;
using lineshift_v3_backend.Dtos;
using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.Services
{
    #region Service Interface
    // Business Logic Layer (All logic that we need to implement goes here)
    // Return type: ICollection (allowed to modify data)
    public interface ISportsService
    {
        Task<ICollection<SportDto>> GetSportsAsync();
    }
    #endregion
    public class SportsService : ISportsService
    {
        // Layer Vars
        private readonly ISportsRepository _sportsRepository;
        private readonly ILogger<SportsService> _logger;

        // DI constructor for vars
        public SportsService(ISportsRepository sportsRepository, ILogger<SportsService> logger)
        {
            _sportsRepository = sportsRepository;
            _logger = logger;
        }

        #region Methods
        public async Task<ICollection<SportDto>> GetSportsAsync()
        {
            try
            {
                var result = await _sportsRepository.GetSportsAsync();
                return MapSportDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing sport repository");
                throw;
            }
        }
        #endregion

        #region Helper Methods
        private ICollection<SportDto> MapSportDto(ICollection<Sport> sports)
        {
            List<SportDto> mappedSports = new List<SportDto>();

            foreach (var sport in sports)
            {
                mappedSports.Add(new SportDto
                {
                    SportId = sport.SportId,
                    SportName = sport.SportName,
                    Description = sport.Description,
                    Type = sport.Type
                });
            }

            return mappedSports;
        }
        #endregion


        #region Interface Implementation
        Task<ICollection<SportDto>> ISportsService.GetSportsAsync()
        {
            return GetSportsAsync();
        }
        #endregion
    }
}
