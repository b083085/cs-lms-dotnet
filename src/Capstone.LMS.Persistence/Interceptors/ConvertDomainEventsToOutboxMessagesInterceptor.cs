using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Capstone.LMS.Persistence.Interceptors
{
    internal sealed class ConvertDomainEventsToOutboxMessagesInterceptor
        : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var outboxMessages = dbContext.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAggregateRoot)
                .Select(e => e.Entity)
                .SelectMany(e =>
                {
                    var aggregateRoot = e as IAggregateRoot;

                    var domainEvents = aggregateRoot.GetDomainEvents();

                    aggregateRoot.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = DateTime.UtcNow, 
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
                })
                .ToList();

            dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
