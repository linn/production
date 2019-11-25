namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class BuildPlanModule : NancyModule
    {
        private readonly IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService;

        public BuildPlanModule(IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService)
        {
            this.buildPlanService = buildPlanService;
            this.Get("/production/maintenance/build-plans", _ => this.GetBuildPlans());
        }

        private object GetBuildPlans()
        {
            return this.Negotiate.WithModel(this.buildPlanService.GetAll())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }
    }
}