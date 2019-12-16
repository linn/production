namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class BuildPlanDetailRepository : IRepository<BuildPlanDetail, BuildPlanDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BuildPlanDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Remove(BuildPlanDetail entity)
        {
            throw new NotImplementedException();
        }

        public BuildPlanDetail FindBy(Expression<Func<BuildPlanDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlanDetail> FilterBy(Expression<Func<BuildPlanDetail, bool>> expression)
        {
            return this.serviceDbContext.BuildPlanDetails.Where(expression);
        }

        public BuildPlanDetail FindById(BuildPlanDetailKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlanDetail> FindAll()
        {
            return this.serviceDbContext.BuildPlanDetails;
        }

        public void Add(BuildPlanDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}