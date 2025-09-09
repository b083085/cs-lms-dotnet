using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Permission : Entity
    {
        public Permission()
            : base(Guid.NewGuid())
        {
            
        }

        public string PermissionName { get; private set; }
        public bool Enabled { get; private set; }
        public int BitwiseValue { get; private set; }

        public IReadOnlyList<DefaultPermission> DefaultPermissions { get; private set; }

        public IReadOnlyList<AccessControl> AccessControls { get; private set; }
    }
}
