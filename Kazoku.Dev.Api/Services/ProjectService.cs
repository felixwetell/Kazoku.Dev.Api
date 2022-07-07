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

            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=KazokuDevDb;Integrated Security=SSPI"))
            {
                var sql = "select * from projects";
                connection.Open();

                var queryProjects = connection.Query<Project>(sql);

                foreach (var project in queryProjects)
                {
                    projects.Add(project);
                }
            };

            return projects;
        }
    }
}
