using Capstone.LMS.Domain.Primitives;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Role : IdentityRole<Guid>, IAudit
    {
        private List<AccessControl> _accessControls = [];

        public Guid CreatedBy { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime ModifiedOnUtc { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }

        public void Created(Guid createdBy)
        {
            CreatedBy = createdBy;
            ModifiedBy = createdBy;
            CreatedOnUtc = DateTime.UtcNow;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        public void Modified(Guid modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        public void Deleted(Guid deletedBy)
        {
            DeletedBy = deletedBy;
            DeletedOnUtc = DateTime.UtcNow;
        }

        public IReadOnlyList<AccessControl> AccessControls => [.. _accessControls];
    }
}
