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
        /// Gets all projects.
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

        /// <summary>
        /// Gets project based on the Id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project object.</returns>
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

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Newly created project</returns>
        [HttpPost]
        public async Task<ActionResult<Project>> PostProjet([FromBody] Project project)
        {
            _logger.LogInformation("Creating project started.");

            try
            {
                project = await _projectService.CreateProjectAsync(project);
                _logger.LogInformation("Fetch was successfull, returning with project.");
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Failed to create project. See error below.");
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public void PutProjects(Guid id, [FromBody] Project project)
        {
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            _logger.LogDebug($"Fetching project with id {id}.");
            Project project = await _projectService.GetProjectAsync(id);

            _logger.LogDebug("Checks if project was found.");
            if (project is null)
            {
                _logger.LogWarning("No projet was found.");
                return NotFound();
            }

            try
            {
                _logger.LogDebug("Starting to delete project.");
                await _projectService.DeleteProjectAsync(id);

                _logger.LogInformation($"Project with id {id} was deleted.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Failed to delete project. See error below.");
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
