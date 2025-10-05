using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.DomainEvents
{
    public sealed record ApprovedBorrowBookDomainEvent(Guid BookBorrowedId) : IDomainEvent;
}
