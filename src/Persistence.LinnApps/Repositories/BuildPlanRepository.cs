namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanRepository : IRepository<BuildPlan, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BuildPlanRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BuildPlan FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlan> FindAll()
        {
            return this.serviceDbContext.BuildPlans;
        }

        public void Add(BuildPlan entity)
        {
            this.serviceDbContext.BuildPlans.Add(entity);
        }

        public void Remove(BuildPlan entity)
        {
            throw new NotImplementedException();
        }

        public BuildPlan FindBy(Expression<Func<BuildPlan, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlan> FilterBy(Expression<Func<BuildPlan, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}