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

            var prefix = "api/v{apiVersion:apiVersion}";
            if (!string.IsNullOrEmpty(groupName))
            {
                prefix += $"/{groupName}";
            }

            var mapGroup = app
                .MapGroup(prefix)
                .WithApiVersionSet(apiVersionSet);

            return mapGroup;
        }
    }
}
