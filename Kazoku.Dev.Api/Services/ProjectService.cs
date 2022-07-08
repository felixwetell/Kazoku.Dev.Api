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
                var sql = "SELECT * FROM [dbo].[Projects]";
                
                _logger.LogDebug("Opens SQL connection.");
                connection.Open();

                try
                {
                    _logger.LogDebug("Tries to execute query on SQL connection.");
                    var result = await connection.QueryAsync<Project>(sql);
                    
                    _logger.LogDebug("Loops through result list and adds projects to project list.");
                    foreach (var project in result)
                    {
                        projects.Add(project);
                    }
                 
                    _logger.LogInformation($"Returns list of projects containing {projects.Count} objects.");
                    return projects;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    throw new Exception(ex.ToString());
                }
            };
        }

        /// <summary>
        /// Gets a projects based on Id from database async. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Project> GetProjectAsync(Guid id)
        {
            _logger.LogDebug("Initializing SQL connection");
            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=KazokuDevDb;Integrated Security=SSPI"))
            {
                var sql = $"SELECT * FROM [dbo].[Projects] WHERE [Id] = '{id}'";

                _logger.LogDebug("Opens SQL connection.");
                connection.Open();

                try
                {
                    _logger.LogDebug("Tries to execute query on SQL connection.");
                    var result = await connection.QuerySingleAsync<Project>(sql);
                    
                    _logger.LogInformation("Returns project object.");
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    throw new Exception(ex.ToString());
                }
            };
        }

        /// <summary>
        /// Creates project in database async.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>New project object.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Project> CreateProjectAsync(Project project)
        {
            _logger.LogDebug("Initializing SQL connection");
            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=KazokuDevDb;Integrated Security=SSPI"))
            {
                string sql = @"INSERT INTO [dbo].[Projects]([Name], [Image], [Description], [Status], [Url], [Created], [Updated], [Deleted], [Views], [Shares]) 
                               OUTPUT INSERTED.Id 
                               VALUES (@Name, @Image, @Description, @Status, @Url, @Created, @Updated, @Deleted, @Views, @Shares)";

                _logger.LogDebug("Opens SQL connection.");
                connection.Open();

                try
                {
                    await connection.ExecuteAsync(sql, project);
                    return project;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Updates an existing project in the database async.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Updated project object.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Project> UpdateProjectAsync(Project project)
        {
            _logger.LogDebug("Initializing SQL connection");
            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=KazokuDevDb;Integrated Security=SSPI"))
            {
                string sql = @"UPDATE [dbo].[Projects] SET 
                              [Name] = @Name, [Image] = @Image, [Description] = @Description, [Status] = @Status, [Url] = @Url, 
                              [Created] = @Created, [Updated] = @Updated, [Deleted] = @Deleted, [Views] = @Views, [Shares] = @Shares";

                _logger.LogDebug("Opens SQL connection.");
                connection.Open();

                try
                {
                    await connection.ExecuteAsync(sql, project);
                    return project;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Deletes a project from database async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteProjectAsync(Guid id)
        {
            _logger.LogDebug("Initializing SQL connection");
            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=KazokuDevDb;Integrated Security=SSPI"))
            {
                var sql = $"DELETE FROM [dbo].[Projects] WHERE [Id] = '{id}'";

                _logger.LogDebug("Opens SQL connection.");
                connection.Open();

                try
                {
                    _logger.LogDebug("Tries to execute query on SQL connection.");
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    throw new Exception(ex.ToString());
                }
            };
        }
    }
}
