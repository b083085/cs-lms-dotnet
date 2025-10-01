using Capstone.LMS.Application;
using Capstone.LMS.Infrastructure;
using Capstone.LMS.Infrastructure.Cors;
using Capstone.LMS.Persistence;
using Capstone.LMS.Presentation;
using Carter;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
var host = builder.Host;

services
    .AddOpenApi()
    .AddApplication(config)
    .AddInfrastructure(config)
    .AddPersistence(config)
    .AddPresentation(config)
    .AddCarter();

host.UseSerilog((ctx, config) => 
config.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.MapCarter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }      
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(CorsPolicy.AllowOrigin);
app.UseAuthentication();
app.UseAuthorization();

app.Run();

