using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Author : Entity
    {
        private List<Book> _books = [];
        private Author()
        {
            
        }

        private Author(
            Guid id,
            string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public IReadOnlyList<Book> Books => [.. _books];

        public static Author Create(
            Guid id,
            string name)
        {
            return new(id, name);
        }
    }
}
