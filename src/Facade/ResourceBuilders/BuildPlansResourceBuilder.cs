namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;


    public class BuildPlansResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<BuildPlan>>>
    {
        private readonly BuildPlanResourceBuilder buildPlanResourceBuilder;

        public BuildPlansResourceBuilder(IAuthorisationService authorisationService)
        {
            this.buildPlanResourceBuilder = new BuildPlanResourceBuilder(authorisationService);
        }

        public IEnumerable<BuildPlanResource> Build(ResponseModel<IEnumerable<BuildPlan>> model)
        {
            var buildPlans = model.ResponseData;

            return buildPlans.OrderByDescending(b => b.BuildPlanName).Select(
                b => this.buildPlanResourceBuilder.Build(new ResponseModel<BuildPlan>(b, model.Privileges)));
        }

        object IResourceBuilder<ResponseModel<IEnumerable<BuildPlan>>>.Build(ResponseModel<IEnumerable<BuildPlan>> buildPlans) =>
            this.Build(buildPlans);

        public string GetLocation(ResponseModel<IEnumerable<BuildPlan>> model)
        {
            throw new System.NotImplementedException();
        }
    }
}