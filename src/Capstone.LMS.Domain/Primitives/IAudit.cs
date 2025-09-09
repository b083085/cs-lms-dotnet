using System;

namespace Capstone.LMS.Domain.Primitives
{
    public interface IAudit
    {
        Guid CreatedBy { get; }
        Guid ModifiedBy { get; }
        Guid? DeletedBy { get; }
        DateTime CreatedAtUtc { get; }
        DateTime ModifiedAtUtc { get; }
        DateTime? DeletedAtUtc { get; }
    }
}
