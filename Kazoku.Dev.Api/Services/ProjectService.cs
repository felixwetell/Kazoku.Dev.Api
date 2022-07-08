using Dapper;
using Kazoku.Common.Models.Kazoku.Dev;
using Kazoku.Dev.Api.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Kazoku.Dev.Api.Services
{
    public class ProjectService
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        public ProjectService(
            ILogger<ProjectService> logger,
            IOptions<ConnectionStrings> connectionStrings)
        {
            _logger = logger;
            _connectionStrings = connectionStrings;
        }

        /// <summary>
        /// Gets projects from database async.
        /// </summary>
        /// <returns>List of projects.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Project>> GetProjectsAsync()
        {
            List<Project> projects = new List<Project>();
            
            _logger.LogDebug("Initializing SQL connection");
            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=KazokuDevDb;Integrated Security=SSPI"))
            {
                var sql = "SELECT * FROM projects";
                
                _logger.LogDebug("Opens SQL connection.");
                connection.Open();

                _logger.LogDebug("Tries to execute query on SQL connection.");
                var result = await connection.QueryAsync<Project>(sql);

                _logger.LogDebug("Loops through result list and adds projects to project list.");
                foreach (var project in result)
                {
                    projects.Add(project);
                }

                _logger.LogInformation($"Returns list of projects containing {projects.Count} objects.");
            };
            return projects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Project> GetProjectAsync(Guid id)
        {
            _logger.LogDebug("Initializing SQL connection");
            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=KazokuDevDb;Integrated Security=SSPI"))
            {
                var sql = $"SELECT * FROM projects WHERE id = '{id}'";

                _logger.LogDebug("Opens SQL connection.");
                connection.Open();

                _logger.LogDebug("Tries to execute query on SQL connection.");
                var result = await connection.QuerySingleAsync<Project>(sql);

                Project project = result;

                return project;
            };
        }
    }
}
