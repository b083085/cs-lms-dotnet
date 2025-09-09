using Capstone.LMS.Domain.Primitives;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Role : IdentityRole<Guid>, IAudit
    {
        public Guid CreatedBy { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime ModifiedAtUtc { get; private set; }
        public DateTime? DeletedAtUtc { get; private set; }

        public IReadOnlyList<AccessControl> AccessControls { get; private set; }
    }
}
