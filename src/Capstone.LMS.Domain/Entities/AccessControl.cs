using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class AccessControl : Entity
    {
        private AccessControl()
        {
            
        }

        private AccessControl(
            Guid id,
            Role role,
            Permission permission,
            SubPermission subPermission)
            : base(id)
        {
            AddRole(role);
            AddPermission(permission);
            AddSubPermission(subPermission);
        }

        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }
        public Guid SubPermissionId { get; private set; }

        public Role Role { get; private set; }
        public Permission Permission { get; private set; }
        public SubPermission SubPermission { get; private set; }

        public void AddRole(Role role) => RoleId = role.Id;
        public void AddPermission(Permission permission) => PermissionId = permission.Id;
        public void AddSubPermission(SubPermission subPermission) => SubPermissionId = subPermission.Id;

        public static AccessControl Create(
            Guid id,
            Role role,
            Permission permission,
            SubPermission subPermission)
        {
            return new(
                id,
                role,
                permission,
                subPermission);
        }
    }
}
