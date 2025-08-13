using AutoMapper;
using lineshift_v3_backend.DataAccess.Repository;
using lineshift_v3_backend.Dtos.Sport;
using lineshift_v3_backend.Exceptions;
using lineshift_v3_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lineshift_v3_backend.Services
{
    #region Service Interface
    // Business Logic Layer (All logic that we need to implement goes here)
    // Return type: ICollection (allowed to modify data)
    public interface ISportsService
    {
        Task<ICollection<SportDto>> GetSportsAsync();
        Task<Sport> CreateSportAsync(CreateSportDto createSportDto);
    }
    #endregion
    public class SportsService : ISportsService
    {
        // Layer Vars
        private readonly ISportsRepository _sportsRepository;
        private readonly ILogger<SportsService> _logger;
        private readonly IMapper _mapper;

        // DI constructor for vars
        public SportsService(
            ISportsRepository sportsRepository, 
            ILogger<SportsService> logger,
            IMapper mapper)
        {
            _sportsRepository = sportsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        #region Methods
        public async Task<ICollection<SportDto>> GetSportsAsync()
        {
            try
            {
                var result = await _sportsRepository.GetSportsAsync();

                // Mapping Sport Model
                //List<SportDto> sports = new List<SportDto>();
                //foreach (var sport in result)
                //{ 
                //    sports.Add(_mapper.Map<SportDto>(sport));
                //}

                var sports = result.Select(sport => _mapper.Map<SportDto>(sport)).ToList();
                return sports;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing sport repository.");
                throw;
            }
        }

        public async Task<Sport> CreateSportAsync(CreateSportDto createSportDto)
        {
            try
            {
                var sport = _mapper.Map<Sport>(createSportDto);
                var recordsAffected = await _sportsRepository.CreateSportAsync(sport);

                if (recordsAffected < 1)
                {
                    throw new ResourceCreationException("sport");
                }

                return sport;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Sports_SportName"))
                {
                    throw new DuplicateResourceException($"The sport '{createSportDto.SportName}' already exist.", ex);
                }
                else
                {
                    // For all other DbUpdateExceptions, throw a more generic exception.
                    //  A DbUpdateException could be caused by many things
                    //  (e.g., a data type mismatch, a foreign key constraint), not just a duplicate.
                    throw new DatabaseOperationException(ex);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the sport repository.");
                throw;
            }
        }
        #endregion


        #region Interface Implementation
        Task<ICollection<SportDto>> ISportsService.GetSportsAsync()
        {
            return GetSportsAsync();
        }

        Task<Sport> ISportsService.CreateSportAsync(CreateSportDto createSportDto)
        {
            return CreateSportAsync(createSportDto);
        }
        #endregion
    }
}
