namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public class BuildPlanRulesFacadeService : IBuildPlanRulesFacadeService
    {
        private readonly IQueryRepository<BuildPlanRule> repository;

        public BuildPlanRulesFacadeService(IQueryRepository<BuildPlanRule> repository)
        {
            this.repository = repository;
        }

        public SuccessResult<IEnumerable<BuildPlanRule>> GetAll()
        {
            return new SuccessResult<IEnumerable<BuildPlanRule>>(this.repository.FindAll());
        }
    }
}