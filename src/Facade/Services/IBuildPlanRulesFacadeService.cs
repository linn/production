namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public interface IBuildPlanRulesFacadeService
    {
        SuccessResult<IEnumerable<BuildPlanRule>> GetAll();

        SuccessResult<BuildPlanRule> GetById(string ruleCode);
    }
}