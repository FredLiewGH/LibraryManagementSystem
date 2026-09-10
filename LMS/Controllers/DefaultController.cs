using LMS.Services.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        [Route("health")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> HealthCheck(CancellationToken token = default)
        {
            return Ok();
        }
    }
}
