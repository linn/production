namespace Linn.Production.Service.Modules
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class BuildPlansModule : NancyModule
    {
        private readonly IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService;

        private readonly IBuildPlansReportFacadeService buildPlansReportService;

        private readonly IBuildPlanRulesFacadeService buildPlanRulesService;

        private readonly
            IFacadeService<BuildPlanDetail, BuildPlanDetailKey, BuildPlanDetailResource, BuildPlanDetailResource>
            buildPlanDetailService;

        public BuildPlansModule(
            IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService,
            IBuildPlansReportFacadeService buildPlansReportService,
            IBuildPlanRulesFacadeService buildPlanRulesService,
            IFacadeService<BuildPlanDetail, BuildPlanDetailKey, BuildPlanDetailResource, BuildPlanDetailResource> buildPlanDetailService)
        {
            this.buildPlanService = buildPlanService;
            this.buildPlansReportService = buildPlansReportService;
            this.buildPlanRulesService = buildPlanRulesService;
            this.buildPlanDetailService = buildPlanDetailService;
            this.Get("/production/maintenance/build-plans", _ => this.GetBuildPlans());
            this.Post("/production/maintenance/build-plans", _ => this.AddBuildPlan());
            this.Put("/production/maintenance/build-plans", _ => this.UpdateBuildPlan());
            this.Get("/production/reports/build-plans", _ => this.GetBuildPlanReportOptions());
            this.Get("/production/reports/build-plans/report", _ => this.GetBuildPlanReport());
            this.Get("/production/maintenance/build-plan-rules", _ => this.GetBuildPlanRules());
            this.Get("/production/maintenance/build-plan-rules/{ruleCode}", parameters => this.GetBuildPlanRule(parameters.ruleCode));
            this.Get("/production/maintenance/build-plan-details", _ => this.GetBuildPlanDetails());
            this.Post("/production/maintenance/build-plan-details", _ => this.AddBuildPlanDetail());
            this.Put("/production/maintenance/build-plan-details", _ => this.UpdateBuildPlanDetail());
        }

        private object GetBuildPlans()
        {
            return this.Negotiate.WithModel(this.buildPlanService.GetAll())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object AddBuildPlan()
        {
            var resource = this.Bind<BuildPlanResource>();

            return this.Negotiate.WithModel(this.buildPlanService.Add(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object UpdateBuildPlan()
        {
            var resource = this.Bind<BuildPlanResource>();
            return this.Negotiate.WithModel(this.buildPlanService.Update(resource.BuildPlanName, resource))
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

        private object GetBuildPlanDetails()
        {
            var resource = this.Bind<BuildPlanDetailsRequestResource>();
            if (string.IsNullOrEmpty(resource.BuildPlanName))
            {
                return this.Negotiate.WithModel(this.buildPlanDetailService.GetAll())
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
            }

            return this.Negotiate.WithModel(this.buildPlanDetailService.Search(resource.BuildPlanName))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }

        private object AddBuildPlanDetail()
        {
            var resource = this.Bind<BuildPlanDetailResource>();
            return this.Negotiate.WithModel(this.buildPlanDetailService.Add(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }

        private object UpdateBuildPlanDetail()
        {
            var resource = this.Bind<BuildPlanDetailResource>();

            var key = new BuildPlanDetailKey
                          {
                              BuildPlanName = resource.BuildPlanName,
                              PartNumber = resource.PartNumber,
                              FromLinnWeekNumber = resource.FromLinnWeekNumber
                          };

            return this.Negotiate.WithModel(this.buildPlanDetailService.Update(key, resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }
    }
}