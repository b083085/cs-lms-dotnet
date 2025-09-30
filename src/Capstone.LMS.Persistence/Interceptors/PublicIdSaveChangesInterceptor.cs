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
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in addedEntries)
            {
                var property = entry.Entity
                    .GetType()
                    .GetProperty(
                        nameof(Entity.PublicId), 
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if(property != null && string.IsNullOrWhiteSpace(property.GetValue(entry.Entity) as string))
                {
                    property.SetValue(entry.Entity, Guid.NewGuid());
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
