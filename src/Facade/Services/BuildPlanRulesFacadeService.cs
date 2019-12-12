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

        public SuccessResult<ResponseModel<IEnumerable<BuildPlanRule>>> GetAll(IEnumerable<string> privileges)
        {
            return new SuccessResult<ResponseModel<IEnumerable<BuildPlanRule>>>(
                new ResponseModel<IEnumerable<BuildPlanRule>>(this.repository.FindAll(), privileges));
        }

        public SuccessResult<ResponseModel<BuildPlanRule>> GetById(string ruleCode, IEnumerable<string> privileges)
        {
            return new SuccessResult<ResponseModel<BuildPlanRule>>(
                new ResponseModel<BuildPlanRule>(this.repository.FindBy(b => b.RuleCode == ruleCode), privileges));
        }
    }
}