using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class SubPermission : Entity
    {
        private List<DefaultPermission> _defaultPermissions = [];
        private List<AccessControl> _accessControls = [];

        private SubPermission()
        {
            
        }

        private SubPermission(
            Guid id,
            string subPermissionName,
            bool enabled,
            int bitwiseValue)
            : base(id)
        {
            SubPermissionName = subPermissionName;
            Enabled = enabled;
            BitwiseValue = bitwiseValue;
        }

        public string SubPermissionName { get; private set; }
        public bool Enabled { get; private set; }
        public int BitwiseValue { get; private set; }

        public IReadOnlyList<DefaultPermission> DefaultPermissions => [.. _defaultPermissions];
        public IReadOnlyList<AccessControl> AccessControls => [.._accessControls];

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;

        public static SubPermission Create(
            Guid id,
            string subPermissionName,
            bool enabled,
            int bitwiseValue)
        {
            var subPermission = new SubPermission(
                id,
                subPermissionName,
                enabled,
                bitwiseValue);

            return subPermission;
        }
    }
}
