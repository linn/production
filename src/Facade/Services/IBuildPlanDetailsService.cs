namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public interface IBuildPlanDetailsService : IFacadeService<BuildPlanDetail, BuildPlanDetailKey,
        BuildPlanDetailResource, BuildPlanDetailResource>
    {
        IResult<ResponseModel<BuildPlanDetail>> UpdateBuildPlanDetail(
            BuildPlanDetailResource resource,
            IEnumerable<string> privileges);
    }
}