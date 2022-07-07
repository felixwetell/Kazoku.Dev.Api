using Kazoku.Common.Models.Kazoku.Dev;
using Kazoku.Dev.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kazoku.Dev.Api.Controllers
{
    [Route("api/projects")]
    [ApiVersion("2022-07-01")]
    [ApiController]
    public class ProjectsController : BaseApiController
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly ProjectService _projectService;

        public ProjectsController(
            ILogger<ProjectsController> logger, 
            ProjectService projectService)
        {
            _logger = logger;
            _projectService = projectService;
        }



        /// <summary>
        /// Gets projects.
        /// </summary>
        /// <returns>List of projects</returns>
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            try
            {
                List<Project> list = await _projectService.GetProjectsAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public string GetProject(int id)
        {
            return "value";
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public void PostProjet([FromBody] string value)
        {
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public void PutProjects(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public void DeleteProject(int id)
        {
        }
    }
}
