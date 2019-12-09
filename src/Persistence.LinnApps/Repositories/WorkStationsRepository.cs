namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class WorkStationsRepository : IRepository<WorkStation, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WorkStationsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WorkStation FindById(string key)
        {
            return this.serviceDbContext.WorkStations.Where(w => w.WorkStationCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<WorkStation> FindAll()
        {
            return this.serviceDbContext.WorkStations;
        }

        public void Add(WorkStation entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(WorkStation entity)
        {
            throw new NotImplementedException();
        }

        public WorkStation FindBy(Expression<Func<WorkStation, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorkStation> FilterBy(Expression<Func<WorkStation, bool>> expression)
        {
            return this.serviceDbContext.WorkStations.Where(expression);
        }
    }
}
