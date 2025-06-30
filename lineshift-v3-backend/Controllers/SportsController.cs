using lineshift_v3_backend.Models;
using lineshift_v3_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace lineshift_v3_backend.Controllers
{
    // Controller Layer for Resource
    // Return Type: ICollection (for extended collection methods)
    [Route("api/v3/[Controller]")]
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
        public async Task<ActionResult<ICollection<Sport>>> GetSports()
        {
            try
            {
                var sports = await _sportsService.GetSportsAsync();
                return Ok(sports);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
