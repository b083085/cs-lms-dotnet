using Capstone.LMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Capstone.LMS.Domain.Shared
{
    public class DataQuery<T>
    {
        public Expression<Func<T, bool>> Predicate { get; set; } = null;
        public Expression<Func<T, object>> SortBy { get; set; } = null;
        public SortDirection? SortDirection { get; set; } = null;
        public IEnumerable<Expression<Func<T, object>>> Includes { get; set; } = null;
        public bool NoTracking { get; set; } = false;
        public int? Skip { get; set; } = null;
        public int? Take { get; set; } = null;
    }

    public class DataQuery<T, U> : DataQuery<T>
    {
        public Func<T, U> Transform { get; set; } = null;
    }
}
