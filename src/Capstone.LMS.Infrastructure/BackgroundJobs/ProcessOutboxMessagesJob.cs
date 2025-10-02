using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Primitives;
using Capstone.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;

namespace Capstone.LMS.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob(
        LmsContext context,
        IPublisher publisher) : IJob
    {
        private readonly LmsContext _context = context;
        private readonly IPublisher _publisher = publisher;

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _context
                .Set<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            foreach (var message in messages)
            {
                var domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(
                    message.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent is null)
                {
                    // add a log here
                    continue;
                }

                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(
                        3, 
                        attempt => TimeSpan.FromMilliseconds(50 * attempt));

                var result = await policy.ExecuteAndCaptureAsync(() =>
                    _publisher.Publish(
                        domainEvent,
                        context.CancellationToken));

                message.Error = result.FinalException?.ToString();
                message.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
