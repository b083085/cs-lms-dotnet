using System;

namespace Capstone.LMS.Domain.Primitives
{
    public interface IAudit
    {
        Guid CreatedBy { get; }
        Guid ModifiedBy { get; }
        Guid? DeletedBy { get; }
        DateTime CreatedOnUtc { get; }
        DateTime ModifiedOnUtc { get; }
        DateTime? DeletedOnUtc { get; }
        void Created(Guid createdBy);
        void Modified(Guid modifiedBy);
        void Deleted(Guid deletedBy);
    }
}
