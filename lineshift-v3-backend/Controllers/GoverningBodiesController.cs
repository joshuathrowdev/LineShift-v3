using lineshift_v3_backend.Dtos.GoverningBody;
using lineshift_v3_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace lineshift_v3_backend.Controllers
{
    [Route("api/v3/governing-bodies")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize]
    [ApiController]
    public class GoverningBodiesController : ControllerBase
    {
        private readonly IGoverningBodiesServices _governingBodiesServices;

        public GoverningBodiesController(IGoverningBodiesServices governingBodiesServices)
        {
            _governingBodiesServices = governingBodiesServices;
        }

        #region Resource Routes
        [HttpGet("")]
        public async Task<ActionResult<ICollection<GoverningBodyDto>>> GetGoverningBodies()
        {
            try
            {
                var governingBodies = await _governingBodiesServices.GetGoverningBodiesAsync();
                return Ok(governingBodies);
            
            } catch (Exception ex)
            {
                throw;
            }

        }
        #endregion
    }
}
