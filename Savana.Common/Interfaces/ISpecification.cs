using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Savana.Common.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; } //Where criteria
        List<Expression<Func<T, object>>> Includes { get; } //List of include statements
        Expression<Func<T, object>> OrderByAsc { get; }
        Expression<Func<T, object>> OrderByDesc { get; }
        
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}