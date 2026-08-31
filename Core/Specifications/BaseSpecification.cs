using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
    {
        protected BaseSpecification() : this(null) { }

        public Expression<Func<T, bool>> Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<T, Object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T, Object>> OrderByDescendingExpression)
        {
            OrderByDescending = OrderByDescendingExpression;
        }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }

            return query;
        }

    }

    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria)
    : BaseSpecification<T>(criteria), ISpecification<T, TResult>
    {
        protected BaseSpecification() : this(null!) { }
        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }
}