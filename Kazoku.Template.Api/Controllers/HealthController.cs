using Microsoft.AspNetCore.Mvc;

namespace Kazoku.Template.Api.Controllers
{
    /// <summary>
    /// Health controller.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiVersion("2022-06-01")]
    public class HealthController : BaseApiController
    {
        private readonly ILogger<HealthController> _logger;

        /// <summary>
        /// Health controller constructor.
        /// </summary>
        /// <param name="logger"></param>
        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Health check endpoint.
        /// </summary>
        /// <returns>Status code 200.</returns>
        [HttpGet]
        public ActionResult GetHealth()
        {
            _logger.LogInformation("Server is responding.");
            return Ok();
        }
    }
}
