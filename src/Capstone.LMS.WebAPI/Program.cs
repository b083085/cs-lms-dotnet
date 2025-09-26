using Capstone.LMS.Application;
using Capstone.LMS.Infrastructure;
using Capstone.LMS.Persistence;
using Capstone.LMS.Presentation;
using Carter;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services
    .AddOpenApi()
    .AddApplication(config)
    .AddInfrastructure(config)
    .AddPersistence(config)
    .AddPresentation(config)
    .AddCarter();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCarter();

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseCors();
//app.UseAuthentication();
//app.UseAuthorization();

app.Run();

