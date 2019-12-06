namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class BuildPlanDetailRepository : IQueryRepository<BuildPlanDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BuildPlanDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BuildPlanDetail FindBy(Expression<Func<BuildPlanDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlanDetail> FilterBy(Expression<Func<BuildPlanDetail, bool>> expression)
        {
            return this.serviceDbContext.BuildPlanDetails.Where(expression);
        }

        public IQueryable<BuildPlanDetail> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}