using lineshift_v3_backend.DataAccess.Repository;
using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.Services
{
    #region Service Interface
    // Business Logic Layer (All logic that we need to implement goes here)
    // Return type: ICollection (allowed to modify data)
    public interface ISportsService
    {
        Task<ICollection<Sport>> GetSportsAsync();
    }
    #endregion
    public class SportsService : ISportsService
    {
        // Layer Vars
        private readonly ISportsRepository _sportsRepository;

        // DI constructor for vars
        public SportsService(ISportsRepository sportsRepository)
        {
            _sportsRepository = sportsRepository;
        }

        #region Methods
        public async Task<ICollection<Sport>> GetSportsAsync()
        {
            try
            {
                var result = await _sportsRepository.GetSportsAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region Interface Implementation
        Task<ICollection<Sport>> ISportsService.GetSportsAsync()
        {
            return GetSportsAsync();
        }
        #endregion
    }
}
