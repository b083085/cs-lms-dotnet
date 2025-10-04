using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.DomainEvents
{
    public sealed record BorrowedBookDomainEvent(Guid BookId) : IDomainEvent;
}
