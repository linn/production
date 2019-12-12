namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    public class BuildPlanResourceBuilder : IResourceBuilder<ResponseModel<BuildPlan>>
    {
        private readonly IAuthorisationService authorisationService;

        public BuildPlanResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public BuildPlanResource Build(ResponseModel<BuildPlan> model)
        {
            var buildPlan = model.ResponseData;

            return new BuildPlanResource
                       {
                           BuildPlanName = buildPlan.BuildPlanName,
                           DateCreated = buildPlan.DateCreated.ToString("o"),
                           DateInvalid = buildPlan.DateInvalid?.ToString("o"),
                           Description = buildPlan.Description,
                           LastMrpDateFinished = buildPlan.LastMrpDateFinished?.ToString("o"),
                           LastMrpDateStarted = buildPlan.LastMrpDateStarted?.ToString("o"),
                           LastMrpJobRef = buildPlan.LastMrpJobRef,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ResponseModel<BuildPlan> model)
        {
            return $"/production/maintenance/build-plans/{model.ResponseData.BuildPlanName}";
        }

        object IResourceBuilder<ResponseModel<BuildPlan>>.Build(ResponseModel<BuildPlan> buildPlan) => this.Build(buildPlan);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<BuildPlan> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.BuildPlanAdd, model.Privileges))
            {
                yield return new LinkResource { Rel = "create", Href = this.GetLocation(model) };
            }

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.BuildPlanUpdate, model.Privileges))
            {
                yield return new LinkResource { Rel = "update", Href = this.GetLocation(model) };
            }
        }
    }
}