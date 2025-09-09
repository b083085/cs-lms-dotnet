using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class DefaultPermission : Entity
    {
        public DefaultPermission()
            : base(Guid.NewGuid())
        {
            
        }

        public Guid PermissionId { get; private set; }
        public Guid SubPermissionId { get; private set; }

        public Permission Permission { get; private set; }
        public SubPermission SubPermission { get; private set; }
    }
}
