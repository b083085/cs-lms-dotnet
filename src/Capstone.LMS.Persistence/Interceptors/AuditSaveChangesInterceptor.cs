using Capstone.LMS.Application.Extensions;
using Capstone.LMS.Domain.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Capstone.LMS.Persistence.Interceptors
{
    public sealed class AuditSaveChangesInterceptor(IHttpContextAccessor contextAccessor) : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

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
                            audit.Created(_contextAccessor.GetCurrentUserId());
                            break;
                        case EntityState.Modified:
                            audit.Modified(_contextAccessor.GetCurrentUserId());
                            break;
                        case EntityState.Deleted:
                            audit.Deleted(_contextAccessor.GetCurrentUserId());
                            break;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
