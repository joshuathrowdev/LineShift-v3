using AutoMapper;
using lineshift_v3_backend.DataAccess.Repository;
using lineshift_v3_backend.Dtos.League;
using lineshift_v3_backend.Dtos.Sport;

namespace lineshift_v3_backend.Services
{
    public interface ILeaguesServices
    {
        Task<ICollection<LeagueDto>> GetLeaguesAsync();
    }
    public class LeaguesServices : ILeaguesServices
    { 
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly ILogger<LeaguesServices> _logger;
        private readonly IMapper _mapper;

        public LeaguesServices(
            ILeaguesRepository leaguesRepository, 
            ILogger<LeaguesServices> logger,
            IMapper mapper)
        {
            _leaguesRepository = leaguesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        #region Methods
        async Task<ICollection<LeagueDto>> GetLeaguesAsync()
        {
            try
            {
                var result = await _leaguesRepository.GetLeaguesAsync();

                var leagues = result.Select(league => _mapper.Map<LeagueDto>(league)).ToList();
                return leagues;

            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred while accessing the leagues repository");
                throw;
            }
        }
        #endregion

        #region Interface Stuff
        Task<ICollection<LeagueDto>> ILeaguesServices.GetLeaguesAsync()
        {
            return GetLeaguesAsync();
        }
        #endregion

    }


}
