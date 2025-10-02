using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.DomainEvents
{
    public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
}
