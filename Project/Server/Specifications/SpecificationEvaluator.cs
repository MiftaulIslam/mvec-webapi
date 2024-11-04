using Microsoft.EntityFrameworkCore;
using Server.Entities;
using Server.Specifications.Base;
using System.Linq.Expressions;

namespace Server.Specifications;

public static class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        IQueryable<T> query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        foreach (Expression<Func<T, object>> include in specification.Includes)
        {
            query = query.Include(include);
        }

        foreach (string includeString in specification.IncludeStrings)
        {
            query = query.Include(includeString);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> specification)
    {
        IQueryable<T> query = GetQuery(inputQuery, (ISpecification<T>)specification);

        return specification.Select != null
            ? query.Select(specification.Select)
            : query.Cast<TResult>();
    }
}