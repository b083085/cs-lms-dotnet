using System.Collections.Generic;

namespace Capstone.LMS.Domain.Primitives
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> GetDomainEvents();
        void ClearDomainEvents();
    }
}
