using AutoMapper;
using lineshift_v3_backend.DataAccess.Repository;
using lineshift_v3_backend.Dtos.GoverningBody;

namespace lineshift_v3_backend.Services
{
    public interface IGoverningBodiesServices
    {

        Task<ICollection<GoverningBodyDto>> GetGoverningBodiesAsync();
    }

    public class GoverningBodiesServices : IGoverningBodiesServices
    {
        private readonly IGoverningBodiesRepository _governingBodiesRepository;
        private readonly ILogger<GoverningBodiesServices> _logger;
        private readonly IMapper _mapper;

        public GoverningBodiesServices(
            IGoverningBodiesRepository governingBodiesRepository, 
            ILogger<GoverningBodiesServices> logger,
            IMapper mapper)
        {
            _governingBodiesRepository = governingBodiesRepository;
            _logger = logger;
            _mapper = mapper;
            
        }

        #region Methods
        public async Task<ICollection<GoverningBodyDto>> GetGoverningBodiesAsync()
        {
            try
            {
                var result = await _governingBodiesRepository.GetGoverningBodiesAsync();

                List<GoverningBodyDto> governingBodies = new List<GoverningBodyDto>();
                foreach (var governingBody in result)
                {
                    governingBodies.Add(_mapper.Map<GoverningBodyDto>(governingBody));
                }

                return governingBodies;
            
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing governing bodies repository");
                throw;
            }
        }
        #endregion


        #region Interface Implementation
        Task<ICollection<GoverningBodyDto>> IGoverningBodiesServices.GetGoverningBodiesAsync()
        {
            return GetGoverningBodiesAsync();
        }
        #endregion
    }
}
