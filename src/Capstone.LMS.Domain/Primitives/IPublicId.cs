using System;

namespace Capstone.LMS.Domain.Primitives
{
    public interface IPublicId
    {
        Guid PublicId { get; }
        void SetPublicId();
    }
}
