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

        public IReadOnlyList<AccessControl> AccessControls => [.. _accessControls];
    }
}
