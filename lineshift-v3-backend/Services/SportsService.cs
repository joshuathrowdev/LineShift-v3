using AutoMapper;
using lineshift_v3_backend.DataAccess.Repository;
using lineshift_v3_backend.Dtos.Sport;
using lineshift_v3_backend.Models;
using lineshift_v3_backend.Utils;
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
        Task<Result<Sport>> CreateSportAsync(CreateSportDto createSportDto);
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
                List<SportDto> sports = new List<SportDto>();
                foreach (var sport in result)
                { 
                    sports.Add(_mapper.Map<SportDto>(sport));
                }

                return sports;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing sport repository.");
                throw;
            }
        }

        public async Task<Result<Sport>> CreateSportAsync(CreateSportDto createSportDto)
        {
            try
            {
                var sport = _mapper.Map<Sport>(createSportDto);
                var recordsAffected = await _sportsRepository.CreateSportAsync(sport);

                if (recordsAffected < 1)
                {
                    return Result<Sport>.Failure("An error occurred while attempting to add sport", "INVALID_OPERATION");
                }
                return Result<Sport>.Success(sport);

            } catch (DbUpdateException) // database constrain violation (duplicate sport names)
            {
                return Result<Sport>.Failure($"Sport '{createSportDto.SportName}' already exist", "DATABASE_CONSTRAINT_VIOLATION");
            }
            catch (OperationCanceledException) // Async operation canceled (timeout)
            {
                return Result<Sport>.Failure("Operation canceled", "REQUEST_CANCELLED");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the sport repository.");
                return Result<Sport>.Failure("An unexpected server error occurred", "SERVER_ERROR");
            }
        }
        //DbUpdateException: Catch this to detect database constraint violations.
        //If its inner exception indicates a unique constraint violation
        //(e.g., a sport with that name already exists), map this to a specific message
        //like "DUPLICATE_SPORT_NAME". For other DbUpdateException causes
        //(like NOT NULL violations not caught by API validation), a generic
        //"DATABASE_CONSTRAINT_VIOLATION" might suffice.

        //OperationCanceledException: Catch this if asynchronous operations can be cancelled
        //(e.g., by a client timeout or token). This indicates the operation didn't complete
        //and can be mapped to a message like "REQUEST_CANCELLED".

        //Generic Exception: As a catch-all, any other unexpected Exception should be
        //caught.This signifies an unhandled server-side error, and a message like
        //"UNEXPECTED_SERVER_ERROR" is appropriate.
        #endregion


        #region Interface Implementation
        Task<ICollection<SportDto>> ISportsService.GetSportsAsync()
        {
            return GetSportsAsync();
        }

        Task<Result<Sport>> ISportsService.CreateSportAsync(CreateSportDto createSportDto)
        {
            return CreateSportAsync(createSportDto);
        }
        #endregion
    }
}
