namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanDetailsResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<BuildPlanDetail>>>
    {
        private readonly BuildPlanDetailResourceBuilder buildPlanDetailResourceBuilder;

        public BuildPlanDetailsResourceBuilder(IAuthorisationService authorisationService)
        {
            this.buildPlanDetailResourceBuilder = new BuildPlanDetailResourceBuilder(authorisationService);
        }

        public IEnumerable<BuildPlanDetailResource> Build(ResponseModel<IEnumerable<BuildPlanDetail>> model)
        {
            var buildPlanDetails = model.ResponseData;

            return buildPlanDetails.Select(
                b => this.buildPlanDetailResourceBuilder.Build(
                    new ResponseModel<BuildPlanDetail>(b, model.Privileges)));
        }

        object IResourceBuilder<ResponseModel<IEnumerable<BuildPlanDetail>>>.Build(
            ResponseModel<IEnumerable<BuildPlanDetail>> buildPlanDetails) =>
            this.Build(buildPlanDetails);

        public string GetLocation(ResponseModel<IEnumerable<BuildPlanDetail>> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
