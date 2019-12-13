namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    public interface IBuildPlanRulesFacadeService
    {
        SuccessResult<ResponseModel<IEnumerable<BuildPlanRule>>> GetAll(IEnumerable<string> privileges);

        SuccessResult<ResponseModel<BuildPlanRule>> GetById(string ruleCode, IEnumerable<string> privileges);
    }
}