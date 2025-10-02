using System.Collections.Generic;

namespace Capstone.LMS.Domain.Collections
{
    public sealed class GenreCollection : List<string>
    {
        public GenreCollection()
        {
            Add("Biography");
            Add("Memoir");
            Add("True Crime");
            Add("History");
            Add("Personal Development");
            Add("Philosophy");
            Add("Psychology");
            Add("Science");
            Add("Technology");
            Add("Business");
            Add("Politics");
            Add("Travel");
            Add("Religion");
            Add("Health");
            Add("Education");
            Add("Journalism");
            Add("Sociology");
            Add("Environmental");
        }
    }
}
