using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace Capstone.LMS.Presentation.Endpoints
{
    public abstract class BaseEndpoints
    {
        protected RouteGroupBuilder CreateMapGroup(IEndpointRouteBuilder app, string groupName = "")
        {
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new Asp.Versioning.ApiVersion(1))
                .ReportApiVersions()
                .Build();

            var prefix = "/api/v{apiVersion:apiVersion}";
            if (!string.IsNullOrEmpty(groupName))
            {
                prefix += $"/{groupName}";
            }

            var mapGroup = app
                .MapGroup(prefix)
                .RequireAuthorization(p =>
                {
                    p.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                    p.RequireAuthenticatedUser();
                })
                .WithApiVersionSet(apiVersionSet);
                

            return mapGroup;
        }
    }
}
