namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IQueryRepository<T>
    {
        T FindBy(Expression<Func<T, bool>> expression);

        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
    }
}