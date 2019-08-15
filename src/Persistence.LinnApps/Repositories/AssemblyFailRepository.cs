namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class AssemblyFailRepository : IRepository<AssemblyFail, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AssemblyFailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AssemblyFail FindById(int key)
        {
            return this.serviceDbContext.AssemblyFails
                .Include(f => f.WorksOrder)
                .Include(f => f.CitResponsible)
                .Include(f => f.CompletedBy)
                .Include(f => f.FaultCode)
                .Include(f => f.EnteredBy)
                .Include(f => f.PersonResponsible)
                .Include(f => f.ReturnedBy)
                .Include(f => f.BoardPart)
                .Where(f => f.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<AssemblyFail> FindAll()
        {
            return this.serviceDbContext.AssemblyFails.Include(f => f.WorksOrder);
        }

        public void Add(AssemblyFail entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(AssemblyFail entity)
        {
            throw new NotImplementedException();
        }

        public AssemblyFail FindBy(Expression<Func<AssemblyFail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AssemblyFail> FilterBy(Expression<Func<AssemblyFail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}