namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    using Microsoft.EntityFrameworkCore;

    public class PartFailRepository : IRepository<PartFail, int>
    {
        private readonly ServiceDbContext serviceDbContext;


        public PartFailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartFail FindById(int key)
        {
            return this.serviceDbContext
                .PartFails
                .Include(f => f.EnteredBy)
                .Include(f => f.FaultCode)
                .Include(f => f.WorksOrder)
                .Include(f => f.ErrorType)
                .Include(f => f.Part)
                .Include(f => f.StorageLocation)
                .Where(f => f.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<PartFail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PartFail entity)
        {
            this.serviceDbContext.PartFails.Add(entity);
        }

        public void Remove(PartFail entity)
        {
            throw new NotImplementedException();
        }

        public PartFail FindBy(Expression<Func<PartFail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartFail> FilterBy(Expression<Func<PartFail, bool>> expression)
        {
            return this.serviceDbContext
                .PartFails
                .Where(expression)
                .Include(f => f.EnteredBy)
                .Include(f => f.FaultCode)
                .Include(f => f.WorksOrder)
                .Include(f => f.ErrorType)
                .Include(f => f.Part)
                .Include(f => f.StorageLocation)
                .OrderBy(f => f.Id.ToString().Length);
        }
    }
}