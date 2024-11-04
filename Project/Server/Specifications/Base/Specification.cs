using System.Linq.Expressions;

namespace Server.Specifications.Base;

public class Specification<T> : ISpecification<T>
{
    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
        Includes = [];
        IncludeStrings = [];
    }

    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; }
    public List<string> IncludeStrings { get; }
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int Take { get; private set; } = 0;
    public int Skip { get; private set; } = 0;
    public bool IsPagingEnabled { get; private set; } = false;

    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    public void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    public void ApplyPaging(int skip, int take)
    {
        IsPagingEnabled = true;
        Skip = skip;
        Take = take;
    }
}

public class Specification<T, TResult> : Specification<T>, ISpecification<T, TResult>
{
    public Specification(Expression<Func<T, bool>> criteria) : base(criteria)
    {
    }

    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}