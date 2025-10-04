using System;
using System.Linq.Expressions;

namespace Capstone.LMS.Domain.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
        {
            return Combine(first, second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return Combine(first, second, Expression.OrElse);
        }

        private static Expression<Func<T, bool>> Combine<T>(
            Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second,
            Func<Expression, Expression, BinaryExpression> merge)
        {
            // Align parameter from second with first
            var parameter = first.Parameters[0];
            var visitor = new ReplaceParameterVisitor(second.Parameters[0], parameter);

            var secondBody = visitor.Visit(second.Body)!;
            var body = merge(first.Body, secondBody);

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private sealed class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _source;
            private readonly ParameterExpression _target;

            public ReplaceParameterVisitor(ParameterExpression source, ParameterExpression target)
            {
                _source = source;
                _target = target;
            }

            protected override Expression VisitParameter(ParameterExpression node)
                => node == _source ? _target : base.VisitParameter(node);
        }
    }
}
