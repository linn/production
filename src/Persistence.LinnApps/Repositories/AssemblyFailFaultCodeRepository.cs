namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    public class AssemblyFailFaultCodeRepository : IRepository<AssemblyFailFaultCode, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AssemblyFailFaultCodeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AssemblyFailFaultCode FindById(string key)
        {
            return this.serviceDbContext.AssemblyFailFaultCodes.Where(a => a.FaultCode == key).ToList()
                .FirstOrDefault();
        }

        public IQueryable<AssemblyFailFaultCode> FindAll()
        {
            return this.serviceDbContext.AssemblyFailFaultCodes.OrderBy(c => c.FaultCode);
        }

        public void Add(AssemblyFailFaultCode entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(AssemblyFailFaultCode entity)
        {
            throw new NotImplementedException();
        }

        public AssemblyFailFaultCode FindBy(Expression<Func<AssemblyFailFaultCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AssemblyFailFaultCode> FilterBy(Expression<Func<AssemblyFailFaultCode, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}