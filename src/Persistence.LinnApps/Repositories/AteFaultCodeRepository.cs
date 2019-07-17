namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Persistence.LinnApps;

    public class AteFaultCodeRepository : IRepository<AteFaultCode, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AteFaultCodeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AteFaultCode FindById(string key)
        {
            return this.serviceDbContext.AteFaultCodes
                .Where(f => f.FaultCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<AteFaultCode> FindAll()
        {
            return this.serviceDbContext.AteFaultCodes;
        }

        public void Add(AteFaultCode entity)
        {
            this.serviceDbContext.AteFaultCodes.Add(entity);
        }

        public void Remove(AteFaultCode entity)
        {
            throw new NotImplementedException();
        }

        public AteFaultCode FindBy(Expression<Func<AteFaultCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AteFaultCode> FilterBy(Expression<Func<AteFaultCode, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}