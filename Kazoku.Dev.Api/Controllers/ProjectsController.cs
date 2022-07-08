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
            _logger.LogInformation("Tries to get projects.");
            try
            {
                List<Project> projects = await _projectService.GetProjectsAsync();
                _logger.LogInformation("Fetch was successfull, returning with list of projects.");
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Failed to fetch projects. See error below.");
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            _logger.LogInformation("Tries to get project.");
            try
            {
                Project project = await _projectService.GetProjectAsync(id);
                _logger.LogInformation("Fetch was successfull, returning with project.");
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Failed to fetch project. See error below.");
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
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
