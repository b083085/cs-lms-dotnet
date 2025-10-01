using Capstone.LMS.Domain.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace Capstone.LMS.Persistence.Interceptors
{
    public sealed class AuditSaveChangesInterceptor(IHttpContextAccessor contextAccessor) : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;


        private Guid GetCurrentUserId()
        {
            return Guid.TryParse(
                _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out Guid parsed)
                ? parsed : Guid.Empty;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if(entry.Entity is IAudit audit)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            audit.Created(GetCurrentUserId());
                            break;
                        case EntityState.Modified:
                            audit.Modified(GetCurrentUserId());
                            break;
                        case EntityState.Deleted:
                            audit.Deleted(GetCurrentUserId());
                            break;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
