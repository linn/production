namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Persistence.LinnApps;

    public class WhoBuiltWhatRepository : IRepository<WhoBuiltWhat, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WhoBuiltWhatRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WhoBuiltWhat FindById(string key)
        {
            return this.serviceDbContext.WhoBuiltWhat
                .Where(f => f.CitCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<WhoBuiltWhat> FindAll()
        {
            return this.serviceDbContext.WhoBuiltWhat;
        }

        public void Add(WhoBuiltWhat entity)
        {
            throw new NotImplementedException();

        }

        public void Remove(WhoBuiltWhat entity)
        {
            throw new NotImplementedException();
        }

        public WhoBuiltWhat FindBy(Expression<Func<WhoBuiltWhat, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WhoBuiltWhat> FilterBy(Expression<Func<WhoBuiltWhat, bool>> expression)
        {
            return this.serviceDbContext.WhoBuiltWhat.Where(expression);
        }
    }
}