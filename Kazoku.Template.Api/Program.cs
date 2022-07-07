using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Kazoku.Template.Api.SwaggerOptions;

var builder = WebApplication.CreateBuilder(args);

// Variables
string apiName = "Kazoku.Template.Api";

// Versioning
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = ApiVersion.Parse("2022-06-01");
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new QueryStringApiVersionReader();
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "G";
    options.DefaultApiVersionParameterDescription = "API Version in YYYY-MM-DD format";
});

// Swagger options
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(xmlFilePath);
});


// Logging
builder.Logging.ClearProviders();
builder.Services.AddLogging(options =>
{
    options.AddSimpleConsole(c =>
    {
        c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
        // Remove comments to use UTC time.
        //c.UseUtcTimestamp = true; 
    });
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();

// Building the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Logger.LogWarning($"--- ALERT: {apiName} is in development mode. ---");
}

// Using Swagger UI
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        var title = $"{apiName} version {desc.ApiVersion}";

        options.DocumentTitle = title;
        options.SwaggerEndpoint($"{desc.GroupName}/swagger.json", title);
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    }
});

// Final setup
app.UseAuthorization();
app.MapControllers();
app.Logger.LogInformation($"{apiName} is now running.");
app.Logger.LogInformation($"API Template developed by Kazoku IT AB.");
app.Logger.LogInformation($"More info can be found here: https://github.com/kazokuit/Kazoku.Template.Api.");

// Running application
app.Run();
