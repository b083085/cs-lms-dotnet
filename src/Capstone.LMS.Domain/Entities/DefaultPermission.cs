using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class DefaultPermission : Entity
    {
        private DefaultPermission()
        {
            
        }

        private DefaultPermission(
            Guid id,
            Permission permission, 
            SubPermission subPermission)
            : base(id)
        {
            AddPermission(permission);
            AddSubPermission(subPermission);
        }

        public Guid PermissionId { get; private set; }
        public Guid SubPermissionId { get; private set; }

        public Permission Permission { get; private set; }
        public SubPermission SubPermission { get; private set; }

        public void AddPermission(Permission permission) => PermissionId = permission.Id;
        public void AddSubPermission(SubPermission subPermission) => SubPermissionId = subPermission.Id;

        public static DefaultPermission Create(
            Guid id,
            Permission permission,
            SubPermission subPermission)
        {
            return new(
                id,
                permission,
                subPermission);
        }
    }
}
