namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Persistence.LinnApps;
    using Linn.Production.Proxy;

    public class SerialNumberRepository : IRepository<SerialNumber, int>
    {
        private readonly IDatabaseService linnappsDatabaseService;

        private readonly ServiceDbContext serviceDbContext;

        public SerialNumberRepository(ServiceDbContext serviceDbContext, IDatabaseService linnappsDatabaseService)
        {
            this.serviceDbContext = serviceDbContext;
            this.linnappsDatabaseService = linnappsDatabaseService;
        }

        public SerialNumber FindById(int key)
        {
            return this.serviceDbContext.SerialNumbers
                .Where(a => a.SernosTRef == key)
                .ToList()
                .FirstOrDefault();
        }

        public IQueryable<SerialNumber> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(SerialNumber entity)
        {
            entity.SernosTRef = this.linnappsDatabaseService.GetIdSequence("SERNOS_SEQ");
            this.serviceDbContext.SerialNumbers.Add(entity);
        }

        public void Remove(SerialNumber entity)
        {
            throw new NotImplementedException();
        }

        public SerialNumber FindBy(Expression<Func<SerialNumber, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SerialNumber> FilterBy(Expression<Func<SerialNumber, bool>> expression)
        {
            return this.serviceDbContext.SerialNumbers.Where(expression);
        }
    }
}