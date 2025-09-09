using Capstone.LMS.Domain.Primitives;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class User : IdentityUser<Guid>, IAudit
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Gender { get; private set; }

        public Guid CreatedBy { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime ModifiedAtUtc { get; private set; }
        public DateTime? DeletedAtUtc { get; private set; }

        public IReadOnlyList<BorrowedBook> Issues { get; private set; }
    }
}
