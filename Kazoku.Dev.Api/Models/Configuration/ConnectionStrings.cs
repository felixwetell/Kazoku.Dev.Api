namespace Kazoku.Dev.Api.Models.Configuration
{
    public class ConnectionStrings
    {
        public class ConnectionStringsConfig
        {
            /// <summary>
            /// Key to the connection strings in the settings file
            /// </summary>
            public const string Key = "ConnectionStrings";

            /// <summary>
            /// Database connection string
            /// </summary>
            /// 
            public string Database { get; set; } = string.Empty;
        }
    }
}
