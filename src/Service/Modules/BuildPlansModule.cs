namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class BuildPlansModule : NancyModule
    {
        private readonly IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService;

        public BuildPlansModule(IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService)
        {
            this.buildPlanService = buildPlanService;
            this.Get("/production/maintenance/build-plans", _ => this.GetBuildPlans());
            this.Get("/production/reports/build-plans", _ => this.GetBuildPlanReportOptions());
            this.Get("/production/reports/build-plans/report", _ => this.GetBuildPlanReport());
        }

        private object GetBuildPlans()
        {
            return this.Negotiate.WithModel(this.buildPlanService.GetAll())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetBuildPlanReportOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetBuildPlanReport()
        {
            throw new System.NotImplementedException();
        }
    }
}