using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class AccessControl : Entity
    {
        public AccessControl()
            : base(Guid.NewGuid())
        {
            
        }

        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }
        public Guid SubPermissionId { get; private set; }

        public Role Role { get; private set; }
        public Permission Permission { get; private set; }
        public SubPermission SubPermission { get; private set; }
    }
}
