using Microsoft.AspNetCore.Mvc;

namespace Kazoku.Dev.Api.Controllers
{
    /// <summary>
    /// Base API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Base API controller constructor.
        /// </summary>
        public BaseApiController() : base()
        {

        }
    }
}
