using lineshift_v3_backend.Dtos.League;
using lineshift_v3_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace lineshift_v3_backend.Controllers
{
    [Route("api/v3/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeaguesServices _leaguesServices;

        public LeaguesController(ILeaguesServices leaguesServices)
        {
            _leaguesServices = leaguesServices;
        }

        #region Resource Routes
        public async Task<ActionResult<ICollection<LeagueDto>>> GetLeagues()
        {
            try
            {
                var leagues = await _leaguesServices.GetLeaguesAsync();
                return Ok(leagues);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
