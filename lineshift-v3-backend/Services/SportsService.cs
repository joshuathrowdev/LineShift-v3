using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.Services
{
    #region Service Interface
    public interface ISportsService
    {
        Task<ICollection<Sport>> GetSports();
    }
    #endregion
    public class SportsService : ISportsService
    {
        // Layer Vars
        private readonly ISportsService _sportsService;

        // DI constructor for vars
        public SportsService(ISportsService sportsService)
        {
            _sportsService = sportsService;
        }

        #region Methods
        public async Task<ICollection<Sport>> GetSports()
        {
            try
            {
                // Call Next Layer (Repository Layer) (Actual LINQ Code base)
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region Interface Implementation
        Task<ICollection<Sport>> ISportsService.GetSports()
        {
            return GetSports();
        }
        #endregion
    }
}
