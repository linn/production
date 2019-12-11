namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class BuildPlansModule : NancyModule
    {
        private readonly IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService;

        private readonly IBuildPlansReportFacadeService buildPlansReportService;

        private readonly IBuildPlanRulesFacadeService buildPlanRulesService;

        public BuildPlansModule(
            IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService,
            IBuildPlansReportFacadeService buildPlansReportService,
            IBuildPlanRulesFacadeService buildPlanRulesService)
        {
            this.buildPlanService = buildPlanService;
            this.buildPlansReportService = buildPlansReportService;
            this.buildPlanRulesService = buildPlanRulesService;
            this.Get("/production/maintenance/build-plans", _ => this.GetBuildPlans());
            this.Get("/production/reports/build-plans", _ => this.GetBuildPlanReportOptions());
            this.Get("/production/reports/build-plans/report", _ => this.GetBuildPlanReport());
            this.Get("/production/maintenance/build-plan-rules", _ => this.GetBuildPlanRules());
            this.Get("/production/maintenance/build-plan-rules/{ruleCode}", parameters => this.GetBuildPlanRule(parameters.ruleCode));
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
            var resource = this.Bind<BuildPlanReportRequestResource>();
            return this.Negotiate.WithModel(
                this.buildPlansReportService.GetBuildPlansReport(
                    resource.BuildPlanName,
                    resource.Weeks,
                    resource.CitName));
        }

        private object GetBuildPlanRules()
        {
            return this.Negotiate.WithModel(this.buildPlanRulesService.GetAll())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }

        private object GetBuildPlanRule(string ruleCode)
        {
            return this.Negotiate.WithModel(this.buildPlanRulesService.GetById(ruleCode))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }
    }
}