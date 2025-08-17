using lineshift_v3_backend.Dtos.Sport;
using lineshift_v3_backend.Exceptions;
using lineshift_v3_backend.Models;
using lineshift_v3_backend.Models.Errors;
using lineshift_v3_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mime;

namespace lineshift_v3_backend.Controllers
{
    // Controller Layer for Resource
    // Return Type: ICollection (for extended collection methods)
    [Route("api/v3/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize]
    [ApiController]
    public class SportsController : ControllerBase
    {
        // Have to declare vars that the class is going to use
        // Dependent Service for the Controller
        private readonly ISportsService _sportsService;

        // Constructor
        public SportsController(ISportsService sportsService)
        {
            _sportsService = sportsService;
        }

        #region Resource Routes
        [HttpGet("")]
        public async Task<ActionResult<ICollection<SportDto>>> GetSports()
        {
            var sports = await _sportsService.GetSportsAsync();
            return Ok(sports);
        }


        [HttpPost("")]
        public async Task<ActionResult> CreateSportAsync([FromBody] CreateSportDto createSportDto)
        {
            var result = await _sportsService.CreateSportAsync(createSportDto);

            return Ok(result);
        }
        #endregion
    }
}
