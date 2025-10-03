using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Genre : Entity
    {
        private List<Book> _books = [];

        private Genre()
        {

        }

        private Genre(
            Guid id,
            string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public IReadOnlyList<Book> Books => [.. _books];

        public static Genre Create(
            Guid id,
            string name)
        {
            var genre = new Genre(
                id,
                name);

            genre.Created(Guid.Empty);

            return genre;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
