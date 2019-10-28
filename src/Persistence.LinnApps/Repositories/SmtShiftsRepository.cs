namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class SmtShiftsRepository : IRepository<SmtShift, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SmtShiftsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SmtShift FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SmtShift> FindAll()
        {
            return this.serviceDbContext.SmtShifts;
        }

        public void Add(SmtShift entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(SmtShift entity)
        {
            throw new NotImplementedException();
        }

        public SmtShift FindBy(Expression<Func<SmtShift, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SmtShift> FilterBy(Expression<Func<SmtShift, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}