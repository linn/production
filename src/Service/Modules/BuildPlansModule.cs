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

        private readonly IBuildPlanDetailsService buildPlanDetailsService;

        public BuildPlansModule(
            IFacadeService<BuildPlan, string, BuildPlanResource, BuildPlanResource> buildPlanService,
            IBuildPlansReportFacadeService buildPlansReportService,
            IBuildPlanRulesFacadeService buildPlanRulesService,
            IBuildPlanDetailsService buildPlanDetailsService)
        {
            this.buildPlanService = buildPlanService;
            this.buildPlansReportService = buildPlansReportService;
            this.buildPlanRulesService = buildPlanRulesService;
            this.buildPlanDetailsService = buildPlanDetailsService;

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
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.buildPlanService.GetAll(privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object AddBuildPlan()
        {
            var resource = this.Bind<BuildPlanResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.buildPlanService.Add(resource, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object UpdateBuildPlan()
        {
            var resource = this.Bind<BuildPlanResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.buildPlanService.Update(resource.BuildPlanName, resource, privileges))
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
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            
            return this.Negotiate.WithModel(this.buildPlanRulesService.GetAll(privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }

        private object GetBuildPlanRule(string ruleCode)
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            
            return this.Negotiate.WithModel(this.buildPlanRulesService.GetById(ruleCode, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }

        private object GetBuildPlanDetails()
        {
            var resource = this.Bind<BuildPlanDetailsRequestResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (string.IsNullOrEmpty(resource.BuildPlanName))
            {
                return this.Negotiate.WithModel(this.buildPlanDetailsService.GetAll(privileges))
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
            }

            return this.Negotiate.WithModel(this.buildPlanDetailsService.Search(resource.BuildPlanName, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }

        private object AddBuildPlanDetail()
        {
            var resource = this.Bind<BuildPlanDetailResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.buildPlanDetailsService.Add(resource, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }

        private object UpdateBuildPlanDetail()
        {
            var resource = this.Bind<BuildPlanDetailResource>();

            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.buildPlanDetailsService.UpdateBuildPlanDetail(resource, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get()).WithView("Index");
        }
    }
}