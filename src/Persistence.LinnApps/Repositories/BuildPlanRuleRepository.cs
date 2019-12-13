namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanRuleRepository : IQueryRepository<BuildPlanRule>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BuildPlanRuleRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BuildPlanRule FindBy(Expression<Func<BuildPlanRule, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlanRule> FilterBy(Expression<Func<BuildPlanRule, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BuildPlanRule> FindAll()
        {
            return this.serviceDbContext.BuildPlanRules;
        }
    }
}