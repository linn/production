namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Repositories;

    public class LinnWeekRepository : ILinnWeekRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public LinnWeekRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public LinnWeek FindById(int key)
        {
            return this.serviceDbContext.LinnWeeks
                .Where(f => f.LinnWeekNumber == key).ToList().FirstOrDefault();
        }

        public IQueryable<LinnWeek> FindAll()
        {
            return this.serviceDbContext.LinnWeeks;
        }

        public void Add(LinnWeek entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(LinnWeek entity)
        {
            throw new NotImplementedException();
        }

        public LinnWeek FindBy(Expression<Func<LinnWeek, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LinnWeek> FilterBy(Expression<Func<LinnWeek, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LinnWeek> GetWeeks(DateTime startDate, DateTime endDate)
        {
            return this.serviceDbContext.LinnWeeks
                .Where(f => f.StartDate <= startDate && f.EndDate >= endDate).ToList();
        }

        public LinnWeek GetWeek(DateTime date)
        {
            return this.serviceDbContext.LinnWeeks
                .Where(f => f.StartDate <= date && f.EndDate >= date).ToList().FirstOrDefault();
        }
    }
}