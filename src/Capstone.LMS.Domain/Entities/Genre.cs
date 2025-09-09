using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Genre : Entity
    {
        public Genre()
            : base(Guid.NewGuid())
        {
            
        }

        public string Name { get; private set; }

        public IReadOnlyList<Book> Books { get; private set; }
    }
}
