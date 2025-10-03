using Capstone.LMS.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Capstone.LMS.Persistence.Interceptors
{
    public sealed class PublicIdSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            var addedEntries = context.ChangeTracker
                .Entries()
                .Where(e => 
                    e.State == EntityState.Added &&
                    e.Entity is IPublicId);

            foreach (var entry in addedEntries)
            {
                ((IPublicId)entry.Entity).SetPublicId();
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
