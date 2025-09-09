using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Permission : Entity
    {
        private List<DefaultPermission> _defaultPermissions = [];
        private List<AccessControl> _accessControls = [];

        private Permission()
        {
            
        }

        private Permission(
            Guid id,
            string permissionName,
            bool enabled,
            int bitwiseValue)
            : base(id)
        {
            PermissionName = permissionName;
            Enabled = enabled;
            BitwiseValue = bitwiseValue;
        }

        public string PermissionName { get; private set; }
        public bool Enabled { get; private set; }
        public int BitwiseValue { get; private set; }

        public IReadOnlyList<DefaultPermission> DefaultPermissions => [.. _defaultPermissions];
        public IReadOnlyList<AccessControl> AccessControls => [.. _accessControls];

        public static Permission Create(
            Guid id,
            string permissionName,
            bool enabled,
            int bitwiseValue)
        {
            var permission = new Permission(
                id,
                permissionName,
                enabled,
                bitwiseValue);

            return permission;
        }
    }
}
