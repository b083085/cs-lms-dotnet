using Capstone.LMS.Application;
using Capstone.LMS.Infrastructure;
using Capstone.LMS.Persistence;
using Capstone.LMS.Presentation;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddOpenApi();
services.AddApplication(config);
services.AddInfrastructure(config);
services.AddPersistence(config);
services.AddPresentation(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseCors();
//app.UseAuthentication();
//app.UseAuthorization();

app.Run();

